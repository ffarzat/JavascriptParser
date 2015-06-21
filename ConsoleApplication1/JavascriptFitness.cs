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
            double fitness = 0.0;
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

            try
            {
                var codeGenerator = new JavascriptAstCodeGenerator(((JavascriptChromosome)chromosome).Tree);
                string generatedJsCode = codeGenerator.DoCodeTransformation();
                File.WriteAllText(fileName, generatedJsCode);
                var compiledJs = scriptRunning.Compile(generatedJsCode);
            }
            catch (Exception)
            {
                fitness = double.MaxValue; //Se é inválido penalizo lá no céu
                //TODO: logar a exceção para tratamento do gerador de código
            }
            
            #endregion
            
            #region Executar os testes no novo Js (medindo tempo)
            scriptRunning.AllowDirectAccess = true;
            scriptRunning.Load(fileName);
           
            /*Meu macete pessoal*/
            scriptRunning["print"] = new NativeFunctionObject("print", (ctx, owner, args) =>
            {
                //Console.WriteLine(args[0]);
                System.Threading.Thread.Sleep(1000); //dorme um segundo. Se não disparar essa função vai ser mais rápido
                return null;
            });

            var sw = new Stopwatch();
            sw.Start();
            
            scriptRunning.Run(new FileInfo(_scriptTestPtah));
            
            sw.Stop();

            int totalTestok = 0;

            if ("3/5/2015" == (string)scriptRunning["stringData1"])
                totalTestok += 1;
            if ("4/5/2015" == (string)scriptRunning["stringData2"])
                totalTestok += 1;
            if ("5/5/2015" == (string)scriptRunning["stringData3"])
                totalTestok += 1;
            if ("6/5/2015" == (string)scriptRunning["stringData4"])
                totalTestok += 1;
            if ("7/5/2015" == (string)scriptRunning["stringData5"])
                totalTestok += 1;

            fitness = (sw.ElapsedMilliseconds + totalTestok);

            #endregion



            //Penalizar o tempo x Qtd de Testes passando

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
