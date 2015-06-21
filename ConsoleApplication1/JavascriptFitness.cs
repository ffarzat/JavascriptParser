using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using AForge.Genetic;
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
        private readonly string _pathToExecution;

        /// <summary>
        /// Dir of this run
        /// </summary>
        private DirectoryInfo dirOfRun;

        /// <summary>
        /// Path to test script
        /// </summary>
        private string _scriptTestPtah;

        /// <summary>
        /// Do the setup for a future execution
        /// </summary>
        public JavascriptFitness(string pathToExecution, string scriptTestPtah)
        {
            _pathToExecution = pathToExecution;
            _scriptTestPtah = scriptTestPtah;
            var dirInfo = new DirectoryInfo(_pathToExecution);

            #region setup directory
            if (!dirInfo.Exists)
                dirInfo.Create();
            
            dirOfRun = new DirectoryInfo(pathToExecution + "/" + DateTime.Now.ToString("yyyy_MM_dd"));
            dirOfRun.Create();

            #endregion
        }

        /// <summary>
        /// Evaluates chromosome
        /// </summary>
        public double Evaluate(IChromosome chromosome)
        {
            DirectoryInfo directoryForIndividual = null;
            double fitness = double.MaxValue;
            var scriptRunning = new ScriptRunningMachine();

            #region setup a directory for this individual?
            var createNewDirectoryForGeneration = dirOfRun.GetDirectories().FirstOrDefault(d => d.Name == chromosome.GenerationId.ToString(CultureInfo.InvariantCulture)) == null;

            if (createNewDirectoryForGeneration)
                dirOfRun.CreateSubdirectory(chromosome.GenerationId.ToString(CultureInfo.InvariantCulture));

            directoryForIndividual =
                dirOfRun.GetDirectories()
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
                var compiledJs = scriptRunning.Compile(generatedJsCode);
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
            
            scriptRunning.AllowDirectAccess = true;
            scriptRunning.Load(fileName);
           
            /*Meu macete pessoal*/
            scriptRunning["print"] = new NativeFunctionObject("print", (ctx, owner, args) =>
            {
                //Console.WriteLine(args[0]);
                System.Threading.Thread.Sleep(100); //dorme um segundo. Se não disparar essa função vai ser mais rápido
                return null;
            });

            try
            {
                scriptRunning.Run(new FileInfo(_scriptTestPtah));
            }
            catch (Exception)
            {
                return fitness;
            }

            sw.Stop();
            
            //Console.WriteLine("Fitness {0} segundos", sw.Elapsed.Seconds);

            int totalTestok = 0;

            if (scriptRunning.GetGlobalVariable("stringData1") != null && "3/5/2015" == scriptRunning["stringData1"].ToString())
                totalTestok += 1;
            if (scriptRunning.GetGlobalVariable("stringData2") != null && "4/5/2015" == scriptRunning["stringData2"].ToString())
                totalTestok += 1;
            if (scriptRunning.GetGlobalVariable("stringData3") != null && "5/5/2015" == scriptRunning["stringData3"].ToString())
                totalTestok += 1;
            if (scriptRunning.GetGlobalVariable("stringData4") != null && "6/5/2015" == scriptRunning["stringData4"].ToString())
                totalTestok += 1;
            if (scriptRunning.GetGlobalVariable("stringData5") != null && "7/5/2015" == scriptRunning["stringData5"].ToString())
                totalTestok += 1;

            if(totalTestok == 5)
                fitness = double.Parse(sw.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));

            #endregion
            
            //Console.WriteLine("Fitness {0}", fitness);
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
