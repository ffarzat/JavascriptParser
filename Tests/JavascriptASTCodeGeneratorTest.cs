using System;
using System.IO;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using ConsoleApplication1;
using NUnit.Framework;
using Xebic.Parsers.ES3;
using unvell.ReoScript;

namespace Tests
{
    [TestFixture]
    public class JavascriptAstCodeGeneratorTest
    {
        private const string JsFileWithouComments = @"scriptData_sem_comentarios.js";   
        private static string _javascriptTextWithoutComments = "";
        private static CommonTree _tree;

        [TestFixtureSetUp]
        public void Setup()
        {
            _javascriptTextWithoutComments = File.ReadAllText(JsFileWithouComments);

            #region Build the AST from Js
            var stream = new ANTLRStringStream(_javascriptTextWithoutComments);
            var lexer = new ES3Lexer(stream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ES3Parser(tokenStream);
            ES3Parser.program_return programReturn = parser.program();
            _tree = programReturn.Tree as CommonTree;
            #endregion
        }

        
        [Test]
        public void ToCodeTest()
        {
            var codeGenerator = new JavascriptAstCodeGenerator(_tree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();

            File.WriteAllText("generatedJsCode.js", generatedJsCode);

            var originalText = _javascriptTextWithoutComments.Replace(" ", "").Replace("\r\n", "").Replace("\r", "");
            var generatedText = generatedJsCode.Replace(" ", "").Replace("\r\n", "").Replace("\r", "");

            Assert.AreEqual(originalText, generatedText);
        }

        /// <summary>
        /// MomentJs Code regenaration
        /// </summary>
        [Test]
        public void ToMomentCodeTest()
        {

            var momentTextWithoutComments = File.ReadAllText("moment.Js"); //momentNoComments

            #region Build the AST from Js
            var stream = new ANTLRStringStream(momentTextWithoutComments);
            var lexer = new ES3Lexer(stream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ES3Parser(tokenStream);
            ES3Parser.program_return programReturn = parser.program();
            var tree = programReturn.Tree as CommonTree;
            #endregion

            var codeGenerator = new JavascriptAstCodeGenerator(tree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();

            File.WriteAllText("momentgeneratedJsCode.js", generatedJsCode);

            var originalText = momentTextWithoutComments.Replace(" ", "").Replace("\n", "").Replace("\t", "").Replace("\r", "");
            var generatedText = generatedJsCode.Replace(" ", "").Replace("\r\n", "").Replace("\r", "");

            Assert.AreEqual(originalText, generatedText);
        }


        /// <summary>
        /// Execute the tests of MomentJs
        /// </summary>
        [Test]
        public void RunTestsFromMomentJs()
        {
            var fileToParse = File.ReadAllText("moment.Js"); //momentNoComments
            var localeFile = "locales.js";
            var jsqueryFile = "jquery.min.js";
            var jsTestFile = "tests.js";
            var jsRequire = "require.js";
            string fileMomentPath = "momentgeneratedJsCodeForTests.js";
            var scriptRunning = new ScriptRunningMachine();
            
            #region Build the AST from Js
            var stream = new ANTLRStringStream(fileToParse);
            var lexer = new ES3Lexer(stream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ES3Parser(tokenStream);
            ES3Parser.program_return programReturn = parser.program();
            var tree = programReturn.Tree as CommonTree;
            #endregion

            #region Generates the target code
            var codeGenerator = new JavascriptAstCodeGenerator(tree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();
            File.WriteAllText(fileMomentPath, generatedJsCode);
            #endregion

            /*Entender melhor porque o MomentJs não executa sem esses caras, não servem para nada*/
            scriptRunning.SetGlobalVariable("global", scriptRunning.GlobalObject);
            //scriptRunning.SetGlobalVariable("window", scriptRunning.GlobalObject);
            scriptRunning["factory"] = new NativeFunctionObject("print", (ctx, owner, args) => null);

            scriptRunning["window"] = new NativeFunctionObject("window", (ctx, owner, args) => scriptRunning.GlobalObject);

            scriptRunning["print"] = new NativeFunctionObject("print", (ctx, owner, args) =>
            {
                Console.WriteLine(args[0]);
                return null;
            });

            scriptRunning.AllowDirectAccess = true;
            scriptRunning.Load(jsqueryFile);
            scriptRunning.Load(jsRequire);
            scriptRunning.Load(fileMomentPath);
            //scriptRunning.Load(localeFile);
            //scriptRunning.Load(jsTestFile);
            


        }
        
    }
}
