using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Genetic;
using Antlr.Runtime.Tree;

namespace ConsoleApplication1
{

    /// <summary>
    /// Represents a Javascript Chromosome 
    /// </summary>
    public class JavascriptChromosome : IChromosome
    {
        /// <summary>
        /// Keeps the original AST from function
        /// </summary>
        private ITree _function;

        /// <summary>
        /// Parser generated file
        /// </summary>
        private string _instructionsFile = "ES3.tokens";

        /// <summary>
        /// List of javascript instructions
        /// </summary>
        private List<string> _possibleFunctions;

        /// <summary>
        /// Total size of the tree
        /// </summary>
        private int _totalLevel = 0;

        // tree root
        private GPTreeNode _root = new GPTreeNode();

        /// <summary>
        /// Read Only Tree nodes
        /// </summary>
        public GPTreeNode Root {
            get { return _root; }
        }

        // random number generator for chromosoms generation
        protected static Random Rand = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Chromosome's fintess value
        /// </summary>
        public double Fitness { get; private set; }

        /// <summary>
        /// Starts a new instance based on function AST
        /// </summary>
        /// <param name="function">Function AST</param>
        public JavascriptChromosome(ITree function)
        {
            _possibleFunctions = BuildFunctionList();
            _function = function;
            BuildGenesFrom(function);
        }

        /// <summary>
        /// Reads the Tokens file and process a lista of possible functions
        /// </summary>
        /// <returns></returns>
        private List<string> BuildFunctionList()
        {
            var sr = System.IO.File.OpenText(_instructionsFile);
            string line = "";
            var list = new List<string>();
            int lineCount = 0;

            while ((line = sr.ReadLine()) != null && lineCount <=168)
            {
                lineCount++;
                var functionName = line.Split(" = ".ToCharArray())[0];
                var functionNumber = line.Split(" = ".ToCharArray())[1];
                if (IsFunction(int.Parse(functionNumber)))
                    list.Add(functionName);

            }

            sr.Close();

            return list;
        }

        /// <summary>
        /// Creates a Tree 
        /// </summary>
        /// <param name="function"></param>
        private void BuildGenesFrom(ITree function)
        {
            var functionBlock = function.GetChild(2);
            SetupRoot(function);
            
            _totalLevel = 1;

            for (int i = 0; i < functionBlock.ChildCount; i++)
            {
                var instructionLine = functionBlock.GetChild(i);
                var geneFromInstruction = BuildGene(instructionLine);
                _root.Children.Add(geneFromInstruction);
            }
        }

        /// <summary>
        /// Build a single gene (recursive)
        /// </summary>
        /// <param name="instructionLine"></param>
        private GPTreeNode BuildGene(ITree instructionLine)
        {
            var jsGene = new JavascriptGene
                {
                    GeneType = IsFunction(instructionLine) ? GPGeneType.Function : GPGeneType.Argument,
                    Name = instructionLine.Text
                };

            var gene = new GPTreeNode {Gene = jsGene};

            if (jsGene.GeneType == GPGeneType.Function)
            {
                for (int i = 0; i < instructionLine.ChildCount; i++)
                {
                    var childInstruction = instructionLine.GetChild(i);
                    var childGene = BuildGene(childInstruction);
                    gene.Children.Add(childGene);
                }
                _totalLevel += 1;
            }

            return gene;
        }

        /// <summary>
        /// Make a root GPGPTreeNode
        /// </summary>
        /// <param name="functionBlock"></param>
        private void SetupRoot(ITree functionBlock)
        {
            _root.Gene = new JavascriptGene()
                {
                    Name = "" /*functionBlock.Text*/,
                    GeneType = GPGeneType.Function,
                    MaxArgumentsCount = int.MaxValue
                };
        }

        /// <summary>
        /// Determine if the instruction is a function node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsFunction(ITree node)
        {
            switch (node.Type)
            {
                case 113: //Block
                    return true;
                    break;
                case 116: //Call
                    return true;
                    break;
                case 126: //PAREXPR = {}
                    return true;
                    break;
                default: // Restante
                    if (node.Type >= 7)
                    {
                        if (node.Type <= 110)
                        {
                            return true;
                        }
                    }
                    break;

            }
            return false;
        }

        /// <summary>
        /// Determine if the instruction is a function node
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private bool IsFunction(int number)
        {
            switch (number)
            {
                case 113: //Block
                    return true;
                    break;
                case 116: //Call
                    return true;
                    break;
                case 126: //PAREXPR = {}
                    return true;
                    break;
                default: // Restante
                    if (number >= 7)
                    {
                        if (number <= 110)
                        {
                            return true;
                        }
                    }
                    break;

            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _root.ToString();
        }

        #region IChromosome implementation

        /// <summary>
        /// Generate random chromosome value
        /// </summary>
        public int CompareTo(object obj)
        {
            double f = ((JavascriptChromosome)obj).Fitness;

            return (Fitness == f) ? 0 : (Fitness < f) ? 1 : -1;
        }

        /// <summary>
        /// Generate random chromosome value
        /// </summary>
        public void Generate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create new random chromosome with same parameters (factory method)
        /// </summary>
        public IChromosome CreateOffspring()
        {
            var newChromosome = new JavascriptChromosome(this._function);
            newChromosome.Mutate();
            return newChromosome;
        }

        /// <summary>
        /// Clone the chromosome
        /// </summary>
        public IChromosome Clone()
        {
            return new JavascriptChromosome(this._function){ Fitness = this.Fitness};
        }

        /// <summary>
        /// Mutation operator (replaces a function node by another one)
        /// </summary>
        public void Mutate()
        {
            int instructionLevelToMutate = Rand.Next(0, _root.Children.Count); //at line instruction
            var functionNode = _root.Children[instructionLevelToMutate] as GPTreeNode;
            int levelToMutate = Rand.Next(0, functionNode.Children.Count); //at level 
            int indexTargetFunction = Rand.Next(0, _possibleFunctions.Count);
            var targetFunction = _possibleFunctions.ElementAt(indexTargetFunction);

            var functionToMutate = functionNode.Children[levelToMutate] as GPTreeNode;
            functionToMutate.Gene = new JavascriptGene()
                {
                    GeneType = GPGeneType.Function,
                    Name = targetFunction
                };

        }

        /// <summary>
        /// Crossover operator
        /// </summary>
        public  void Crossover(IChromosome pair)
        {
            var javascriptChromosomePair = (JavascriptChromosome)pair;
            int cutPointInstructionDad = Rand.Next(0, javascriptChromosomePair.Root.Children.Count); //at line instruction
            int cutPointInstructionMom = Rand.Next(0, _root.Children.Count); //at line instruction

            var functionNodeDad = javascriptChromosomePair.Root.Children[cutPointInstructionDad] as GPTreeNode;
            var functionNodeMom = javascriptChromosomePair.Root.Children[cutPointInstructionMom] as GPTreeNode;

            int cutPointDad = Rand.Next(0, functionNodeDad.Children.Count); //at instruction
            int cutPointMom = Rand.Next(0, functionNodeMom.Children.Count); //at instruction

            var functionToCrossDad = functionNodeDad.Children[cutPointDad] as GPTreeNode;
            var functionToCrossMom = functionNodeMom.Children[cutPointMom] as GPTreeNode;

            //Change one per another one
            functionNodeDad.Children[cutPointDad] = functionToCrossMom;
            functionNodeMom.Children[cutPointMom] = functionToCrossDad;
        }

        /// <summary>
        /// Evaluate chromosome with specified fitness function
        /// </summary>
        public void Evaluate(IFitnessFunction function)
        {
            Fitness = function.Evaluate(this);
        }

        #endregion
    }
}
