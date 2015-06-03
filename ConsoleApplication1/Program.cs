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
        private static string jsFile = @"scriptData.js";
        private static string jsFileTest = @"scriptDataTest.js";
        private static string _configFile = @"Template.params";
        private static string _javaFile = @"Template.java";
        private static string _package = "ec.app.@package";
        private static string _javaProblemFile = @"Problem.java";
        private static string _nomeFuncaoOtimizar = "AvancaDias";

        private static List<Funcao> _funcoes = new List<Funcao>(); 
        private static List<Argumento> _argumentos = new List<Argumento>(); 

        static void Main(string[] args)
        {
            try
            {
                var text = System.IO.File.ReadAllText(jsFile);
                var textoConfiguracao = System.IO.File.ReadAllText(_configFile);
                var textoJava = System.IO.File.ReadAllText(_javaFile);
                var textoProblema = System.IO.File.ReadAllText(_javaProblemFile);

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
            RecuperarFuncoesEParametros(tree);

            var dirinfo = new DirectoryInfo(jsFile);
            
            var nomeArquivoGramatica = ProcessarGramatica(dirinfo);
            var nomeProblema = ProcessarProblema(dirinfo, textoProblema);
            var nomeArquivoConfiguracao = ProcessarConfiguracao(dirinfo, textoConfiguracao, nomeArquivoGramatica, nomeProblema);
            ProcessarArquivosDeNo(dirinfo, textoJava);
            

        } 


         /// <summary>
        /// Itera a árvore e escreve no arquivo
        /// </summary>
        /// <param name="tree"></param>
        private static void RecuperarFuncoesEParametros(CommonTree tree) 
        {

            var funcaoOtimizar = RecuperarNoDaFuncao(tree, _nomeFuncaoOtimizar);

            if (funcaoOtimizar == null)
                throw new ApplicationException(String.Format("Função não encontrada: {0}", _nomeFuncaoOtimizar));


            var blocoDaFuncao = funcaoOtimizar.GetChild(2);

             for (int i = 0; i < blocoDaFuncao.ChildCount; i++)
             {

                 var instrucaoAtual = blocoDaFuncao.GetChild(i);
                 var funcao = AdicionarFuncao(instrucaoAtual, true);

             }

        }

        /// <summary>
        /// Determina se um nó é 'função' ou não
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        private static bool DeterminarFuncao(ITree no)
        {
            switch (no.Type)
            {
                case 113: //Block
                    return true;
                    break;
                case 116: //Call
                    return true;
                    break;
                case 126: //PAREXPR = {}
                    return true;
                    break;
                default: // todo o restante
                    if (no.Type >= 7)
                    {
                        if (no.Type <= 110)
                        {
                            return true;
                        }
                    }
                    break;

            }
            return false;
        }

        /// <summary>
        /// Escreve o arquivo de gramatica
        /// </summary>
        /// <param name="dirinfo"></param>
        private static string ProcessarGramatica(DirectoryInfo dirinfo)
        {

            //ECJ com gramatica tem problema para processar sobrecarga... :-/

            int i = 0;

            var arquivo = dirinfo.FullName + @"\" + jsFile + ".grammar";
            var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

            //Declara as funções na gramatica
            foreach (var funcao in _funcoes)
            {
                if(funcao.Principal)
                    sw.WriteLine("<start> ::= <{0}> ", funcao.Nome);
            }

            //Descreve as funções
            foreach (var funcao in _funcoes)
            {
                sw.Write(string.Format("<{0}> ::= ", funcao.Nome));
                sw.Write(string.Format("({0} ", funcao.NomeSemArgumentos));

                foreach (var argumento in funcao.Argumentos)
                {
                    sw.Write(string.Format("<{0}> ", argumento.Nome));

                    if (!_argumentos.Contains(argumento))
                    {
                        _argumentos.Add(argumento);

                    }
                }

                sw.Write(")");
                sw.WriteLine("");
            }

            //Descreve os argumentos
            foreach (var argumento in _argumentos)
            {
                sw.Write(string.Format("<{0}> ::= ", argumento.Nome));
                sw.Write(string.Format("({0}", argumento.Nome));
                sw.Write(")");
                sw.WriteLine("");

            }


            sw.Close();
            //i++;
            //Console.WriteLine(i);
            //Console.Read();

            return Path.GetFileName(arquivo);
        }

        /// <summary>
        /// Adicona ou não uma função a Lista global
        /// </summary>
        /// <param name="instrucao"></param>
        /// <param name="principal">Determina se a funcao é para escrever na linha principal da gramatica</param>
        private static Funcao AdicionarFuncao(ITree instrucao, bool principal)
        {
            var total = _funcoes.Count(funcao => funcao.NomeSemArgumentos == Funcao.TraduzirNome(instrucao.Text));

            var func = new Funcao() {Nome = instrucao.Text, Instancia = total, Principal = principal};
            _funcoes.Add(func);

            #region Determina os argumentos da função. Se for outra função faz a recursão

            // Tipos especiais que precisam de tratamento especifico devido a ATS do ANTLR
            switch (instrucao.Type)
            {
                case 116: // CALL
                    #region Tratamendo de nó tipo CALL
                    TratarNoCall(instrucao, func);
                    break;

                    #endregion
                default:
                    #region roda os filhos e adiciona como argumentos da função.
                    for (int i = 0; i < instrucao.ChildCount; i++)
                    {

                        if (DeterminarFuncao(instrucao.GetChild(i)))
                        {
                            var argumento = AdicionarFuncao(instrucao.GetChild(i), false);
                            //após o add a função tem o nome correto
                            func.AddArgumento(argumento.Nome);
                        }
                        else
                        {
                            var argumento = instrucao.GetChild(i);
                            func.AddArgumento(Funcao.TraduzirNome(argumento.Text));
                        }
                    }

                    #endregion
                    break;
            }


            

            #endregion

            
            
            return func;            
        }

        /// <summary>
        /// Trata um nó do tipo
        /// </summary>
        /// <param name="argumento"></param>
        /// <param name="func"></param>
        private static void TratarNoCall(ITree argumento, Funcao func)
        {
            //(call <funcao> <args>)
            //<call_0> ::= (call <determinar> <ldias> <lmes>)

            var nomeFuncaoAlvo = argumento.GetChild(0).Text;
            var total = _funcoes.Count(funcao => funcao.NomeSemArgumentos == Funcao.TraduzirNome(nomeFuncaoAlvo));
            var funcaoAlvo = new Funcao() {Nome = nomeFuncaoAlvo, Instancia = total, Principal = false};

            //Itera os argumentos de fato. Se for funcao faz um recursao disfarçada
            for (int j = 0; j < argumento.GetChild(1).ChildCount; j++)
            {
                var arg = argumento.GetChild(1).GetChild(j);

                if (DeterminarFuncao(arg))
                {
                    var argumentoInstanciado = AdicionarFuncao(arg, false);
                    funcaoAlvo.AddArgumento(argumentoInstanciado.Nome);
                }
                else
                    funcaoAlvo.AddArgumento(Funcao.TraduzirNome(arg.Text));
            }

            _funcoes.Add(funcaoAlvo);

            //Adicona a funcao CALL_N a instancia da função
            func.AddArgumento(funcaoAlvo.Nome);
        }

        /// <summary>
        /// Descobre qual a função na Tree pelo nome
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="nomeFuncaoOtimizar"></param>
        /// <returns></returns>
        private static ITree RecuperarNoDaFuncao(CommonTree tree, string nomeFuncaoOtimizar)
        {

            for (int i = 0; i < tree.ChildCount; i++)
            {
                var funcaoAtual = tree.GetChild(i);

                if (funcaoAtual.GetChild(0).Text == nomeFuncaoOtimizar)
                    return funcaoAtual;
            }

            return null;
        }



        /// <summary>
        /// Gera o .Java do problema
        /// </summary>
        /// <param name="dirinfo"></param>
        /// <param name="textoProblema"></param>
        /// <returns></returns>
        private static string ProcessarProblema(DirectoryInfo dirinfo, string textoProblema)
        {
            var arquivo = dirinfo.FullName + @"\Problem.java";

            string textoClasse = textoProblema;

            //@package
            textoClasse = textoClasse.Replace("@package", jsFile);

            var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));
            sw.Write(textoClasse);
            sw.Close();

            return "ec.app." + jsFile + ".Problem";
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

                var arquivo = dirinfo.FullName + @"\" + argumento.Nome + ".java";

                var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

                string textoClasse = textoJava;

                //@package
                textoClasse = textoClasse.Replace("@package", jsFile);
                //@NomeFuncao
                textoClasse = textoClasse.Replace("@NomeFuncao", argumento.Nome);

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
        /// <param name="nomeProblema"></param>
        /// <remarks>
        /// @NomeArquivoGramatica
        /// @NumeroDeFuncoes
        /// </remarks>
        private static string ProcessarConfiguracao(DirectoryInfo configuracao, string textoConfiguracao, string nomeArquivoGramatica, string nomeProblema)
        {
            var arquivo = configuracao.FullName + @"\" + jsFile + ".params";
            StringBuilder sb = new StringBuilder();
            var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

            //@NomeArquivoGramatica
            textoConfiguracao = textoConfiguracao.Replace("@NomeArquivoGramatica", nomeArquivoGramatica);
            //@Problema
            textoConfiguracao = textoConfiguracao.Replace("@Problema", nomeProblema);

            for (int i = 0; i < _funcoes.Count; i++)
            {
                var f = _funcoes[i];
                sb.AppendLine(string.Format("gp.fs.0.func.{0} = {1}", i, _package + "." + f.Nome));
                sb.AppendLine(string.Format("gp.fs.0.func.{0}.nc = nc{1}", i, f.Argumentos.Count));
            }

            var j = 0;
            
            for (int i = 0; i < _argumentos.Count; i++)
            {

                var argumento = _argumentos[i];
                if (!_funcoes.Exists(f => f.NomeSemArgumentos == argumento.Nome))
                {
                    //TODO: retirar os argumentos repeditos e inválidos da lista ANTES de iterar
                    sb.AppendLine(string.Format("gp.fs.0.func.{0} = {1}", _funcoes.Count + j, _package + "." + argumento.Nome));
                    sb.AppendLine(string.Format("gp.fs.0.func.{0}.nc = nc{1}", _funcoes.Count + j, 0));
                    j++;
                }
            }


            //@NumeroDeFuncoes
            int total = _funcoes.Count + j;
            textoConfiguracao = textoConfiguracao.Replace("@NumeroDeFuncoes", total.ToString(CultureInfo.InvariantCulture));


            sw.Write(textoConfiguracao);
            sw.Write(sb.ToString());

            sw.Close();

            return arquivo;
        }


    } 
}
