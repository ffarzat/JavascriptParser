﻿using System.IO;
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
        private static ITree _functionBody;

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

            _functionBody = JavascriptAstCodeGenerator.FindFunctionTree(_tree, _functionName).GetChild(2);
        }

        //Cover the cenario for creates a ancestor Chromosome from AST (ANTLR)
        [Test]
        public void BuildJavascriptChromosomeFromAst()
        {
            var javaChromosome = new JavascriptChromosome(_tree, _functionName);

            Assert.AreEqual(_tree, javaChromosome.Tree);
            Assert.AreEqual(_functionBody, javaChromosome.Function);
        }

        //Covers mutation too
        [Test]
        public void CreateOffspring()
        {
            var javaChromosome = new JavascriptChromosome(_tree, _functionName);
            var newJavaChromosome = javaChromosome.CreateOffspring();
            Assert.AreNotEqual(newJavaChromosome, javaChromosome);

        }

        //Covers Clone
        [Test]
        public void Clone()
        {
            var javaChromosome = new JavascriptChromosome(_tree, _functionName);
            var newJavaChromosome = javaChromosome.Clone();

            Assert.AreEqual(_tree, javaChromosome.Tree);
            Assert.AreEqual(_functionBody, javaChromosome.Function);
            Assert.AreEqual(newJavaChromosome.Fitness, javaChromosome.Fitness);
        }

        //Covers Delete operator
        [Test]
        public void Delete()
        {
            var javaChromosome = new JavascriptChromosome(_tree, _functionName);
            var totalLines = javaChromosome.Function.ChildCount;
            
            javaChromosome.Delete();

            var totalLinesAfter = javaChromosome.Function.ChildCount;

            Assert.AreNotEqual(totalLines, totalLinesAfter);

            //var codeGenerator = new JavascriptAstCodeGenerator(javaChromosome.Tree);
            //var generatedJsCode = codeGenerator.DoCodeTransformation();
            //File.WriteAllText("generatedJsCode_Delete.js", generatedJsCode);

        }


        //Covers ToString Behavior
        [Test]
        public void ToCodeTest()
        {
            var newTree = _tree;
            var codeGenerator = new JavascriptAstCodeGenerator(newTree);
            var generatedJsCode = codeGenerator.DoCodeTransformation();

            //File.WriteAllText("generatedJsCode.js", generatedJsCode);

            var originalText = _javascriptTextWithoutComments.Replace(" ", "").Replace("\r\n", "");
            var generatedText = generatedJsCode.Replace(" ", "").Replace("\r\n", "");

            Assert.AreEqual(originalText, generatedText);
        }

    }
}
