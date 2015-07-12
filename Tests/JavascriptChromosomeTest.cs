using System;
using System.IO;
using AForge.Genetic;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using ConsoleApplication1;
using NUnit.Framework;
using Xebic.Parsers.ES3;
using unvell.ReoScript;

namespace Tests
{
    [TestFixture]
    public class JavascriptChromosomeTest
    {
        private static string _jsFile = @"scriptData.js";
        private static string _jsFileWithouComments = @"scriptData_sem_comentarios.js";
        private static string _jsFileTest = @"scriptDataTest.js";
        private static string _functionName = "AvancaDias";
        private static string _javascriptText = "";
        private static string _javascriptTextWithoutComments = "";
        private static CommonTree _tree;
        private static string _JsQUnitFile = "qunit-1.18.0.js";

        [TestFixtureSetUp]
        public void Setup()
        {
            _javascriptText = System.IO.File.ReadAllText(_jsFile);
            _javascriptTextWithoutComments = System.IO.File.ReadAllText(_jsFileWithouComments);

            #region Build the AST from Js
            var stream = new ANTLRStringStream(_javascriptText);
            var lexer = new ES3Lexer(stream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ES3Parser(tokenStream);
            ES3Parser.program_return programReturn = parser.program();
            _tree = programReturn.Tree as CommonTree;
            #endregion
        }

        //Covers CreateOffspring
        [Test]
        public void CreateOffspring()
        {
            var javaChromosome = new JavascriptChromosome(_tree, _functionName);
            var newJavaChromosome = javaChromosome.CreateOffspring();
            Assert.AreNotEqual(newJavaChromosome, javaChromosome);
        }

        //Covers mutation
        [Test]
        public void Mutate()
        {
            var javaChromosome = new JavascriptChromosome(_tree, _functionName);
            var totalLines = javaChromosome.Function.ChildCount;
            javaChromosome.Mutate();
            var totalLinesAfter = javaChromosome.Function.ChildCount;
            Assert.AreEqual(totalLines, totalLinesAfter);

            var codeGenerator = new JavascriptAstCodeGenerator(javaChromosome.Tree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();
            File.WriteAllText("generatedJsCode_Mutate.js", generatedJsCode);
        }

        //Covers Clone
        [Test]
        public void Clone()
        {
            var javaChromosome = new JavascriptChromosome(_tree, _functionName);
            var newJavaChromosome = javaChromosome.Clone() as JavascriptChromosome;

            Assert.AreNotEqual(javaChromosome, newJavaChromosome);
            Assert.AreEqual(newJavaChromosome.Fitness, javaChromosome.Fitness);

            var codeGenerator = new JavascriptAstCodeGenerator(newJavaChromosome.Tree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();

            var originalText = _javascriptTextWithoutComments.Replace(" ", "").Replace("\r\n", "");
            var generatedText = generatedJsCode.Replace(" ", "").Replace("\r\n", "");
            Assert.AreEqual(originalText, generatedText);
        }

        //Covers Clone not equal each other
        [Test]
        public void CloneNotEqual()
        {
            var javaChromosome = new JavascriptChromosome(_tree, _functionName);
            IFitnessFunction fitness = new JavascriptFitness(javaChromosome, Environment.CurrentDirectory, _jsFile, _JsQUnitFile);
            javaChromosome.Evaluate(fitness);
            
            var newJavaChromosome = javaChromosome.Clone() as JavascriptChromosome;
            
            Assert.AreNotEqual(javaChromosome, newJavaChromosome);
            Assert.AreNotEqual(newJavaChromosome.Fitness, javaChromosome.Fitness);
            
            newJavaChromosome.Evaluate(fitness);
            Assert.AreNotEqual(newJavaChromosome.Fitness, javaChromosome.Fitness);

            //Do Delete and compare again
            var newDeletedChromosome = newJavaChromosome.Clone() as JavascriptChromosome;
            newDeletedChromosome.Delete(); 

            Assert.AreNotEqual(newJavaChromosome.Fitness, newDeletedChromosome.Fitness);
            Assert.AreNotEqual(newJavaChromosome.Code, newDeletedChromosome.Code);
            
            newDeletedChromosome.Evaluate(fitness);
            Assert.AreNotEqual(newJavaChromosome.Code, newDeletedChromosome.Code);

            
        }
        
        //Covers Delete operator
        [Test]
        public void Delete()
        {
            var javaChromosome = new JavascriptChromosome(_tree, _functionName);
            javaChromosome.Delete();

            var codeGenerator = new JavascriptAstCodeGenerator(javaChromosome.Tree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();
            
            File.WriteAllText("generatedJsCode_Delete.js", generatedJsCode);

            Assert.AreNotEqual(_javascriptTextWithoutComments, generatedJsCode);

        }

        //Compile the Js code
        [Test]
        public void ToCompileCodeTest()
        {
            ScriptRunningMachine scriptRunning = new ScriptRunningMachine();

            var codeGenerator = new JavascriptAstCodeGenerator(_tree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();

            var result = scriptRunning.Compile(generatedJsCode);
            Assert.AreEqual(0, result.CompilingErrors.Count);
        }

        //Compile the Js code if erros
        [Test]
        public void ToCompileCodeTestFail()
        {
            ScriptRunningMachine scriptRunning = new ScriptRunningMachine();

            var codeGenerator = new JavascriptAstCodeGenerator(_tree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();

            Assert.Throws<ReoScriptCompilingException>(() => scriptRunning.Compile(generatedJsCode + "EROROROROROROR"));
        }


        //Execs the Js
        [Test]
        public void ToExecCodeTest()
        {
            ScriptRunningMachine scriptRunning = new ScriptRunningMachine();
            var codeGenerator = new JavascriptAstCodeGenerator(_tree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();

            scriptRunning.AllowDirectAccess = true;
            scriptRunning.Load(_jsFile);
            scriptRunning["print"] = new NativeFunctionObject("print", (ctx, owner, args) =>
            {
                Console.WriteLine(args[0]);
                return null;
            });
            scriptRunning.Run(new FileInfo(_jsFileTest));

            Assert.AreEqual("3/5/2015", scriptRunning["stringData1"]);
            Assert.AreEqual("4/5/2015", scriptRunning["stringData2"]);
            Assert.AreEqual("5/5/2015", scriptRunning["stringData3"]);
            Assert.AreEqual("6/5/2015", scriptRunning["stringData4"]);
            Assert.AreEqual("7/5/2015", scriptRunning["stringData5"]);
            


        }

    }
}
