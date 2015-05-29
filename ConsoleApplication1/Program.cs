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
            var dirinfo = new DirectoryInfo(jsFile);
            
            var nomeArquivoGramatica = ProcessarGramatica(dirinfo, tree);
            var nomeProblema = ProcessarProblema(dirinfo, textoProblema);
            var nomeArquivoConfiguracao = ProcessarConfiguracao(dirinfo, textoConfiguracao, nomeArquivoGramatica, nomeProblema);
            //ProcessarArquivosDeNo(dirinfo, textoJava);
            

        } 

        /// <summary>
        /// Escreve o arquivo de gramatica
        /// </summary>
        /// <param name="dirinfo"></param>
        /// <param name="tree"></param>
        private static string ProcessarGramatica(DirectoryInfo dirinfo, CommonTree tree)
        {
            int i = 0;
            var arquivo = dirinfo.FullName + @"\" + jsFile + ".grammar";
            var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

            var funcaoOtimizar = RecuperarNoDaFuncao(tree, _nomeFuncaoOtimizar);

            if(funcaoOtimizar == null)
                throw new ApplicationException(String.Format("Função não encontrada: {0}", _nomeFuncaoOtimizar));

            EscreverArvoreDaFuncao(funcaoOtimizar, sw);
            
            sw.Close();

            return Path.GetFileName(arquivo);
        }

        /// <summary>
        /// Visita todas as instruções da function e escreve a gramática correspondente
        /// </summary>
        /// <param name="funcaoOtimizar"></param>
        /// <param name="sw"></param>
        private static void EscreverArvoreDaFuncao(ITree funcaoOtimizar, StreamWriter sw)
        {

            var blocoDaFuncao = funcaoOtimizar.GetChild(2);

            for (int i = 0; i < blocoDaFuncao.ChildCount; i++)
            {

                var instrucaoAtual = blocoDaFuncao.GetChild(i);

                sw.Write(string.Format("<{0}> ::= ", "start"));
                
                EscreverNoPeloTipo(instrucaoAtual, sw);

                sw.WriteLine("");
            }

            sw.Flush();
        }

        /// <summary>
        /// Trata cada tipo de instrução para escrever sua gramatica
        /// </summary>
        /// <param name="instrucao"></param>
        /// <param name="sw"></param>
        private static void EscreverNoPeloTipo(ITree instrucao, StreamWriter sw)
        {

            switch (instrucao.Type)
            {

                case 18: //if
                    EscreverFuncaoComVariosParametros(instrucao, sw);
                    break;

                case 22: //return
                    EscreverFuncaoComVariosParametros(instrucao, sw);
                    break;

                case 28: //Var
                    EscreverFuncaoComVariosParametros(instrucao, sw);
                    break;

                case 74: //Atribuição de variável
                    EscreverFuncaoComDoisParametros(instrucao, sw);
                    break;

                case 80: //Soma
                    EscreverFuncaoComDoisParametros(instrucao, sw);
                    
                    break;

                case 83: // %
                    EscreverFuncaoComDoisParametros(instrucao, sw);
                    break;

                case 98: //Atribuição de variável
                    EscreverFuncaoComDoisParametros(instrucao, sw);
                    
                    break;

                case 109: // divisão (/)
                    EscreverFuncaoComDoisParametros(instrucao, sw);
                    
                    break;

                case 111: //Args

                    for (int i = 0; i < instrucao.ChildCount; i++)
                    {
                        var instrucaoDoBloco = instrucao.GetChild(i);
                        EscreverNoPeloTipo(instrucaoDoBloco, sw);
                        
                    }

                    break;

                case 113: //Block

                    for (int i = 0; i < instrucao.ChildCount; i++)
                    {
                        var instrucaoDoBloco = instrucao.GetChild(i);
                        //TODO: Rever se é assim mesmo quw vai ser tratado
                        //sw.WriteLine("");
                        //sw.Write("<start> ::= ");
                        EscreverNoPeloTipo(instrucaoDoBloco, sw);
                        
                    }
                    break;

                case 116: //CALL de Função

                    //Adicionar o nome da Função
                    //Adicionar os Parametros dentro de Args.

                    EscreverInicioDeFuncao(instrucao.GetChild(0), sw);
                    
                    EscreverNoPeloTipo(instrucao.GetChild(1), sw); //Argumentos

                    EscreverFinalDeFuncao(sw);

                    AdicionarCALLDeFuncao(instrucao);
                    
                    break;

                case 126: //PAREXPR (algo dentro)
                    EscreverNoPeloTipo(instrucao.GetChild(0), sw);
                    
                    break;

                default:
                    //Escreve variáveis na gramática
                    sw.Write(string.Format("({0})", instrucao.Text));
                    break;
            }

        }

        /// <summary>
        /// Escreve o nó de função na gramatica levando em conta dois nós filhos
        /// </summary>
        /// <param name="instrucao"></param>
        /// <param name="sw"></param>
        private static void EscreverFuncaoComDoisParametros(ITree instrucao, StreamWriter sw)
        {
            EscreverInicioDeFuncao(instrucao, sw);

            EscreverNoPeloTipo(instrucao.GetChild(0), sw);
            EscreverNoPeloTipo(instrucao.GetChild(1), sw);

            EscreverFinalDeFuncao(sw);

            AdicionarFuncao(instrucao);
        }

        /// <summary>
        /// Adicona ou não uma função a Lista global
        /// </summary>
        /// <param name="instrucao"></param>
        private static void AdicionarFuncao(ITree instrucao)
        {
            var func = new Funcao() {Nome = instrucao.Text};

            for (int i = 0; i < instrucao.ChildCount; i++)
            {
                func.AddArgumento(instrucao.GetChild(i).Text);
                
                var argumento = new Argumento() {Nome = instrucao.GetChild(i).Text};

                if (!_argumentos.Exists(a => a.Nome == argumento.Nome))
                {
                    

                    switch (argumento.Nome)
                    {
                        case "PAREXPR":
                            //Faço nada
                            break;

                        case "BLOCK":
                            //Faço nada
                            break;

                        default:
                            _argumentos.Add(argumento);
                            break;
                    }
                }

            }

            if (!_funcoes.Exists(f => f.Nome == func.Nome))
                _funcoes.Add(func);
        }

        /// <summary>
        /// Adiciona um nó do tipo call na lista de funções globais
        /// </summary>
        /// <param name="instrucao"></param>
        private static void AdicionarCALLDeFuncao(ITree instrucao)
        {
            var func = new Funcao() { Nome = instrucao.GetChild(0).Text };

            for (int i = 0; i < instrucao.GetChild(1).ChildCount; i++)// ARGS
            {
                var argumentoDaInstrucao = instrucao.GetChild(1).GetChild(i);

                var argumento = new Argumento() { Nome = argumentoDaInstrucao.Text };

                func.Argumentos.Add(argumento);

                if (!_funcoes.Exists(a => a.NomeSemArgumentos == argumento.Nome))
                {
                    _funcoes.Add(new Funcao() {Nome = argumento.Nome});
                }

            }

            if (!_funcoes.Exists(f => f.Nome == func.Nome))
                _funcoes.Add(func);
        }

        /// <summary>
        /// Escreve o nó de função na gramatica levando em todos os nós filhos
        /// </summary>
        /// <param name="instrucao"></param>
        /// <param name="sw"></param>
        private static void EscreverFuncaoComVariosParametros(ITree instrucao, StreamWriter sw)
        {
            AdicionarFuncao(instrucao);

            EscreverInicioDeFuncao(instrucao, sw);

            for (int i = 0; i < instrucao.ChildCount; i++)
            {
                var instrucaoDoBloco = instrucao.GetChild(i);
                EscreverNoPeloTipo(instrucaoDoBloco, sw);
            }

            EscreverFinalDeFuncao(sw);

            
        }

        /// <summary>
        /// Fecha um nó terminal na gramática
        /// </summary>
        /// <param name="sw"></param>
        private static void EscreverFinalDeFuncao(StreamWriter sw)
        {
            sw.Write(")");
        }

        /// <summary>
        /// Abre nó terminal na gramática
        /// </summary>
        /// <param name="instrucao"></param>
        /// <param name="sw"></param>
        private static void EscreverInicioDeFuncao(ITree instrucao, StreamWriter sw)
        {
            sw.Write(string.Format("({0} ", instrucao.Text));
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
        private static string ProcessarConfiguracao(DirectoryInfo configuracao, string textoConfiguracao, string nomeArquivoGramatica, string nomeProblema)
        {
            var arquivo = configuracao.FullName + @"\" + jsFile + ".params";

            var sw = new StreamWriter(arquivo, false, new UTF8Encoding(false));

            //@NomeArquivoGramatica
            textoConfiguracao = textoConfiguracao.Replace("@NomeArquivoGramatica", nomeArquivoGramatica);
            //@NumeroDeFuncoes
            int total = _funcoes.Count + _argumentos.Count;
            textoConfiguracao = textoConfiguracao.Replace("@NumeroDeFuncoes", total.ToString(CultureInfo.InvariantCulture));
            //@Problema
            textoConfiguracao = textoConfiguracao.Replace("@Problema", nomeProblema);

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
                if (!_funcoes.Exists(f => f.NomeSemArgumentos == argumento.Nome))
                {
                    //TODO: retirar os argumentos repeditos e inválidos da lista ANTES de iterar
                    sw.WriteLine(string.Format("gp.fs.0.func.{0} = {1}", _funcoes.Count + i, _package + "." + argumento.Nome));
                    sw.WriteLine(string.Format("gp.fs.0.func.{0}.nc = nc{1}", _funcoes.Count + i, 0));
                }
            }

            sw.Close();

            return arquivo;
        }


    } 
}
