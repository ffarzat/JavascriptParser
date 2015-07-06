using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using AForge.Genetic;
using Jurassic;
using unvell.ReoScript;

namespace ConsoleApplication1
{
    /// <summary>
    /// Representa uma função de fitness do 
    /// </summary>
    public class JavascriptFitness : IFitnessFunction
    {
        /// <summary>
        /// Path for generate the target code
        /// </summary>
        private string _pathToExecution;

        /// <summary>
        /// Dir of this run
        /// </summary>
        private DirectoryInfo _dirOfRun;

        /// <summary>
        /// Path to test script
        /// </summary>
        private string _scriptTestPtah;

        /// <summary>
        /// Jurassic.ScriptEngine to run javascript
        /// </summary>
        private ScriptEngine _engine;

        /// <summary>
        /// QUnit path
        /// </summary>
        private string _qunitPath;

        /// <summary>
        /// Alert delegate
        /// </summary>
        /// <param name="message"></param>
        private delegate void DAlertDelegate(string message);

        /// <summary>
        /// Do the setup for a future execution
        /// </summary>
        public JavascriptFitness(string pathToExecution, string scriptTestPtah)
        {
            _pathToExecution = pathToExecution;
            _scriptTestPtah = scriptTestPtah;
            _engine= new ScriptEngine();
            DirectorySetup(pathToExecution);
        }


        /// <summary>
        /// Do the setup for a future execution
        /// </summary>
        public JavascriptFitness(string pathToExecution, string scriptTestPtah, string qunitPath)
        {
            _pathToExecution = pathToExecution;
            _scriptTestPtah = scriptTestPtah;
            _qunitPath = qunitPath;

            _engine = new ScriptEngine();
            DirectorySetup(pathToExecution);
            //LoadQunitAndTests();
        }

        /// <summary>
        /// Loads Once a Qunit JsFile
        /// </summary>
        private void LoadQunitAndTests()
        {
            _engine.SetGlobalFunction("alert", new DAlertDelegate(Console.WriteLine));
            _engine.ExecuteFile(_qunitPath);
            #region registra os retornos dos testes
            _engine.Execute(@"   var total, sucess, fail, time;
                                    QUnit.done(function( details ) {
                                    //alert('=============================================');
                                    //alert('Total:' + details.total);
                                    //alert('Falha:' + details.failed);
                                    //alert('Sucesso:' + details.passed);
                                    //alert('Tempo:' + details.runtime);

                                    total = details.total;
                                    sucess = details.passed;
                                    fail = details.failed;
                                    time = details.runtime;

                                });

                                /*
                                QUnit.testDone(function( details ) {
                                    alert('Modulo:' + details.module);
                                    alert('Teste:' + details.name);
                                    alert(' Falha:' + details.failed);
                                    alert(' Total:' + details.total);
                                    alert(' Tempo:' + details.duration);
                                });
                                */
                                QUnit.config.autostart = false;
                                QUnit.config.ignoreGlobalErrors = true;
                        ");
            #endregion

            _engine.ExecuteFile(_scriptTestPtah);

            _engine.Execute(@"QUnit.load();");
        }

        /// <summary>
        /// Cleanup or Creates the target directory for run
        /// </summary>
        /// <param name="pathToExecution"></param>
        private void DirectorySetup(string pathToExecution)
        {
            var dirInfo = new DirectoryInfo(_pathToExecution);

            #region setup directory

            if (!dirInfo.Exists)
                dirInfo.Create();

            _dirOfRun = new DirectoryInfo(pathToExecution + "/" + DateTime.Now.ToString("yyyy_MM_dd"));
            _dirOfRun.Create();

            #endregion
        }

        /// <summary>
        /// Evaluates chromosome
        /// </summary>
        public double Evaluate(IChromosome chromosome)
        {
            DirectoryInfo directoryForIndividual = null;
            double fitness = double.MaxValue;
            
            #region setup a directory for this individual?
            var createNewDirectoryForGeneration = _dirOfRun.GetDirectories().FirstOrDefault(d => d.Name == chromosome.GenerationId.ToString(CultureInfo.InvariantCulture)) == null;

            if (createNewDirectoryForGeneration)
                _dirOfRun.CreateSubdirectory(chromosome.GenerationId.ToString(CultureInfo.InvariantCulture));

            directoryForIndividual =
                _dirOfRun.GetDirectories()
                        .FirstOrDefault(d => d.Name == chromosome.GenerationId.ToString(CultureInfo.InvariantCulture));

            #endregion

            #region gerar o código em um diretório temporário, testar compilação
            string fileName = string.Format("{0}/{1}.js", directoryForIndividual.FullName, chromosome.Id);
            chromosome.File = fileName;
            try
            {
                var codeGenerator = new JavascriptAstCodeGenerator(((JavascriptChromosome)chromosome).Tree);
                string generatedJsCode = codeGenerator.DoCodeTransformation();
                File.WriteAllText(fileName, generatedJsCode);
            }
            catch (Exception)
            {
                return fitness;
                //TODO: logar a exceção para tratamento do gerador de código
            }
            
            #endregion
            
            #region Executar os testes no novo Js (medindo tempo)
            var sw = new Stopwatch();
            sw.Start();

            double total, sucess, fail, time;
            Console.WriteLine("=====================================");
            Console.WriteLine(chromosome.Id);
            try
            {
                _engine = new ScriptEngine();
                LoadQunitAndTests();
                _engine.ExecuteFile(fileName);
                _engine.Execute(@"QUnit.start();");

                total = _engine.GetGlobalValue<double>("total");
                sucess = _engine.GetGlobalValue<double>("sucess");
                fail = _engine.GetGlobalValue<double>("fail");
                time = _engine.GetGlobalValue<double>("time");

            }
            catch (JavaScriptException ex)
            {
                Console.WriteLine(string.Format("Script error in \'{0}\', line: {1}\n{2}", ex.SourcePath, ex.LineNumber, ex.Message));
                return fitness;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return fitness;
            }


            sw.Stop();
            
            //Console.WriteLine("Fitness {0} segundos", sw.Elapsed.Seconds);

            double totalTestok = 69;// 39287;

            if (totalTestok.Equals(total))
                fitness = double.Parse(sw.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));

            #endregion
            
            Console.WriteLine("Fitness={0}", fitness);
            return fitness;
        }

        /// <summary>
        /// Translates genotype to phenotype 
        /// </summary>
        public object Translate(IChromosome chromosome)
        {
            return chromosome.ToString();
        }
    }
}
