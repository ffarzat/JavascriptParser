using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            const string qunitFile = "qunit-1.18.0.js";
            const string fileMomentPath = "moment.js";
            const string fileGeneratedCode = "target.js";
            
            var sw = new Stopwatch();
            var swTotal = new Stopwatch();

            swTotal.Start();

            sw.Start();
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
            sw.Stop();
            Console.WriteLine("{0} - {1}", "AST criada", sw.Elapsed.ToString("mm\\:ss\\.ff"));

            sw.Reset();
            sw.Start();
            var engine = new Jurassic.ScriptEngine();
            engine.SetGlobalFunction("alert", new DAlertDelegate(Console.WriteLine));
            
            sw.Stop();
            Console.WriteLine("{0} - {1}", "Engine criada e configurada", sw.Elapsed.ToString("mm\\:ss\\.ff"));

            sw.Reset();
            sw.Start();
            engine.ExecuteFile(fileGeneratedCode);
            sw.Stop();
            Console.WriteLine("{0} - {1}", "Individuo novo carregado", sw.Elapsed.ToString("mm\\:ss\\.ff"));
            
            sw.Reset();
            sw.Start();
            engine.ExecuteFile(qunitFile);
            Console.WriteLine("{0} - {1}", qunitFile, sw.Elapsed.ToString("mm\\:ss\\.ff"));
            sw.Stop();

            sw.Reset();
            sw.Start();
            engine.ExecuteFile(jsTestFile);
            sw.Stop();
            Console.WriteLine("{0} - {1}", jsTestFile, sw.Elapsed.ToString("mm\\:ss\\.ff"));
            

            try
            {
                #region registra os retornos dos testes
                sw.Reset();
                sw.Start();
                
                engine.Execute(@"   

                                    QUnit.config.autostart = false;
                                    QUnit.config.ignoreGlobalErrors = true;
                                    QUnit.config.moduleFilter = 'zones';

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
                                QUnit.moduleDone(function( details ) {
                                    if(details.failed > 0)
                                    {
                                        alert('=============================================');
                                        alert('Modulo:' + details.name);
                                        alert('Testes:' + details.tests);
                                        alert('Falha:' + details.failed);
                                        alert('Sucesso:' + details.passed);
                                        alert('Total:' + details.total);
                                        alert('Tempo:' + details.runtime);
                                    }
                                });
*/


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


                        ");

                sw.Stop();
                Console.WriteLine("{0} - {1}", "Qunit config", sw.Elapsed.ToString(@"m\:ss"));
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

            
            swTotal.Stop();
            Console.WriteLine("{0} - {1}", "Tempo total", swTotal.Elapsed.ToString("mm\\:ss\\.ff"));
            Assert.AreEqual(total, sucesso);

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


        /// <summary>
        /// Underscore Code regenaration //versão 1.8.3 em 04/08/2015
        /// </summary>
        [Test]
        public void ToUnderScoreCodeTest()
        {
            
            var momentTextWithoutComments = File.ReadAllText("underscore.js"); //underscore sem comentários

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

            File.WriteAllText("underscoreGeneratedCode.js", generatedJsCode);

            var originalText = momentTextWithoutComments.Replace(" ", "").Replace("\n", "").Replace("\t", "").Replace("\r", "");
            var generatedText = generatedJsCode.Replace(" ", "").Replace("\n", "").Replace("\t", "").Replace("\r", "");

            Assert.AreEqual(originalText, generatedText);

            try
            {
                var engine = new Jurassic.ScriptEngine();
                engine.SetGlobalFunction("alert", new DAlertDelegate(Console.WriteLine));
                engine.ExecuteFile("underscoreGeneratedCode.js");
            }
            catch (JavaScriptException jex)
            {
                Console.WriteLine(string.Format("Script error in \'{0}\', line: {1}\n{2}", jex.SourcePath, jex.LineNumber, jex.Message));
                throw;

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.ToString());
                throw;
            }

        }

        /// <summary>
        /// Underscore unit tests
        /// </summary>
        [Test]
        public void RunTestsFromUnderscore()
        {

            const string jsTestFile = "underscoreTests.js";
            const string qunitFile = "qunit-1.18.0.js";
            const string fileMomentPath = "underscore.js";
            const string fileGeneratedCode = "underscoreGeneratedFortests.js";

            var sw = new Stopwatch();
            var swTotal = new Stopwatch();

            swTotal.Start();

            sw.Start();
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
            sw.Stop();
            Console.WriteLine("{0} - {1}", "AST criada", sw.Elapsed.ToString("mm\\:ss\\.ff"));

            sw.Reset();
            sw.Start();
            var engine = new Jurassic.ScriptEngine();
            engine.SetGlobalFunction("alert", new DAlertDelegate(Console.WriteLine));
            engine.SetGlobalFunction("esperar", new DEsperar(Esperar));
            engine.SetGlobalValue("global", engine.Global);
            engine.Execute("setTimeout = function (funcToCall, millis) { esperar(millis);  funcToCall(); };");

            sw.Stop();
            Console.WriteLine("{0} - {1}", "Engine criada e configurada", sw.Elapsed.ToString("mm\\:ss\\.ff"));

            sw.Reset();
            sw.Start();
            engine.ExecuteFile(fileGeneratedCode);
            sw.Stop();
            Console.WriteLine("{0} - {1}", "Individuo novo carregado", sw.Elapsed.ToString("mm\\:ss\\.ff"));

            sw.Reset();
            sw.Start();
            engine.ExecuteFile(qunitFile);
            Console.WriteLine("{0} - {1}", qunitFile, sw.Elapsed.ToString("mm\\:ss\\.ff"));
            sw.Stop();

            sw.Reset();
            sw.Start();
            engine.ExecuteFile(jsTestFile);
            sw.Stop();
            Console.WriteLine("{0} - {1}", jsTestFile, sw.Elapsed.ToString("mm\\:ss\\.ff"));

            try
            {
                #region registra os retornos dos testes
                sw.Reset();
                sw.Start();

                engine.Execute(@"   

                                    QUnit.config.autostart = false;
                                    QUnit.config.ignoreGlobalErrors = true;
                                    QUnit.config.moduleFilter = 'zones';

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
                                QUnit.moduleDone(function( details ) {
                                    if(details.failed > 0)
                                    {
                                        alert('=============================================');
                                        alert('Modulo:' + details.name);
                                        alert('Testes:' + details.tests);
                                        alert('Falha:' + details.failed);
                                        alert('Sucesso:' + details.passed);
                                        alert('Total:' + details.total);
                                        alert('Tempo:' + details.runtime);
                                    }
                                });
*/


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


                        ");

                sw.Stop();
                Console.WriteLine("{0} - {1}", "Qunit config", sw.Elapsed.ToString(@"m\:ss"));
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


            swTotal.Stop();
            Console.WriteLine("{0} - {1}", "Tempo total", swTotal.Elapsed.ToString("mm\\:ss\\.ff"));
            Assert.AreEqual(total, sucesso);

        }

        private void Esperar( int miliseconds)
        {
            System.Threading.Thread.Sleep(miliseconds);
        }

        private delegate void DEsperar(int miliseconds);

    }
}
