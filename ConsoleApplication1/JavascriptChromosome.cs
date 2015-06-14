using System;
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

        // tree root
        private GPTreeNode _root = new GPTreeNode();

        // maximum initial level of the tree
        private int _maxInitialLevel = 10;
        
        // maximum level of the tree
        private int _maxLevel = 5000;

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
        /// Creates a Tree 
        /// </summary>
        /// <param name="function"></param>
        private void BuildGenesFrom(ITree function)
        {
            var functionBlock = function.GetChild(2);

            SetupRoot(function);

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
        private JavascriptGene BuildGene(ITree instructionLine)
        {
            var gene = new JavascriptGene
                {
                    GeneType = IsFunction(instructionLine) ? GPGeneType.Function : GPGeneType.Argument,
                    Name = instructionLine.Text
                };

            if (gene.GeneType == GPGeneType.Function)
            {
                //Do recursive way
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
                    Name = functionBlock.Text,
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

        #region IChromosome implementation
        /// <summary>
        /// Constructor
        /// </summary>
        public JavascriptChromosome()
        {
            
        }

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clone the chromosome
        /// </summary>
        public IChromosome Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mutation operator
        /// </summary>
        public void Mutate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Crossover operator
        /// </summary>
        public void Crossover(IChromosome pair)
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
