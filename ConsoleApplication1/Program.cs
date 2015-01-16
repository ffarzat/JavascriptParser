using System;
using System.Collections.Generic;
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
        static int indent = 0;
        private static string jsFile = @"angular.js";

        static List<Funcao> _funcoes = new List<Funcao>();


        static void Main(string[] args)
        {
            try
            {


                string text = System.IO.File.ReadAllText(jsFile);

                var stream = new ANTLRStringStream(text);
                var lexer = new ES3Lexer(stream);
                var tokenStream = new CommonTokenStream(lexer);
                var parser = new ES3Parser(tokenStream);
                ES3Parser.program_return programReturn = parser.program();


                var tree = programReturn.Tree as CommonTree;
                
                EscreverGramaticaSimplificada(tree);

                //WriteTree(tree); 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey(true); 
        }

        /// <summary>
        /// Função Base para escrever as funções
        /// </summary>
        /// <param name="tree"></param>
        private static void EscreverGramaticaSimplificada(CommonTree tree)
        {
            var sw = new StreamWriter(Environment.CurrentDirectory + @"\"+ jsFile+ ".grammar");
            
            RecuperarFuncoesEParametros(" ", true, tree, sw);
            
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

            

        }

        /// <summary>
        /// Itera a árvore e escreve no arquivo
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="isTail"></param>
        /// <param name="tree"></param>
        /// <param name="sw"></param>
        private static void RecuperarFuncoesEParametros(string prefix, bool isTail, CommonTree tree, StreamWriter sw) 
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
                    RecuperarFuncoesEParametros(prefix + (isTail ? "    " : "│   "), false, child, sw);
                }
            }

        } 

        /// <summary>
        /// Escreve a Arvore no console
        /// </summary>
        /// <param name="tree"></param>
        static void WriteTree(CommonTree tree) 
        { 
            WriteLine("Tree.Text: " + tree.Text);
            WriteLine("Tree.Type: " + tree.Type); 
            WriteLine("Tree.Line: " + tree.Line); 
            WriteLine("Tree.CharPositionInLine: " + tree.CharPositionInLine); 
            WriteLine("Tree.ChildCount: " + tree.ChildCount); 
            WriteLine(""); 

            if (tree.Children != null) { 
                IncreaseIndent(); 
                foreach (CommonTree child in tree.Children) { 
                    WriteTree(child); 
                } 
                DecreaseIndent(); 
            } 
        } 

        static void WriteLine(string text) 
        { 
            string indentText = String.Empty.PadRight(indent * 2); 
            Console.WriteLine(indentText + text); 
        } 

        static void IncreaseIndent() 
        { 
            indent++; 
        } 

        static void DecreaseIndent() 
        { 
            indent--; 
        } 
    } 
}
