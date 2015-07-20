using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using AForge.Genetic;
using Jurassic;
using NLog;

namespace ConsoleApplication1
{
    /// <summary>
    /// Representa uma função de fitness do 
    /// </summary>
    public class JavascriptFitness : IFitnessFunction
    {

        /// <summary>
        /// NLog Logger
        /// </summary>
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Simple Singleton
        /// </summary>
        private static ScriptEngine _engine = new ScriptEngine();

        /// <summary>
        /// Ancestor Js
        /// </summary>
        private JavascriptChromosome _ancestor;

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
            DirectorySetup(pathToExecution);
        }


        /// <summary>
        /// Do the setup for a future execution
        /// </summary>
        public JavascriptFitness(JavascriptChromosome ancestor, string pathToExecution, string scriptTestPtah, string qunitPath)
        {
            _ancestor = ancestor;
            _pathToExecution = pathToExecution;
            _scriptTestPtah = scriptTestPtah;
            _qunitPath = qunitPath;
            DirectorySetup(pathToExecution);
        }

        /// <summary>
        /// Gets a full clone of object
        /// </summary>
        /// <returns></returns>
        public IFitnessFunction Clone()
        {
            return  new JavascriptFitness(_ancestor, this._pathToExecution, this._scriptTestPtah, this._qunitPath);
        }

        /// <summary>
        /// Loads Once a Qunit JsFile
        /// </summary>
        private void LoadQunitAndTests(ScriptEngine engine)
        {
            engine.SetGlobalFunction("alert", new DAlertDelegate(message => Log.WriteLine(message, LogLevel.Trace)));
            engine.ExecuteFile(_qunitPath);
            #region registra os retornos dos testes
            engine.Execute(@"   var total, sucess, fail, time;
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

                                QUnit.testDone(function(details) {
                                    //alert(details);
                                    if (details.failed) {
                                        throw new Error('Falhou no teste: ' + details.module + ':' + details.name);
                                    }
                                });

                                /*
                                QUnit.log(function( details ) {
                                  if ( details.result ) {
                                    return;
                                  }
                                  var loc = details.module + ': ' + details.name + ': ',
                                    output = 'FAILED: ' + loc + ( details.message ? details.message + ', ' : '' );
 
                                  if ( details.actual ) {
                                    output += 'expected: ' + details.expected + ', actual: ' + details.actual;
                                  }
                                  if ( details.source ) {
                                    output += ', ' + details.source;
                                  }

                                    alert('=============================================');
                                    alert( output );
                                });
                                */

                                QUnit.config.autostart = false;
                                QUnit.config.ignoreGlobalErrors = false;
                        ");
            #endregion

            engine.ExecuteFile(_scriptTestPtah);

            engine.Execute(@"QUnit.load();");
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

            //if (!chromosome.Fitness.Equals(0)) //alredy evaluated
            //    return chromosome.Fitness;

            DirectoryInfo directoryForIndividual = null;
            double fitness = double.MaxValue;
            string generatedJsCode = "";
            var sw = new Stopwatch();
            sw.Start();

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
                generatedJsCode = codeGenerator.DoCodeTransformation();
                ((JavascriptChromosome) chromosome).Code = generatedJsCode;
                File.WriteAllText(fileName, generatedJsCode);

                if (((JavascriptChromosome)chromosome).Code.Equals(_ancestor.ToString()) & !chromosome.Id.Equals(_ancestor.Id))
                {
                    Log.WriteLine(string.Format("     {0} -> {1} (em {2} ) [{3}]", chromosome.Id, fitness, sw.Elapsed.ToString("mm\\:ss\\.ff"), "Similar ao original"), LogLevel.Trace);
                    return fitness;
                }

            }
            catch (Exception)
            {
                return fitness;
                //TODO: logar a exceção para tratamento do gerador de código
            }
            
            #endregion
            
            #region Executar os testes no novo Js (medindo tempo)
            

            double total, sucess, fail, time;
            try
            {
                lock (_engine)
                {
                    Cleanup(_engine);
                    _engine.Execute(generatedJsCode);
                    LoadQunitAndTests(_engine);

                    _engine.Execute(@"QUnit.start();");

                    //total = _engine.GetGlobalValue<double>("total");
                    //sucess = _engine.GetGlobalValue<double>("sucess");
                    //fail = _engine.GetGlobalValue<double>("fail");
                    //time = _engine.GetGlobalValue<double>("time");
                }
            }
            catch (JavaScriptException ex)
            {
                //Log.WriteLine(string.Format(string.Format("Script error in \'{0}\', line: {1}\n{2}", ex.SourcePath, ex.LineNumber, ex.Message)));
                Log.WriteLine(string.Format("     {0} -> {1} (em {2} ) [{3}]", chromosome.Id, fitness, sw.Elapsed.ToString("mm\\:ss\\.ff"), ex.Message), LogLevel.Error);
                return fitness;
            }
            catch (Exception ex)
            {
                Log.WriteLine(string.Format("     {0} -> {1} (em {2} ) [{3}]", chromosome.Id, fitness, sw.Elapsed.ToString("mm\\:ss\\.ff"), ex.Message), LogLevel.Error);
                return fitness;
            }

            sw.Stop();

            fitness = double.Parse(sw.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
            
            #endregion


            Log.WriteLine(string.Format("     {0} -> {1} (em {2} )", chromosome.Id, fitness, sw.Elapsed.ToString("mm\\:ss\\.ff")), LogLevel.Trace);
            return fitness;
        }

        /// <summary>
        /// Cleans the engine for a new excetuion
        /// </summary>
        /// <param name="engine"></param>
        private void Cleanup(ScriptEngine engine)
        {
            _logger.Trace("     Limpando {0}", "moment");
            engine.Global.Delete("moment", false);

            _logger.Trace("     Limpando {0}", "QUnit");
            engine.Global.Delete("QUnit", false);
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
