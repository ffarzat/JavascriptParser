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
    public class Program
    {
        private static string jsFile = @"scriptData.js";
        private static string jsFileTest = @"scriptDataTest.js";
        private static string _nomeFuncaoOtimizar = "AvancaDias";

        static void Main(string[] args)
        {
            try
            {
                var text = System.IO.File.ReadAllText(jsFile);
                var dirinfo = Directory.CreateDirectory(jsFile.Replace(".js", ""));

                //Copia o js principal e o Js de Testes
                //File.Copy(jsFile, Path.Combine(dirinfo.FullName, jsFile), true);
                File.Copy(jsFileTest, Path.Combine(dirinfo.FullName, jsFileTest), true);

                //macetão
                jsFile = jsFile.Replace(".js", "");

                #region Gera a AST do javascript origem
                var stream = new ANTLRStringStream(text);
                var lexer = new ES3Lexer(stream);
                var tokenStream = new CommonTokenStream(lexer);
                var parser = new ES3Parser(tokenStream);
                ES3Parser.program_return programReturn = parser.program();
                var tree = programReturn.Tree as CommonTree;
                

                //var gen = new DotTreeGenerator();
                //Console.Write(gen.ToDot(tree));
                Console.Write(tree.GetChild(1).ToStringTree()); //TODO: traduzir o Tostring em código...
                
                #endregion

                GerarArquivosParaExecucao(tree, dirinfo);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //Console.Write("Press any key to continue...");
            //Console.ReadKey(true); 
        }

        /// <summary>
        /// Gera a pasta de saída e inicia o processo de otimização
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="directoryInfo"></param>
        private static void GerarArquivosParaExecucao(CommonTree tree, DirectoryInfo directoryInfo)
        {
            #region Encontra a função alvo da otimização, recupera o bloco de instruções
            var funcaoOtimizar = RecuperarNoDaFuncao(tree, _nomeFuncaoOtimizar);

            if (funcaoOtimizar == null)
                throw new ApplicationException(String.Format("Função não encontrada: {0}", _nomeFuncaoOtimizar));

            #endregion

            #region Monta o primeiro individuo

            var ancestral = new JavascriptChromosome(funcaoOtimizar);
            
            #endregion

            #region Faz o setup da população inicial
            int populationSize = 40;
            int generations = 50;
            
            IFitnessFunction fitness = new JavascriptFitness();

            ISelectionMethod metodoSelecao = new EliteSelection();

            Population population = new Population(populationSize, ancestral, fitness, metodoSelecao);

            #endregion

            #region Executa a otimização

            for (int i = 0; i < generations; i++)
            {
                population.RunEpoch(); //executa uma iteração??
                Console.WriteLine(population.BestChromosome.ToString());
            }
            
            #endregion

            #region Exporta os resultados

            #endregion

        } 

        /// <summary>
        /// Descobre qual a função na Tree pelo nome
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="nomeFuncaoOtimizar"></param>
        /// <returns></returns>
        public static ITree RecuperarNoDaFuncao(CommonTree tree, string nomeFuncaoOtimizar)
        {

            for (int i = 0; i < tree.ChildCount; i++)
            {
                var funcaoAtual = tree.GetChild(i);
                var nome = funcaoAtual.GetChild(0) == null ? "" : funcaoAtual.GetChild(0).Text;

                if (nome == nomeFuncaoOtimizar)
                    return funcaoAtual;
            }

            return null;
        }

    } 
}
