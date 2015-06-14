﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static string _jsFileTest = @"scriptDataTest.js";
        private static string _functionName = "AvancaDias";
        private static string _javascriptText = "";
        private static CommonTree _tree;
        private static ITree _functionBody;

        [TestFixtureSetUp]
        public void Setup()
        {
            _javascriptText = System.IO.File.ReadAllText(_jsFile);

            #region Build the AST from Js
            var stream = new ANTLRStringStream(_javascriptText);
            var lexer = new ES3Lexer(stream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ES3Parser(tokenStream);
            ES3Parser.program_return programReturn = parser.program();
            _tree = programReturn.Tree as CommonTree;
            #endregion

            _functionBody = FindFunctionNode(_tree, _functionName);
        }

        [Test]
        public void BuildJavascriptChromosomeFromAst()
        {
            var javaChromosome = new JavascriptChromosome(_functionBody);
            Assert.AreEqual(true, true);

        }

        /// <summary>
        /// Descobre qual a função na Tree pelo nome
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="nomeFuncaoOtimizar"></param>
        /// <returns></returns>
        private static ITree FindFunctionNode(CommonTree tree, string nomeFuncaoOtimizar)
        {

            for (int i = 0; i < tree.ChildCount; i++)
            {
                var funcaoAtual = tree.GetChild(i);

                if (funcaoAtual.GetChild(0).Text == nomeFuncaoOtimizar)
                    return funcaoAtual;
            }

            return null;
        }

    }
}