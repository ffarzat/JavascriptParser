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

        static List<string> _funcoes = new List<string>();


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
                
                EscreverFuncoes(tree);

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
        private static void EscreverFuncoes(CommonTree tree)
        {
            var sw = new StreamWriter(Environment.CurrentDirectory + @"\Arvoreprocessada.txt");
            sw.WriteLine(" │");
            PrintTree(" ", true, tree, sw);
            sw.Close();
        }

        /// <summary>
        /// Itera a árvore e escreve no arquivo
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="isTail"></param>
        /// <param name="tree"></param>
        /// <param name="sw"></param>
        private static void PrintTree(string prefix, bool isTail, CommonTree tree, StreamWriter sw) 
        {

            if (tree.Type == 17) //function
            {
                sw.WriteLine(prefix + "└── " + tree.Text);

                if (tree.ChildCount > 0)
                {
                    foreach (CommonTree child in tree.Children)
                    {
                        if(child.Type != 113)
                            sw.WriteLine(prefix + "    ├── " + child.Text);

                        if (child.Type == 111)//ARGS
                        {
                            if (child.ChildCount > 0)
                            {
                                foreach (CommonTree child2 in child.Children)
                                {
                                    sw.WriteLine(prefix + "        ├── " + child2.Text);
                                }
                            }
                        }
                    }
                }

            }



            

            if (tree.ChildCount > 0)
            {
                foreach (CommonTree child in tree.Children)
                {
                    PrintTree(prefix + (isTail ? "    " : "│   "), false, child, sw);
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
