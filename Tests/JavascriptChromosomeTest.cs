using System;
using System.IO;
using AForge.Genetic;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using ConsoleApplication1;
using NUnit.Framework;
using Xebic.Parsers.ES3;

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
            
            javaChromosome.Mutate();
            
            var codeGenerator = new JavascriptAstCodeGenerator(javaChromosome.Tree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();
            File.WriteAllText("generatedJsCode_Mutate.js", generatedJsCode);
            Assert.AreNotEqual(javaChromosome.Code, _javascriptTextWithoutComments);
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


        [Test]
        public void DoOperationAndChangeTree()
        {
            var javaChromosome = new JavascriptChromosome(_tree, _functionName);
            IFitnessFunction fitness = new JavascriptFitness(javaChromosome, Environment.CurrentDirectory, _jsFile, _JsQUnitFile);
            
            var fitvalue = javaChromosome.Fitness;
            var originalTree = JavascriptAstCodeGenerator.DeepClone(javaChromosome.Tree);
            var originalFunction = JavascriptAstCodeGenerator.DeepClone(javaChromosome.Function as CommonTree);

            javaChromosome.Mutate();
            javaChromosome.Mutate();
            javaChromosome.Mutate();
            javaChromosome.Evaluate(fitness);
            
            Assert.AreNotEqual(fitvalue, javaChromosome.Fitness);
            Assert.AreNotEqual(JavascriptAstCodeGenerator.CountInstructionsOf(originalTree), JavascriptAstCodeGenerator.CountInstructionsOf(javaChromosome.Tree));
            Assert.AreNotEqual(JavascriptAstCodeGenerator.CountInstructionsOf(originalFunction), JavascriptAstCodeGenerator.CountInstructionsOf(javaChromosome.Function));
        }

    }
}
