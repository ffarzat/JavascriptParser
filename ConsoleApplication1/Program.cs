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
        private static string jsFile = @"scriptData.js";
        private static string jsFileTest = @"scriptDataTest.js";
        private static string _configFile = @"Template.params";
        private static string _javaFile = @"Template.java";
        private static string _package = "ec.app.@package";
        private static string _javaProblemFile = @"Problem.java";
        private static string _nomeFuncaoOtimizar = "AvancaDias";

        static void Main(string[] args)
        {
            try
            {


                string text = System.IO.File.ReadAllText(jsFile);
                string textoConfiguracao = System.IO.File.ReadAllText(_configFile);
                string textoJava = System.IO.File.ReadAllText(_javaFile);
                string textoProblema = System.IO.File.ReadAllText(_javaProblemFile);



                var dirinfo = Directory.CreateDirectory(jsFile.Replace(".", ""));

                //Copia o js principal e o Js de Testes
                File.Copy(jsFile, Path.Combine(dirinfo.FullName, jsFile), true);
                File.Copy(jsFileTest, Path.Combine(dirinfo.FullName, jsFileTest), true);

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

                GerarArquivosParaExecucao(tree, textoConfiguracao, textoJava, textoProblema);
                
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
        /// <param name="textoProblema"></param>
        private static void GerarArquivosParaExecucao(CommonTree tree, string textoConfiguracao, string textoJava, string textoProblema)
        {
            var dirinfo = new DirectoryInfo(jsFile);
            
            var nomeArquivoGramatica = ProcessarGramatica(dirinfo, tree);
            //var nomeProblema = ProcessarProblema(dirinfo, textoProblema);
            //var nomeArquivoConfiguracao = ProcessarConfiguracao(dirinfo, textoConfiguracao, nomeArquivoGramatica, nomeProblema);
            //ProcessarArquivosDeNo(dirinfo, textoJava);
            

        }

        /// <summary>
        /// Gera o .Java do problema
        /// </summary>
        /// <param name="dirinfo"></param>
        /// <param name="textoProblema"></param>
        /// <returns></returns>
        //private static string ProcessarProblema(DirectoryInfo dirinfo, string textoProblema)
        //{
        //    var arquivo = dirinfo.FullName + @"\Problem.java";

        //    string textoClasse = textoProblema;

        //    //@package
        //    textoClasse = textoClasse.Replace("@package", jsFile);

        //    var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));
        //    sw.Write(textoClasse);
        //    sw.Close();

        //    return "ec.app." + jsFile + ".Problem";
        //}

        /// <summary>
        /// Escreve o arquivo java que representa a execução da Função no ECJ
        /// </summary>
        /// <param name="dirinfo"></param>
        /// <param name="textoJava"></param>
        //private static void ProcessarArquivosDeNo(DirectoryInfo dirinfo, string textoJava)
        //{


        //    for (int i = 0; i < _funcoes.Count; i++)
        //    {
        //        var f = _funcoes[i];

        //        var arquivo = dirinfo.FullName + @"\" + f.Nome + ".java";

        //        var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

        //        string textoClasse = textoJava;

        //        //@package
        //        textoClasse = textoClasse.Replace("@package", jsFile);
        //        //@NomeFuncao
        //        textoClasse = textoClasse.Replace("@NomeFuncao", f.Nome);

        //        sw.Write(textoClasse);

        //        //sw.WriteLine(string.Format("gp.fs.0.func.{0} = {1}", i, f.Nome));
                

        //        sw.Close();
        //    }

        //    for (int i = 0; i < _argumentos.Count; i++)
        //    {
        //        var argumento = _argumentos[i];

        //        var arquivo = dirinfo.FullName + @"\" + argumento + ".java";

        //        var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

        //        string textoClasse = textoJava;

        //        //@package
        //        textoClasse = textoClasse.Replace("@package", jsFile);
        //        //@NomeFuncao
        //        textoClasse = textoClasse.Replace("@NomeFuncao", argumento);

        //        sw.Write(textoClasse);

        //        //sw.WriteLine(string.Format("gp.fs.0.func.{0} = {1}", i, f.Nome));


        //        sw.Close();
        //    }

            
        //}

        /// <summary>
        /// Escreve o arquivo de configuracao
        /// </summary>
        /// <param name="configuracao"></param>
        /// <param name="textoConfiguracao"></param>
        /// <param name="nomeArquivoGramatica"></param>
        /// <param name="nomeProblema"></param>
        /// <remarks>
        /// @NomeArquivoGramatica
        /// @NumeroDeFuncoes
        /// </remarks>
        //private static string ProcessarConfiguracao(DirectoryInfo configuracao, string textoConfiguracao, string nomeArquivoGramatica, string nomeProblema)
        //{
        //    var arquivo = configuracao.FullName + @"\" + jsFile + ".params";

        //    var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

        //    //@NomeArquivoGramatica
        //    textoConfiguracao = textoConfiguracao.Replace("@NomeArquivoGramatica", nomeArquivoGramatica);
        //    //@NumeroDeFuncoes
        //    int total = _funcoes.Count + _argumentos.Count;
        //    textoConfiguracao = textoConfiguracao.Replace("@NumeroDeFuncoes", total.ToString(CultureInfo.InvariantCulture));
        //    //@Problema
        //    textoConfiguracao = textoConfiguracao.Replace("@Problema", nomeProblema);
            


        //    sw.Write(textoConfiguracao);

        //    for (int i = 0; i < _funcoes.Count; i++)
        //    {
        //        var f = _funcoes[i];
        //        sw.WriteLine(string.Format("gp.fs.0.func.{0} = {1}", i, _package + "." + f.Nome));
        //        sw.WriteLine(string.Format("gp.fs.0.func.{0}.nc = nc{1}", i, f.Argumentos.Count));    
        //    }

        //    for (int i = 0; i < _argumentos.Count; i++)
        //    {
        //        var argumento = _argumentos[i];
        //        sw.WriteLine(string.Format("gp.fs.0.func.{0} = {1}", _funcoes.Count + i, _package + "." + argumento));
        //        sw.WriteLine(string.Format("gp.fs.0.func.{0}.nc = nc{1}", _funcoes.Count + i, 0));
        //    }
            
        //    sw.Close();

        //    return arquivo;
        //}

        /// <summary>
        /// Escreve o arquivo de gramatica
        /// </summary>
        /// <param name="dirinfo"></param>
        /// <param name="tree"></param>
        private static string ProcessarGramatica(DirectoryInfo dirinfo, CommonTree tree)
        {

            //ECJ com gramatica tem problema para processar sobrecarga de método... :-/

            int i = 0;

            var arquivo = dirinfo.FullName + @"\" + jsFile + ".grammar";
            var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

            //cada function agora é uma instrução, uma linha de código
            var funcoesPrimeiroNivel = new List<string>();

            var funcaoOtimizar = tree.GetChild(0).GetChild(2); //TODO: buscar pelo nome (e pegar o Block)

            //Descreve as funções
            for (int j = 0; j < funcaoOtimizar.ChildCount; j++)
            {
                var funcao = funcaoOtimizar.GetChild(j);

                sw.Write(string.Format("<{0}> ::= ", funcao.Text));
                sw.Write(string.Format("({0} ", funcao.Text));

                for (int k = 0; k < funcao.ChildCount; k++)
                {
                    var argumento = funcao.GetChild(k);

                    sw.Write(string.Format("<{0}> ", argumento.Text));
                }

                sw.Write(")");
                sw.WriteLine("");
                //}
            }

            ////Descreve os argumentos
            //foreach (var argumento in _argumentos)
            //{
            //    sw.Write(string.Format("<{0}> ::= ", argumento));
            //    sw.Write(string.Format("({0}", argumento));
            //    sw.Write(")");
            //    sw.WriteLine("");
                
            //}

            
            sw.Close();
            //i++;
            //Console.WriteLine(i);
            //Console.Read();

            return Path.GetFileName(arquivo);
        }


        //

    } 
}
