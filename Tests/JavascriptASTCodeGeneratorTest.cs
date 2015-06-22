using System.IO;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using ConsoleApplication1;
using NUnit.Framework;
using Xebic.Parsers.ES3;

namespace Tests
{
    [TestFixture]
    public class JavascriptAstCodeGeneratorTest
    {
        private const string JsFileWithouComments = @"momentNoComments.js";
        private static string _javascriptTextWithoutComments = "";
        private static CommonTree _tree;

        [TestFixtureSetUp]
        public void Setup()
        {
            _javascriptTextWithoutComments = System.IO.File.ReadAllText(JsFileWithouComments);

            #region Build the AST from Js
            var stream = new ANTLRStringStream(_javascriptTextWithoutComments);
            var lexer = new ES3Lexer(stream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ES3Parser(tokenStream);
            ES3Parser.program_return programReturn = parser.program();
            _tree = programReturn.Tree as CommonTree;
            #endregion
        }

        //Covers ToString Behavior
        [Test]
        public void ToCodeTest()
        {
            var codeGenerator = new JavascriptAstCodeGenerator(_tree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();

            File.WriteAllText("generatedJsCode.js", generatedJsCode);

            var originalText = _javascriptTextWithoutComments.Replace(" ", "").Replace("\r\n", "");
            var generatedText = generatedJsCode.Replace(" ", "").Replace("\r\n", "");

            Assert.AreEqual(originalText, generatedText);
        }

    }
}
