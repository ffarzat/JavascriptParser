using System;
using System.Collections;
using System.Collections.Generic;
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
        /// 
        /// </summary>
        private static List<string> _possibleFunctions = BuildFunctionList();

        /// <summary>
        /// Total size of the tree
        /// </summary>
        private int _totalLevel = 0;

        // tree root
        private GPTreeNode _root = new GPTreeNode();

        // random number generator for chromosoms generation
        protected static Random rand = new Random((int)DateTime.Now.Ticks);

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
            _function = function;
            BuildGenesFrom(function);
        }

        /// <summary>
        /// Reads the Tokens file and process a lista of possible functions
        /// </summary>
        /// <returns></returns>
        private static List<string> BuildFunctionList()
        {
            throw new NotImplementedException();
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
        /// Generate chromosome's subtree of specified level
        /// </summary>
        private void Generate(GPTreeNode node, int level)
        {
            // create gene for the node
            if (level == 0)
            {
                // the gene should be an argument
                node.Gene = _root.Gene.CreateNew(GPGeneType.Argument);
            }
            else
            {
                // the gene can be function or argument
                node.Gene = _root.Gene.CreateNew();
            }

            // add children
            if (node.Gene.ArgumentsCount != 0)
            {
                node.Children = new ArrayList();
                for (int i = 0; i < node.Gene.ArgumentsCount; i++)
                {
                    // create new child
                    GPTreeNode child = new GPTreeNode();
                    Generate(child, level - 1);
                    // add the new child
                    node.Children.Add(child);
                }
            }
        }

        #region IChromosome implementation

        /// <summary>
        /// Generate random chromosome value
        /// </summary>
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
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
            //TODO: randomicamente criar um novo individuo a partir do original (aqui agora é basicamente um clone!)
            var newChromosome = new JavascriptChromosome(this._function);
            newChromosome.Mutate();
            return newChromosome;
        }

        /// <summary>
        /// Clone the chromosome
        /// </summary>
        public IChromosome Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mutation operator (replaces a function node by another one)
        /// </summary>
        public void Mutate()
        {
            int instructionLevelToMutate = rand.Next(0, _root.Children.Count); //at line instruction
            var functionNode = _root.Children[instructionLevelToMutate] as GPTreeNode;
            int levelToMutate = rand.Next(0, functionNode.Children.Count); //at level 

            var functionToMutate = functionNode.Children[levelToMutate] as GPTreeNode;
            functionToMutate.Gene = new JavascriptGene()
                {
                    GeneType = GPGeneType.Function,
                    Name = "MUTANT"
                };

        }

        /// <summary>
        /// Crossover operator
        /// </summary>
        public  void Crossover(IChromosome pair)
        {
            throw new NotImplementedException();
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
