using System;
using System.Collections.Generic;
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
        private static string jsFile = @"jquery-ui-1.8.24.min.js";

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
                WriteTree(tree); 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey(true); 
        }


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
