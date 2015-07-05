using System;
using System.IO;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using ConsoleApplication1;
using Jurassic;
using Jurassic.Library;
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
        [Ignore]
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
        [Ignore]
        public void RunTestsFromMomentJs()
        {
            const string jsTestFile = "tests.js";
            const string qunitFile = "qunit-1.18.0.js";
            const string fileMomentPath = "moment-with-locales.js";
            
            var engine = new Jurassic.ScriptEngine();
            engine.SetGlobalFunction("alert", new DAlertDelegate(Console.WriteLine));

            engine.ExecuteFile(fileMomentPath);
            engine.ExecuteFile(qunitFile);
            engine.ExecuteFile(jsTestFile);

            #region Debug of tests

//            try
//            {
//                engine.Execute(@"   moment.locale('af');
//                                module('unit tests');
//
//                test('long years', function (assert) {
//                    assert.equal(moment.utc().year(2).format('YYYYYY'), '+000002', 'small year with YYYYYY');
//                    assert.equal(moment.utc().year(2012).format('YYYYYY'), '+002012', 'regular year with YYYYYY');
//                    assert.equal(moment.utc().year(20123).format('YYYYYY'), '+020123', 'big year with YYYYYY');
//            /*
//                    assert.equal(moment.utc().year(-1).format('YYYYYY'), '-000001', 'small negative year with YYYYYY');
//                    assert.equal(moment.utc().year(-2012).format('YYYYYY'), '-002012', 'negative year with YYYYYY');
//                    assert.equal(moment.utc().year(-20123).format('YYYYYY'), '-020123', 'big negative year with YYYYYY');
//            */	
//                });
//
//            ");


//            }
//            catch (JavaScriptException ex)
//            {
//                Console.WriteLine(string.Format("Script error in \'{0}\', line: {1}\n{2}", ex.SourcePath, ex.LineNumber, ex.Message));
//            }
            
            #endregion

            try
            {
                
                
                
                #region registra os retornos dos testes
                engine.Execute(@"   QUnit.done(function( details ) {
                                    alert('=============================================');
                                    alert('Total:' + details.total);
                                    alert('Falha:' + details.failed);
                                    alert('Sucesso:' + details.passed);
                                    alert('Tempo:' + details.runtime);
                                });


                                QUnit.testDone(function( details ) {
                                    alert('Modulo:' + details.module);
                                    alert('Teste:' + details.name);
                                    alert(' Falha:' + details.failed);
                                    alert(' Total:' + details.total);
                                    alert(' Tempo:' + details.duration);
                                });

                                QUnit.config.autostart = false;
                                QUnit.config.ignoreGlobalErrors = true;
                        ");
                #endregion

                engine.Execute(@"   //moment.locale('pt-br'); 
                                    //alert(moment('Maio', 'MMM').month());
                                    QUnit.load();
                                    QUnit.start();
                ");
            }
            catch (JavaScriptException ex)
            {
                Console.WriteLine(string.Format("Script error in \'{0}\', line: {1}\n{2}", ex.SourcePath, ex.LineNumber, ex.Message));
            }
            catch (Exception)
            {
                
                throw;
            }




        }


        /// <summary>
        /// Execute the tests of MomentJs
        /// </summary>
        [Test]
        public void RunTestsFromGeneratedMomentJs()
        {
            const string jsTestFile = "tests.js";
            const string qunitFile = "qunit-1.18.0.js";
            const string fileMomentPath = "moment-with-locales.js";
            const string fileGeneratedCode = "target.js";

            var momentTextWithoutComments = File.ReadAllText("moment.Js");

            #region Build the AST from Js and Generate the code
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

            var engine = new Jurassic.ScriptEngine();
            engine.SetGlobalFunction("alert", new DAlertDelegate(Console.WriteLine));

            engine.ExecuteFile(fileGeneratedCode);
            engine.ExecuteFile(qunitFile);
            engine.ExecuteFile(jsTestFile);

            #region Debug of tests

            //            try
            //            {
            //                engine.Execute(@"   moment.locale('af');
            //                                module('unit tests');
            //
            //                test('long years', function (assert) {
            //                    assert.equal(moment.utc().year(2).format('YYYYYY'), '+000002', 'small year with YYYYYY');
            //                    assert.equal(moment.utc().year(2012).format('YYYYYY'), '+002012', 'regular year with YYYYYY');
            //                    assert.equal(moment.utc().year(20123).format('YYYYYY'), '+020123', 'big year with YYYYYY');
            //            /*
            //                    assert.equal(moment.utc().year(-1).format('YYYYYY'), '-000001', 'small negative year with YYYYYY');
            //                    assert.equal(moment.utc().year(-2012).format('YYYYYY'), '-002012', 'negative year with YYYYYY');
            //                    assert.equal(moment.utc().year(-20123).format('YYYYYY'), '-020123', 'big negative year with YYYYYY');
            //            */	
            //                });
            //
            //            ");


            //            }
            //            catch (JavaScriptException ex)
            //            {
            //                Console.WriteLine(string.Format("Script error in \'{0}\', line: {1}\n{2}", ex.SourcePath, ex.LineNumber, ex.Message));
            //            }

            #endregion

            try
            {

                #region registra os retornos dos testes
                engine.Execute(@"   QUnit.done(function( details ) {
                                    alert('=============================================');
                                    alert('Total:' + details.total);
                                    alert('Falha:' + details.failed);
                                    alert('Sucesso:' + details.passed);
                                    alert('Tempo:' + details.runtime);
                                });


                                QUnit.testDone(function( details ) {
                                    alert('Modulo:' + details.module);
                                    alert('Teste:' + details.name);
                                    alert(' Falha:' + details.failed);
                                    alert(' Total:' + details.total);
                                    alert(' Tempo:' + details.duration);
                                });

                                QUnit.config.autostart = false;
                                QUnit.config.ignoreGlobalErrors = true;
                        ");
                #endregion

                engine.Execute(@"   //moment.locale('pt-br'); 
                                    //alert(moment('Maio', 'MMM').month());
                                    QUnit.load();
                                    QUnit.start();
                ");
            }
            catch (JavaScriptException ex)
            {
                Console.WriteLine(string.Format("Script error in \'{0}\', line: {1}\n{2}", ex.SourcePath, ex.LineNumber, ex.Message));
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Delegate para mensages de alerta no código
        /// </summary>
        /// <param name="message"></param>
        private delegate void DAlertDelegate(string message);
    }



}
