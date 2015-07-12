using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using AForge.Genetic;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using Microsoft.Win32;
using Xebic.Parsers.ES3;


namespace ConsoleApplication1
{
    /// <summary>
    /// Console executable program
    /// </summary>
    public class Program
    {
        private static string JsFile = "";
        private static string JsFileTest = ""; 
        private static string TargetFunction = ""; 
        private static string QunitFile = "";
        private static int PopulationSize = 0;
        private static int Generations = 0;
        private static bool Parallelism = false;
        private static int EvaluateTimeOutMinutes = 0;
        
        private static DirectoryInfo _dirinfo = null;
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

            JsFile = ConfigurationManager.AppSettings["JsFile"];
            JsFileTest = ConfigurationManager.AppSettings["JsFileTest"];
            TargetFunction = ConfigurationManager.AppSettings["TargetFunction"];
            QunitFile = ConfigurationManager.AppSettings["QunitFile"];
            PopulationSize = Convert.ToInt32(ConfigurationManager.AppSettings["PopulationSize"]);
            Generations = Convert.ToInt32(ConfigurationManager.AppSettings["Generations"]);
            Parallelism = Convert.ToBoolean(ConfigurationManager.AppSettings["Parallelism"]);
            EvaluateTimeOutMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["EvaluateTimeOutMinutes"]);

            Console.WriteLine("Alvo(s) da otimização: {0}", TargetFunction);

            var sw = new Stopwatch();
            sw.Start();
            CommonTree tree = null;

            try
            {
                var text = System.IO.File.ReadAllText(JsFile);
                var dirOfRun = new DirectoryInfo(Environment.CurrentDirectory + "/" + DateTime.Now.ToString("yyyy_MM_dd"));

                #region Clean directories and Files
                if (dirOfRun.Exists)
                {
                    dirOfRun.EnumerateDirectories()
                            .ToList()
                            .ForEach(d => d.EnumerateFiles().ToList().ForEach(f => f.Delete()));

                    dirOfRun.EnumerateDirectories()
                            .ToList()
                            .ForEach(d => d.Delete());
                }
                #endregion

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
            
            sw.Stop();
            Console.WriteLine("AST do arquivo {1} construída em {0} milisegundos", sw.Elapsed.ToString("mm\\:ss\\.ff"), JsFile);

            return tree;
        }

        /// <summary>
        /// Gera a pasta de saída e inicia o processo de otimização
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="directoryInfo"></param>
        private static void ExecutarRodadas(CommonTree tree, DirectoryInfo directoryInfo)
        {
            var funcoesAlvo = DefinirFuncoesAlvo(tree);

            Console.WriteLine("================================================================================");
            Console.WriteLine("Biblioteca {0}", JsFile);
            Console.WriteLine("Testes {0}", JsFileTest);
            
            Console.WriteLine("Qunit {0}", QunitFile);

            Console.WriteLine("População {0}", PopulationSize);
            Console.WriteLine("Gerações {0}", Generations);
            Console.WriteLine("Paralelismo {0}", Parallelism);
            Console.WriteLine("TimeOut para Fitness {0}", EvaluateTimeOutMinutes);

            
            foreach (var nomeFuncaoTarget in funcoesAlvo)
            {
                Console.WriteLine("Executando otimização da função '{0}'", nomeFuncaoTarget);

                var sw = new Stopwatch();
                var swEpoch = new Stopwatch();
                sw.Start();

                #region Encontra as função alvo da otimização, recupera o bloco de instruções
                var funcaoOtimizar = JavascriptAstCodeGenerator.FindFunctionTree(tree, nomeFuncaoTarget);

                if (funcaoOtimizar == null)
                {
                    Console.WriteLine("Função não encontrada: {0}", nomeFuncaoTarget);
                    break;
                }

                #endregion

                #region Monta o primeiro individuo

                var ancestral = new JavascriptChromosome(tree, nomeFuncaoTarget);

                #endregion

                #region Faz o setup da população inicial
                IFitnessFunction fitness = new JavascriptFitness(ExecutionPath, JsFileTest, QunitFile);

                ISelectionMethod metodoSelecao = new EliteSelection();

                Population population = new Population(PopulationSize, ancestral, fitness, metodoSelecao, Parallelism, EvaluateTimeOutMinutes);

                #endregion

                sw.Stop();
                Console.WriteLine("Setup da população em {0}", sw.Elapsed.ToString("mm\\:ss\\.ff"));

                sw.Reset();
                sw.Start();

                #region Executa a otimização

                for (int i = 0; i < Generations; i++)
                {
                    swEpoch.Reset();
                    swEpoch.Start();
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Processando geração {0}... ", i + 1);
                    population.RunEpoch();
                    swEpoch.Stop();
                    Console.WriteLine("Best Fit {0}", population.FitnessMin);
                    //Console.WriteLine("Best Fit {0}", population.BestChromosome.ToString().Replace("\r\n", ""));
                    Console.WriteLine("{0} minutos", swEpoch.Elapsed.ToString("mm\\:ss\\.ff"));
                    //Console.WriteLine("--------------------------");
                }

                #endregion

                sw.Stop();
                Console.WriteLine("Processo executado em {0}", sw.Elapsed.ToString("mm\\:ss\\.ff"));

                #region Results
                Console.WriteLine("============================= ");
                Console.WriteLine("Max = " + population.FitnessMin);
                Console.WriteLine("Sum = " + population.FitnessSum);
                Console.WriteLine("Avg = " + population.FitnessAvg);
                Console.WriteLine("Best= " + population.BestChromosome.Id);
                Console.WriteLine(population.BestChromosome.ToString());


                var nppDir = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Notepad++", null, null);
                var nppExePath = Path.Combine(nppDir + "", "Notepad++.exe");
                var nppReadmePath = population.BestChromosome.File;
                var line = 20;
                var sb = new StringBuilder();
                sb.AppendFormat("\"{0}\" -n{1}", nppReadmePath, line);

                if (File.Exists(nppExePath))
                    Process.Start(nppExePath, sb.ToString());

                #endregion
            }

            

           
        }

        /// <summary>
        /// Retorna o conjunto de funções alvo da otimização
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<string> DefinirFuncoesAlvo(CommonTree tree)
        {
           var functions = JavascriptAstCodeGenerator.BuildFunctionList(tree);

            if (string.IsNullOrEmpty(TargetFunction))
            {
                var functionsMostUseds = JavascriptAstCodeGenerator.FindTopUsedFunctions(tree);
             
                return
                    functionsMostUseds.Where(f => functions.Count(tree1 => tree1.GetChild(0).Text == f.Key) > 0)
                                      .Select(x => x.Key);
            }

            return TargetFunction.Split("|".ToCharArray()); //caso contrário retorna do App.config
        }
    } 
}
