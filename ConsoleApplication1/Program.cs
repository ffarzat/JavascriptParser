using System;
using System.IO;
using System.Linq;
using AForge.Genetic;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using Xebic.Parsers.ES3;


namespace ConsoleApplication1
{
    /// <summary>
    /// Console executable program
    /// </summary>
    public class Program
    {
        private const string JsFile = @"scriptData.js";
        private const string JsFileTest = @"scriptDataTest.js";
        private const string NomeFuncaoOtimizar = "AvancaDias";
        private static DirectoryInfo _dirinfo = null;

        private const int PopulationSize = 100;
        private const int Generations = 50;
        private static readonly string ExecutionPath = Environment.CurrentDirectory;


        /// <summary>
        /// Main routine
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var tree = Setup();

            if (tree == null)
                Console.WriteLine("Erro ao processar a AST do arquivo {0}", JsFile);
            else
                ExecutarRodadas(tree, _dirinfo);


            Console.WriteLine("Aperte qualquer tecla para encerrar...");
            Console.Read();

        }

        /// <summary>
        /// Faz o setup do Ambiente e das rodadas
        /// </summary>
        private static CommonTree Setup()
        {
            CommonTree tree = null;

            try
            {
                var text = System.IO.File.ReadAllText(JsFile);
                #region Gera a AST do javascript origem

                var stream = new ANTLRStringStream(text);
                var lexer = new ES3Lexer(stream);
                var tokenStream = new CommonTokenStream(lexer);
                var parser = new ES3Parser(tokenStream);
                ES3Parser.program_return programReturn = parser.program();
                tree = programReturn.Tree as CommonTree;


                //var gen = new DotTreeGenerator();
                //Console.Write(gen.ToDot(tree));
                //Console.Write(tree.GetChild(1).ToStringTree());

                #endregion
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return tree;
        }

        /// <summary>
        /// Gera a pasta de saída e inicia o processo de otimização
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="directoryInfo"></param>
        private static void ExecutarRodadas(CommonTree tree, DirectoryInfo directoryInfo)
        {
            #region Encontra a função alvo da otimização, recupera o bloco de instruções
            var funcaoOtimizar = JavascriptAstCodeGenerator.FindFunctionTree(tree, NomeFuncaoOtimizar);

            if (funcaoOtimizar == null)
                throw new ApplicationException(String.Format("Função não encontrada: {0}", NomeFuncaoOtimizar));

            #endregion

            #region Monta o primeiro individuo

            var ancestral = new JavascriptChromosome(tree, NomeFuncaoOtimizar);
            
            #endregion

            #region Faz o setup da população inicial
            IFitnessFunction fitness = new JavascriptFitness(ExecutionPath, JsFileTest);

            ISelectionMethod metodoSelecao = new EliteSelection();

            Population population = new Population(PopulationSize, ancestral, fitness, metodoSelecao);

            #endregion

            #region Executa a otimização

            for (int i = 0; i < Generations; i++)
            {
                Console.WriteLine("Processing generation {0}... ", i);
                population.RunEpoch();
                Console.WriteLine("--------------------------");
            }
            
            #endregion

            #region Results

            Console.WriteLine("Max = " + population.FitnessMax);
            Console.WriteLine("Sum = " + population.FitnessSum);
            Console.WriteLine("Avg = " + population.FitnessAvg);
            Console.WriteLine("Best= " + population.BestChromosome.Id);
            Console.WriteLine(population.BestChromosome.ToString());

            //foreach (var generationsBestChromosome in population.GenerationsBestChromosomes)
            //{
            //    Console.WriteLine("");
            //    Console.WriteLine("generation {0} ", generationsBestChromosome.Key);
            //    Console.WriteLine("Best Founded = " + generationsBestChromosome.Value);
            //    Console.WriteLine("==========================");
            //}
            #endregion
        }
    } 
}
