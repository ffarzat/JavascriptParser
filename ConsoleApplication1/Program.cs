using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using Xebic.Parsers.ES3;


namespace ConsoleApplication1
{
    class Program
    {
        static int _indent = 0;
        private const string jsFile = @"angular.js";
        private static string _configFile = @"Template.params";

        static List<Funcao> _funcoes = new List<Funcao>();


        static void Main(string[] args)
        {
            try
            {


                string text = System.IO.File.ReadAllText(jsFile);
                string textoConfiguracao = System.IO.File.ReadAllText(_configFile);

                #region processa o js alvo
                var stream = new ANTLRStringStream(text);
                var lexer = new ES3Lexer(stream);
                var tokenStream = new CommonTokenStream(lexer);
                var parser = new ES3Parser(tokenStream);
                ES3Parser.program_return programReturn = parser.program();
                var tree = programReturn.Tree as CommonTree;
                #endregion

                GerarArquivosParaExecucao(tree, textoConfiguracao);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //Console.Write("Press any key to continue...");
            //Console.ReadKey(true); 
        }

        /// <summary>
        /// Função Base para escrever as funções
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="textoConfiguracao"></param>
        private static void GerarArquivosParaExecucao(CommonTree tree, string textoConfiguracao)
        {
            RecuperarFuncoesEParametros(tree);

            var nomeArquivoGramatica = ProcessarGramatica();
            var nomeArquivoConfiguracao = ProcessarConfiguracao(textoConfiguracao, nomeArquivoGramatica);

        }

        /// <summary>
        /// Escreve o arquivo de configuracao
        /// </summary>
        /// <param name="textoConfiguracao"></param>
        /// <param name="nomeArquivoGramatica"></param>
        /// <remarks>
        /// @NomeArquivoGramatica
        /// @NumeroDeFuncoes
        /// </remarks>
        private static string ProcessarConfiguracao(string textoConfiguracao, string nomeArquivoGramatica)
        {
            var arquivo = Environment.CurrentDirectory + @"\" + jsFile + ".params";

            var sw = new StreamWriter(arquivo, false, Encoding.UTF8);

            //@NomeArquivoGramatica
            textoConfiguracao = textoConfiguracao.Replace("@NomeArquivoGramatica", nomeArquivoGramatica);
            //@NumeroDeFuncoes
            textoConfiguracao = textoConfiguracao.Replace("@NumeroDeFuncoes", _funcoes.Count.ToString(CultureInfo.InvariantCulture));

            sw.Write(textoConfiguracao);

            for (int i = 0; i < _funcoes.Count; i++)
            {
                var f = _funcoes[i];
                sw.WriteLine(string.Format("gp.fs.0.func.{0} = {1}", i, f.Nome));
                sw.WriteLine(string.Format("gp.fs.0.func.{0}.nc = nc{1}", i, f.Argumentos.Count));    
            }
            
            sw.Close();

            return arquivo;
        }

        /// <summary>
        /// Escreve o arquivo de gramatica
        /// </summary>
        private static string ProcessarGramatica()
        {
            var arquivo = Environment.CurrentDirectory + @"\" + jsFile + ".grammar";
            var sw = new StreamWriter(arquivo, false, Encoding.UTF8);

            //Declara as funções na gramatica
            foreach (var funcao in _funcoes)
            {
                sw.WriteLine(string.Format("<start> ::= <{0}> ", funcao.Nome));
            }

            //Descreve as funções
            foreach (var funcao in _funcoes)
            {
                if (funcao.Argumentos.Count > 0)
                {
                    sw.Write(string.Format("<{0}> ::= ", funcao.Nome));
                    sw.Write(string.Format("({0} ", funcao.Nome));

                    foreach (var argumento in funcao.Argumentos)
                    {
                        sw.Write(string.Format("({0}) ", argumento.Nome));
                    }

                    sw.Write(")");
                    sw.WriteLine("");
                }
            }

            sw.Close();

            return arquivo;
        }

        /// <summary>
        /// Itera a árvore e escreve no arquivo
        /// </summary>
        /// <param name="tree"></param>
        private static void RecuperarFuncoesEParametros(CommonTree tree) 
        {

            if (tree.Type == 17) //function
            {
                var f = new Funcao();
                
                if (tree.ChildCount > 0)
                {
                    foreach (CommonTree child in tree.Children)
                    {
                        if (child.Type == 148) //Nome da função
                            f.Nome = child.Text;

                        if (child.Type == 111)//ARGS da funcao
                        {
                            if (child.ChildCount > 0)
                            {
                                foreach (CommonTree child2 in child.Children)
                                {
                                    f.AddArgumento(child2.Text);
                                }
                            }
                        }
                    }
                }

                if(!String.IsNullOrEmpty(f.Nome))
                    _funcoes.Add(f);

            }



            

            if (tree.ChildCount > 0)
            {
                foreach (CommonTree child in tree.Children)
                {
                    RecuperarFuncoesEParametros(child);
                }
            }

        } 

        static void WriteLine(string text) 
        { 
            string indentText = String.Empty.PadRight(_indent * 2); 
            Console.WriteLine(indentText + text); 
        } 

        static void IncreaseIndent() 
        { 
            _indent++; 
        } 

        static void DecreaseIndent() 
        { 
            _indent--; 
        }


        //

    } 
}
