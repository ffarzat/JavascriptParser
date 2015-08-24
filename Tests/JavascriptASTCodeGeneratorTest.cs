using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using ConsoleApplication1;
using Microsoft.ClearScript.V8;
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


        public static int Total { get; set; }
        public static int Sucesso { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ToOrderTest()
        {
            var list = JavascriptAstCodeGenerator.BuildFunctionList(_tree); //weak way
            var functionsMostUseds = JavascriptAstCodeGenerator.FindTopUsedFunctions(_tree);
            var functionsWithCallsAndLocs = JavascriptAstCodeGenerator.FindTopLocFunctions(functionsMostUseds).ToList();

            Assert.AreEqual(functionsWithCallsAndLocs[0], "DeterminarQuantidadeDeDias");
            Assert.AreEqual(functionsWithCallsAndLocs[1], "escreverNaTela");
            
        }

        /// <summary>
        /// Generates code?
        /// </summary>
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
        /// Find the top used functions
        /// </summary>
        [Test]
        public void FindFunctionCalls()
        {
            var functionsFounded = JavascriptAstCodeGenerator.FindTopUsedFunctions(_tree);

            Assert.AreEqual(functionsFounded.Count , 4);
            
            Assert.IsTrue(functionsFounded.ContainsKey("parseInt"));
            Assert.AreEqual(functionsFounded["parseInt"], 5);

            Assert.IsTrue(functionsFounded.ContainsKey("print"));
            Assert.AreEqual(functionsFounded["print"], 1);

        }

        /// <summary>
        /// Count INstructions
        /// </summary>
        [Test]
        public void CoutInstructions()
        {
            var escreverNaTela = JavascriptAstCodeGenerator.FindFunctionTree(_tree, "escreverNaTela");
            var loc = JavascriptAstCodeGenerator.CountInstructionsOf(escreverNaTela);

            Assert.AreEqual(loc, 2);
        }

        /// <summary>
        /// MomentJs Code regenaration
        /// </summary>
        [Test]
        public void ToMomentCodeTest()
        {

            var momentTextWithoutComments = File.ReadAllText("momentNoComments.Js"); //momentNoComments

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
            string jsTestFile = File.ReadAllText("core-test.js");
            string qunitFile = File.ReadAllText("qunit-1.18.0.js");
            string fileMomentPath = File.ReadAllText("global.js");

            var engine = new V8ScriptEngine();

            engine.AddHostType("helper", typeof(JavascriptAstCodeGeneratorTest));
            engine.Execute(fileMomentPath);
            engine.Execute(qunitFile);
            engine.Execute(jsTestFile);

            
           #region registra os retornos dos testes
                engine.Execute(@"   
                                    var total, sucesso, falha;

                                    QUnit.done(function( details ) {
                                    //alert('=============================================');
                                    //alert('Total:' + details.total);
                                    //alert('Falha:' + details.failed);
                                    //alert('Sucesso:' + details.passed);
                                    //alert('Tempo:' + details.runtime);
                                       
                                    helper.Total = details.total;
                                    helper.Sucesso = details.passed;
                                    //falha = details.failed;

                                });

                                QUnit.config.autostart = false;
                                QUnit.config.ignoreGlobalErrors = true;
                        ");
                #endregion

                engine.Execute(@"   QUnit.load();
                                    QUnit.start();
                ");

            Assert.AreEqual(Total, 57982);
            Assert.AreEqual(Total, Sucesso);
        }


        /// <summary>
        /// Execute the tests of MomentJs
        /// </summary>
        [Test]
        public void RunTestsFromGeneratedMomentJs()
        {
            string jsTestFile = File.ReadAllText("tests.js");
            string qunitFile = File.ReadAllText("qunit-1.18.0.js");
            string fileMomentPath = File.ReadAllText("moment.js");
            string fileGeneratedCode = "target.js";
            
            var sw = new Stopwatch();
            var swTotal = new Stopwatch();

            swTotal.Start();

            sw.Start();
            #region Build the AST from Js and Generate the code
            var momentTextWithoutComments = fileMomentPath;
            
            var stream = new ANTLRStringStream(momentTextWithoutComments);
            var lexer = new ES3Lexer(stream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ES3Parser(tokenStream);
            ES3Parser.program_return programReturn = parser.program();
            var tree = programReturn.Tree as CommonTree;

            var codeGenerator = new JavascriptAstCodeGenerator(tree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();
            File.WriteAllText(fileGeneratedCode, generatedJsCode);
            #endregion
            sw.Stop();
            Console.WriteLine("{0} - {1}", "AST criada", sw.Elapsed.ToString("mm\\:ss\\.ff"));

            sw.Reset();
            sw.Start();
            var engine = new V8ScriptEngine();
            engine.AddHostType("helper", typeof(JavascriptAstCodeGeneratorTest));
            
            sw.Stop();
            Console.WriteLine("{0} - {1}", "Engine criada e configurada", sw.Elapsed.ToString("mm\\:ss\\.ff"));

            sw.Reset();
            sw.Start();
            engine.Execute(generatedJsCode);
            sw.Stop();
            Console.WriteLine("{0} - {1}", "Individuo novo carregado", sw.Elapsed.ToString("mm\\:ss\\.ff"));
            
            sw.Reset();
            sw.Start();
            engine.Execute(qunitFile);
            Console.WriteLine("{0} - {1}", "", sw.Elapsed.ToString("mm\\:ss\\.ff"));
            sw.Stop();

            sw.Reset();
            sw.Start();
            engine.Execute(jsTestFile);
            sw.Stop();
            Console.WriteLine("{0} - {1}", "", sw.Elapsed.ToString("mm\\:ss\\.ff"));
            

            sw.Reset();
            sw.Start();

            #region registra os retornos dos testes
            engine.Execute(@"   
                                var total, sucesso, falha;

                                QUnit.done(function( details ) {
                                //alert('=============================================');
                                //alert('Total:' + details.total);
                                //alert('Falha:' + details.failed);
                                //alert('Sucesso:' + details.passed);
                                //alert('Tempo:' + details.runtime);
                                       
                                helper.Total = details.total;
                                helper.Sucesso = details.passed;
                                //falha = details.failed;

                            });

                            QUnit.config.autostart = false;
                            QUnit.config.ignoreGlobalErrors = true;
                    ");
            #endregion


            sw.Reset();
            sw.Start();

            engine.Execute(@"   
                                QUnit.config.autostart = false;
                                QUnit.config.ignoreGlobalErrors = true;
                                //QUnit.config.moduleFilter = ""zones"";
            ");

            engine.Execute(@"   QUnit.load();
                                QUnit.start();
            ");
                
            sw.Stop();
            Console.WriteLine("{0} - {1}", "Execução dos testes", sw.Elapsed.ToString("mm\\:ss\\.ff"));
            
            
            
            Console.WriteLine("{0} - {1}", "Tempo total", swTotal.Elapsed.ToString("mm\\:ss\\.ff"));
            Assert.AreEqual(Total, Sucesso);
            Assert.Greater(Total, 1);

        }

        /// <summary>
        /// Delegate para mensages de alerta no código
        /// </summary>
        /// <param name="message"></param>
        private delegate void DAlertDelegate(string message);

        /// <summary>
        /// Make a full clone of a Tree (AST)
        /// </summary>
        [Test]
        public void DeepCloneTest()
        {
            var momentTextWithoutComments = File.ReadAllText("moment.js"); 

            #region Build the AST from Js
            var stream = new ANTLRStringStream(momentTextWithoutComments);
            var lexer = new ES3Lexer(stream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ES3Parser(tokenStream);
            ES3Parser.program_return programReturn = parser.program();
            var tree = programReturn.Tree as CommonTree;
            #endregion

            var clone = JavascriptAstCodeGenerator.DeepClone(tree);
            var anotherClone = JavascriptAstCodeGenerator.DeepClone(tree);
            var cloneofClone = JavascriptAstCodeGenerator.DeepClone(anotherClone);

            Assert.AreNotSame(clone, anotherClone);
            Assert.AreNotSame(anotherClone, cloneofClone);
        }

    }

}
