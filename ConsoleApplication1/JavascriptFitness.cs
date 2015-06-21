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
        /// Do the setup for a future execution
        /// </summary>
        public JavascriptFitness(string pathToExecution)
        {
            _pathToExecution = pathToExecution;
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

            #region setup a directory for this individual?
            var createNewDirectoryForGeneration = dirOfRun.GetDirectories().FirstOrDefault(d => d.Name == chromosome.GenerationId.ToString(CultureInfo.InvariantCulture)) == null;

            if (createNewDirectoryForGeneration)
                dirOfRun.CreateSubdirectory(chromosome.GenerationId.ToString(CultureInfo.InvariantCulture));

            directoryForIndividual =
                dirOfRun.GetDirectories()
                        .FirstOrDefault(d => d.Name == chromosome.GenerationId.ToString(CultureInfo.InvariantCulture));

            #endregion

            #region gerar o código em um diretório temporário
            string fileName = string.Format("{0}/{1}.js", directoryForIndividual.FullName, chromosome.Id);

            try
            {
                var codeGenerator = new JavascriptAstCodeGenerator(((JavascriptChromosome)chromosome).Tree);
                string generatedJsCode = codeGenerator.DoCodeTransformation();
                File.WriteAllText(fileName, generatedJsCode);
                var scriptRunning = new ScriptRunningMachine();
                var compiledJs = scriptRunning.Compile(generatedJsCode);
            }
            catch (Exception)
            {
                fitness = double.MaxValue; //Se é inválido penalizo lá no céu
                //TODO: logar a exceção para tratamento do gerador de código
            }
            
            #endregion

            //TODO: copiar os testes para lá
            //TODO: Executar os testes no novo Js
            //TODO: Medir o tempo de execução

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
