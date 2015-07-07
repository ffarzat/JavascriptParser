using System;
using System.Diagnostics;
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

        
        [Ignore]
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
        [Ignore]
        public void RunTestsFromMomentJs()
        {
            const string jsTestFile = "tests.js";
            const string qunitFile = "qunit-1.18.0.js";
            const string fileMomentPath = "moment.js";
            
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
//    test('creating with utc and no arguments', function (assert) {
//        var startOfTest = new Date().valueOf(),
//            momentDefaultUtcTime = moment.utc().valueOf(),
//            afterMomentCreationTime = new Date().valueOf();
//		
//		alert('startOfTest='+startOfTest);
//		alert('momentDefaultUtcTime='+momentDefaultUtcTime);
//			
//        assert.ok(startOfTest <= momentDefaultUtcTime, 'moment UTC default time should be now, not in the past');
//        assert.ok(momentDefaultUtcTime <= afterMomentCreationTime, 'moment UTC default time should be now, not in the future');
//    });
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
                engine.Execute(@"   
                                    var total, sucesso, falha;

                                    QUnit.done(function( details ) {
                                    alert('=============================================');
                                    alert('Total:' + details.total);
                                    alert('Falha:' + details.failed);
                                    alert('Sucesso:' + details.passed);
                                    alert('Tempo:' + details.runtime);
                                       
                                    total = details.total;
                                    sucesso = details.passed;
                                    falha = details.failed;

                                });

/*

                                QUnit.testDone(function( details ) {
                                    if(details.failed > 0)
                                    {
                                        alert('=============================================');
                                        alert('Modulo:' + details.module);
                                        alert('Teste:' + details.name);
                                        alert(' Falha:' + details.failed);
                                        alert(' Total:' + details.total);
                                        alert(' Tempo:' + details.duration);
                                    }
                                });
*/

                                QUnit.log(function( details ) {
                                  if ( details.result ) {
                                    return;
                                  }
                                  var loc = details.module + ': ' + details.name + ': ',
                                    output = 'FAILED: ' + loc + ( details.message ? details.message + ', ' : '' );
 
                                  if ( details.actual ) {
                                    output += 'expected: ' + details.expected + ', actual: ' + details.actual;
                                  }
                                  if ( details.source ) {
                                    output += ', ' + details.source;
                                  }

                                    alert('=============================================');
                                    alert( output );
                                });



                                QUnit.config.autostart = false;
                                QUnit.config.ignoreGlobalErrors = true;
                        ");
                #endregion

                engine.Execute(@"   QUnit.load();
                                    QUnit.start();
                ");
            }
            catch (JavaScriptException ex)
            {
                Console.WriteLine(string.Format("Script error in \'{0}\', line: {1}\n{2}", ex.SourcePath, ex.LineNumber, ex.Message));
                throw ex;
            }
            catch (Exception)
            {
                
                throw;
            }

            var total = engine.GetGlobalValue<int>("total");
            var sucesso = engine.GetGlobalValue<int>("sucesso");

            Assert.AreEqual(total, sucesso);

        }


        /// <summary>
        /// Execute the tests of MomentJs
        /// </summary>
        [Test]
        public void RunTestsFromGeneratedMomentJs()
        {
            const string jsTestFile = "tests.js";
            //const string qunitFile = "qunit-1.18.0.js";
            const string qunitFile = "core-test.js";
            const string fileMomentPath = "moment.js";
            const string fileGeneratedCode = "target.js";
            var sw = new Stopwatch(); 
            

            #region Build the AST from Js and Generate the code
            var momentTextWithoutComments = File.ReadAllText(fileMomentPath);
            
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

            sw.Start();
            engine.ExecuteFile(fileGeneratedCode);
            engine.ExecuteFile(qunitFile);
            engine.ExecuteFile(jsTestFile);
            sw.Stop();
            //Console.WriteLine("{0} - {1} segundos", qunitFile, sw.Elapsed.TotalSeconds);

            try
            {
                #region registra os retornos dos testes
                engine.Execute(@"   
                                    var total, sucesso, falha;

                                    QUnit.done(function( details ) {
                                    alert('=============================================');
                                    alert('Total:' + details.total);
                                    alert('Falha:' + details.failed);
                                    alert('Sucesso:' + details.passed);
                                    alert('Tempo:' + details.runtime);
                                       
                                    total = details.total;
                                    sucesso = details.passed;
                                    falha = details.failed;

                                });

/*

                                QUnit.testDone(function( details ) {
                                    if(details.failed > 0)
                                    {
                                        alert('=============================================');
                                        alert('Modulo:' + details.module);
                                        alert('Teste:' + details.name);
                                        alert(' Falha:' + details.failed);
                                        alert(' Total:' + details.total);
                                        alert(' Tempo:' + details.duration);
                                    }
                                });
*/

                                QUnit.log(function( details ) {
                                  if ( details.result ) {
                                    return;
                                  }
                                  var loc = details.module + ': ' + details.name + ': ',
                                    output = 'FAILED: ' + loc + ( details.message ? details.message + ', ' : '' );
 
                                  if ( details.actual ) {
                                    output += 'expected: ' + details.expected + ', actual: ' + details.actual;
                                  }
                                  if ( details.source ) {
                                    output += ', ' + details.source;
                                  }

                                    alert('=============================================');
                                    alert( output );
                                });



                                QUnit.config.autostart = false;
                                QUnit.config.ignoreGlobalErrors = true;
                        ");
                #endregion

                engine.Execute(@"   QUnit.load();
                                    QUnit.start();
                ");
                

                
            }
            catch (JavaScriptException ex)
            {
                Console.WriteLine(string.Format("Script error in \'{0}\', line: {1}\n{2}", ex.SourcePath, ex.LineNumber, ex.Message));
                throw ex;
            }
            catch (Exception)
            {

                throw;
            }

            var total = engine.GetGlobalValue<int>("total");
            var sucesso = engine.GetGlobalValue<int>("sucesso");

            Assert.AreEqual(total, sucesso);

        }

        /// <summary>
        /// Delegate para mensages de alerta no código
        /// </summary>
        /// <param name="message"></param>
        private delegate void DAlertDelegate(string message);
    }



}
