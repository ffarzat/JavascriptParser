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
        private static string jsFile = @"angular.js";
        private static string _configFile = @"Template.params";
        private static string _javaFile = @"Template.java";
        private static string _package = "ec.app.@package";

        static List<Funcao> _funcoes = new List<Funcao>();

        static List<String> _argumentos = new List<String>();

        static void Main(string[] args)
        {
            try
            {


                string text = System.IO.File.ReadAllText(jsFile);
                string textoConfiguracao = System.IO.File.ReadAllText(_configFile);
                string textoJava = System.IO.File.ReadAllText(_javaFile);
                
                //macetão
                jsFile = jsFile.Replace(".", "");
                _package = _package.Replace("@package", jsFile);

                #region processa o js alvo
                var stream = new ANTLRStringStream(text);
                var lexer = new ES3Lexer(stream);
                var tokenStream = new CommonTokenStream(lexer);
                var parser = new ES3Parser(tokenStream);
                ES3Parser.program_return programReturn = parser.program();
                var tree = programReturn.Tree as CommonTree;
                #endregion

                GerarArquivosParaExecucao(tree, textoConfiguracao, textoJava);
                
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
        /// <param name="textoJava"></param>
        private static void GerarArquivosParaExecucao(CommonTree tree, string textoConfiguracao, string textoJava)
        {
            RecuperarFuncoesEParametros(tree);

            var dirinfo = Directory.CreateDirectory(jsFile);

            var nomeArquivoGramatica = ProcessarGramatica(dirinfo);
            var nomeArquivoConfiguracao = ProcessarConfiguracao(dirinfo, textoConfiguracao, nomeArquivoGramatica);

            ProcessarArquivosDeNo(dirinfo, textoJava);

        }

        /// <summary>
        /// Escreve o arquivo java que representa a execução da Função no ECJ
        /// </summary>
        /// <param name="dirinfo"></param>
        /// <param name="textoJava"></param>
        private static void ProcessarArquivosDeNo(DirectoryInfo dirinfo, string textoJava)
        {


            for (int i = 0; i < _funcoes.Count; i++)
            {
                var f = _funcoes[i];

                var arquivo = dirinfo.FullName + @"\" + f.Nome + ".java";

                var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

                string textoClasse = textoJava;

                //@package
                textoClasse = textoClasse.Replace("@package", jsFile);
                //@NomeFuncao
                textoClasse = textoClasse.Replace("@NomeFuncao", f.Nome);

                sw.Write(textoClasse);

                //sw.WriteLine(string.Format("gp.fs.0.func.{0} = {1}", i, f.Nome));
                

                sw.Close();
            }

            for (int i = 0; i < _argumentos.Count; i++)
            {
                var argumento = _argumentos[i];

                var arquivo = dirinfo.FullName + @"\" + argumento + ".java";

                var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

                string textoClasse = textoJava;

                //@package
                textoClasse = textoClasse.Replace("@package", jsFile);
                //@NomeFuncao
                textoClasse = textoClasse.Replace("@NomeFuncao", argumento);

                sw.Write(textoClasse);

                //sw.WriteLine(string.Format("gp.fs.0.func.{0} = {1}", i, f.Nome));


                sw.Close();
            }

            
        }

        /// <summary>
        /// Escreve o arquivo de configuracao
        /// </summary>
        /// <param name="configuracao"></param>
        /// <param name="textoConfiguracao"></param>
        /// <param name="nomeArquivoGramatica"></param>
        /// <remarks>
        /// @NomeArquivoGramatica
        /// @NumeroDeFuncoes
        /// </remarks>
        private static string ProcessarConfiguracao(DirectoryInfo configuracao, string textoConfiguracao, string nomeArquivoGramatica)
        {
            var arquivo = configuracao.FullName + @"\" + jsFile + ".params";

            var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

            //@NomeArquivoGramatica
            textoConfiguracao = textoConfiguracao.Replace("@NomeArquivoGramatica", nomeArquivoGramatica);
            //@NumeroDeFuncoes
            int total = _funcoes.Count + _argumentos.Count;
            textoConfiguracao = textoConfiguracao.Replace("@NumeroDeFuncoes", total.ToString(CultureInfo.InvariantCulture));

            sw.Write(textoConfiguracao);

            for (int i = 0; i < _funcoes.Count; i++)
            {
                var f = _funcoes[i];
                sw.WriteLine(string.Format("gp.fs.0.func.{0} = {1}", i, _package + "." + f.Nome));
                sw.WriteLine(string.Format("gp.fs.0.func.{0}.nc = nc{1}", i, f.Argumentos.Count));    
            }

            for (int i = 0; i < _argumentos.Count; i++)
            {
                var argumento = _argumentos[i];
                sw.WriteLine(string.Format("gp.fs.0.func.{0} = {1}", _funcoes.Count + i, _package + "." + argumento));
                sw.WriteLine(string.Format("gp.fs.0.func.{0}.nc = nc{1}", _funcoes.Count + i, 0));
            }
            
            sw.Close();

            return arquivo;
        }

        /// <summary>
        /// Escreve o arquivo de gramatica
        /// </summary>
        /// <param name="dirinfo"></param>
        private static string ProcessarGramatica(DirectoryInfo dirinfo)
        {
            var arquivo = dirinfo.FullName + @"\" + jsFile + ".grammar";
            var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

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
                    sw.Write(string.Format("({0}", funcao.Nome));

                    foreach (var argumento in funcao.Argumentos)
                    {
                        sw.Write(string.Format("<{0}> ", argumento.Nome));
                    }

                    sw.Write(")");
                    sw.WriteLine("");
                }
            }

            //Descreve os argumentos
            foreach (var argumento in _argumentos)
            {
                sw.Write(string.Format("<{0}> ::= ", argumento));
                sw.Write(string.Format("({0}", argumento));
                sw.Write(")");
                sw.WriteLine("");
            }


            sw.Close();

            return Path.GetFileName(arquivo);
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
                                    if(!_argumentos.Contains(child2.Text))
                                        _argumentos.Add(child2.Text);
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
