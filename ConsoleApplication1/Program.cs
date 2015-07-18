using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using AForge.Genetic;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using Microsoft.Win32;
using Xebic.Parsers.ES3;
using NLog;

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
        private static int TopTargets = 0;
        
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
                Log.WriteLine(string.Format("Erro ao processar a AST do arquivo {0}", JsFile), LogLevel.Error);
            else
                ExecutarRodadas(tree, _dirinfo);


            Environment.Exit(1); //tudo certo!
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
            TopTargets = Convert.ToInt32(ConfigurationManager.AppSettings["Targets"]);

            
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
                //Log.WriteLine(string.Format(gen.ToDot(tree)));
                //Log.WriteLine(string.Format(tree.GetChild(1).ToStringTree()));

                #endregion
               
            }
            catch (Exception ex)
            {
                Log.WriteLine(string.Format(ex.ToString()), LogLevel.Trace);
            }
            
            sw.Stop();
            Log.WriteLine(string.Format("AST do arquivo {1} construída em {0} milisegundos", sw.Elapsed.ToString("mm\\:ss\\.ff"), JsFile), LogLevel.Info);

            return tree;
        }

        /// <summary>
        /// Gera a pasta de saída e inicia o processo de otimização
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="directoryInfo"></param>
        private static void ExecutarRodadas(CommonTree tree, DirectoryInfo directoryInfo)
        {
            var funcoesAlvo = DefinirFuncoesAlvo(tree).ToList();
            string names = "";
            
            funcoesAlvo.Take(TopTargets).ToList().ForEach(f => names += "|" + f.ToString(CultureInfo.InvariantCulture));

            TopTargets = TopTargets == 0 ? funcoesAlvo.Count() : TopTargets;

            Log.WriteLine(string.Format("Quantidade de Funções para Otmizar (corte): {0}", TopTargets), LogLevel.Info);
            Log.WriteLine(string.Format("Alvo(s) da otimização: {0}", names), LogLevel.Info);

            Log.WriteLine(string.Format("================================================================================"), LogLevel.Info);
            Log.WriteLine(string.Format("Biblioteca {0}", JsFile), LogLevel.Info);
            Log.WriteLine(string.Format("Testes {0}", JsFileTest), LogLevel.Info);

            Log.WriteLine(string.Format("Qunit {0}", QunitFile), LogLevel.Info);

            Log.WriteLine(string.Format("População {0}", PopulationSize), LogLevel.Info);
            Log.WriteLine(string.Format("Gerações {0}", Generations), LogLevel.Info);
            Log.WriteLine(string.Format("Paralelismo {0}", Parallelism), LogLevel.Info);
            Log.WriteLine(string.Format("TimeOut para Fitness {0}", EvaluateTimeOutMinutes), LogLevel.Info);
            Log.WriteLine(string.Format("================================================================================"), LogLevel.Info);

            const int countFunctions = 0;

            foreach (var nomeFuncaoTarget in funcoesAlvo)
            {
                if (countFunctions >= TopTargets)
                    break;

                Log.WriteLine(string.Format("Executando otimização da função '{0}'", nomeFuncaoTarget), LogLevel.Info);
                Log.WriteLine(string.Format("================================================================================"), LogLevel.Info);

                var sw = new Stopwatch();
                var swEpoch = new Stopwatch();
                sw.Start();

                #region Encontra as função alvo da otimização, recupera o bloco de instruções
                var funcaoOtimizar = JavascriptAstCodeGenerator.GetFunctionTree(nomeFuncaoTarget);

                if (funcaoOtimizar == null)
                {
                    Log.WriteLine(string.Format("Função não encontrada: {0}", nomeFuncaoTarget), LogLevel.Error);
                    break;
                }

                #endregion

                #region Monta o primeiro individuo

                var ancestral = new JavascriptChromosome(tree, nomeFuncaoTarget);

                #endregion

                #region Faz o setup da população inicial
                IFitnessFunction fitness = new JavascriptFitness(ancestral, ExecutionPath, JsFileTest, QunitFile);

                ISelectionMethod metodoSelecao = new EliteSelection();

                Population population = new Population(PopulationSize, ancestral, fitness, metodoSelecao, Parallelism, EvaluateTimeOutMinutes);

                #endregion

                sw.Stop();
                Log.WriteLine(string.Format("Setup da população em {0}", sw.Elapsed.ToString("mm\\:ss\\.ff")), LogLevel.Info);

                sw.Reset();
                sw.Start();

                #region Executa a otimização

                for (int i = 0; i < Generations; i++)
                {
                    swEpoch.Reset();
                    swEpoch.Start();
                    Log.WriteLine(string.Format("--------------------------"), LogLevel.Info);
                    Log.WriteLine(string.Format("Processando geração {0}... ", i + 1), LogLevel.Info);
                    population.RunEpoch();
                    swEpoch.Stop();
                    Log.WriteLine(string.Format("Best Fit {0}", population.FitnessMin), LogLevel.Info);
                    //Log.WriteLine(string.Format("Best Fit {0}", population.BestChromosome.ToString().Replace("\r\n", "")));
                    Log.WriteLine(string.Format("{0} minutos", swEpoch.Elapsed.ToString("mm\\:ss\\.ff")), LogLevel.Info);
                    //Log.WriteLine(string.Format("--------------------------"));
                }

                #endregion

                sw.Stop();
                Log.WriteLine(string.Format("Processo executado em {0}", sw.Elapsed.ToString("mm\\:ss\\.ff")), LogLevel.Info);

                #region Results
                Log.WriteLine(string.Format("============================= "), LogLevel.Info);
                Log.WriteLine(string.Format("Min = " + population.FitnessMin), LogLevel.Info);
                Log.WriteLine(string.Format("Sum = " + population.FitnessSum), LogLevel.Info);
                Log.WriteLine(string.Format("Avg = " + population.FitnessAvg), LogLevel.Info);
                Log.WriteLine(string.Format("Best= " + population.BestChromosome.Id), LogLevel.Info);
                Log.WriteLine(string.Format(population.BestChromosome.ToString()), LogLevel.Trace);


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
            CommonTree _tree = JavascriptAstCodeGenerator.DeepClone(tree);

            var functions = JavascriptAstCodeGenerator.BuildFunctionList(_tree); // just to build up a function list // TODO: refactoring to not expose this

            if (string.IsNullOrEmpty(TargetFunction))
            {
                var functionsMostUseds = JavascriptAstCodeGenerator.FindTopUsedFunctions(_tree);
                var functionsWithCallsAndLocs = JavascriptAstCodeGenerator.FindTopLocFunctions(functionsMostUseds);
                return functionsWithCallsAndLocs;
            }

            return TargetFunction.Split("|".ToCharArray()); //caso contrário retorna do App.config
        }
    } 
}
