using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Genetic;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using Antlr3.ST;
using Xebic.Parsers.ES3;


namespace ConsoleApplication1
{
    /// <summary>
    /// Console executable program
    /// </summary>
    public class Program
    {
        private static string _jsFile = @"scriptData.js";
        private const string JsFileTest = @"scriptDataTest.js";
        private const string NomeFuncaoOtimizar = "AvancaDias";
        private static DirectoryInfo _dirinfo = null;

        /// <summary>
        /// Main routine
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var tree = Setup();
            ExecutarRodadas(tree, _dirinfo);
        }

        /// <summary>
        /// Faz o setup do Ambiente e das rodadas
        /// </summary>
        private static CommonTree Setup()
        {
            CommonTree tree;

            try
            {
                var text = System.IO.File.ReadAllText(_jsFile);
                _dirinfo = Directory.CreateDirectory(_jsFile.Replace(".js", ""));
                _dirinfo.EnumerateFiles().ToList().ForEach(f => f.Delete()); //clean directory


                //Copia o js principal e o Js de Testes
                //File.Copy(jsFile, Path.Combine(dirinfo.FullName, jsFile), true);
                File.Copy(JsFileTest, Path.Combine(_dirinfo.FullName, JsFileTest), true);

                //macetão
                _jsFile = _jsFile.Replace(".js", "");

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
            int populationSize = 500;
            int generations = 50;
            string executionPath = Environment.CurrentDirectory;

            IFitnessFunction fitness = new JavascriptFitness(executionPath);

            ISelectionMethod metodoSelecao = new EliteSelection();

            Population population = new Population(populationSize, ancestral, fitness, metodoSelecao);

            #endregion

            #region Executa a otimização

            for (int i = 0; i < generations; i++)
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

            foreach (var generationsBestChromosome in population.GenerationsBestChromosomes)
            {
                Console.WriteLine("");
                Console.WriteLine("generation {0} ", generationsBestChromosome.Key);
                Console.WriteLine("Best Founded = " + generationsBestChromosome.Value);
                Console.WriteLine("==========================");
            }

            Console.WriteLine("Press any key...");
            Console.Read();
            #endregion
        }
    } 
}
