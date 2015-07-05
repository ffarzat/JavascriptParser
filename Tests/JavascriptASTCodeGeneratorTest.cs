using System;
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
            const string localeFile = "locales.js";
            const string jsTestFile = "tests.js";
            const string envJs = "";
            const string qunitFile = "qunit-1.18.0.js";
            const string fileMomentPath = "moment.js";
            
            var engine = new Jurassic.ScriptEngine();
            
            engine.SetGlobalFunction("alert", new DAlertDelegate(Console.WriteLine));

            //engine.ExecuteFile(envJs);
            engine.ExecuteFile(fileMomentPath);
            engine.ExecuteFile(localeFile);
            engine.ExecuteFile(qunitFile);
            //engine.ExecuteFile(jsTestFile);



            engine.Execute(@"moment.locale('af');");


            engine.Execute(@"   QUnit.done(function( details ) {
                                    alert(details.total);
                                    alert(details.failed);
                                    alert(details.passed);
                                    alert(details.runtime);
                                });

                                QUnit.config.autostart = false;

                        ");


            engine.Execute(@"   
                                test('parse', function (assert) {
                                        assert.equal(moment([2011, 0, 1]).format('DDDo'), '1ste');
                                   
                                });

                                QUnit.load();
                                QUnit.start();
                                
                            ");

            
            
        }
        
        private delegate void DAlertDelegate(string message);
    }



}
