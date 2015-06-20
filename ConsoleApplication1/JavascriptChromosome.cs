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
        /// Keeps the name of a function to Optmize
        /// </summary>
        private string _functionName;

        /// <summary>
        /// Keeps the AST Block from function
        /// </summary>
        private ITree _function;

        /// <summary>
        /// Function Block Tree of Chromosome
        /// </summary>
        public ITree Function {
            get { return _function; }
        }

        /// <summary>
        /// Keeps the original AST tree
        /// </summary>
        private CommonTree _tree;

        /// <summary>
        /// Full Tree
        /// </summary>
        public CommonTree Tree
        {
            get { return _tree; }
        }

        /// <summary>
        /// Parser generated file
        /// </summary>
        private const string InstructionsFile = "ES3.tokens";

        /// <summary>
        /// List of javascript instructions
        /// </summary>
        private List<string> _possibleFunctions;
        
        // random number generator for chromosoms generation
        protected static Random Rand = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Chromosome's fintess value
        /// </summary>
        public double Fitness { get; private set; }

        /// <summary>
        /// Starts a new instance based on Tree AST
        /// </summary>
        /// <param name="functionName"></param>
        public JavascriptChromosome(CommonTree tree, string functionName)
        {
            _tree = tree;
            _functionName = functionName;
            _possibleFunctions = BuildFunctionList(); //TODO: rever isso aqui. Pool mais forte de funções tipadas para substituição na mutação
            _function = JavascriptAstCodeGenerator.FindFunctionTree(_tree, functionName).GetChild(2);
        }

        /// <summary>
        /// Reads the Tokens file and process a lista of possible functions
        /// </summary>
        /// <returns></returns>
        private List<string> BuildFunctionList()
        {
            var sr = System.IO.File.OpenText(InstructionsFile);
            string line = "";
            var list = new List<string>();
            int lineCount = 0;

            while ((line = sr.ReadLine()) != null && lineCount <=168)
            {
                lineCount++;
                var functionName = line.Split(" = ".ToCharArray())[0];
                var functionNumber = line.Split(" = ".ToCharArray())[1];
                if (JavascriptAstCodeGenerator.IsFunction(int.Parse(functionNumber)))
                    list.Add(functionName);

            }

            sr.Close();

            return list;
        }

        /// <summary>
        /// Overrides ToString 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _function.ToStringTree();
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
            var newChromosome = this.Clone();
            newChromosome.Mutate();
            return newChromosome;
        }

        /// <summary>
        /// Clone the chromosome
        /// </summary>
        public IChromosome Clone()
        {
            return new JavascriptChromosome(this._tree, _functionName ){ Fitness = this.Fitness};
        }

        /// <summary>
        /// Mutation operator (replaces a function node by another one)
        /// </summary>
        public void Mutate()
        {
            #region Defines Line and instruction to Mutate
            int lineLevelToMutate = Rand.Next(0, _function.ChildCount); //at line instruction
            var functionNode = _function.GetChild(lineLevelToMutate);
            int instructionLevelToMutate = Rand.Next(0, functionNode.ChildCount); //at level <--------------------------------------------------------------------
            #endregion
            
            #region Defines Target Function to Mutate
            int indexTargetFunction = Rand.Next(0, _possibleFunctions.Count);//target mutation function
            var targetFunction = _possibleFunctions.ElementAt(indexTargetFunction);
            #endregion

            var instructionToMutate = functionNode.GetChild(instructionLevelToMutate) as CommonTree;

            instructionToMutate.Text = targetFunction;
            instructionToMutate.Type = 1;

            functionNode.ReplaceChildren(0, instructionLevelToMutate, instructionToMutate);
            
        }

        /// <summary>
        /// Delete operator
        /// </summary>
        public void Delete()
        {
            bool sinal = true;

            while (sinal)
            {
                int instructionLevelToDelete = Rand.Next(0,  _function.ChildCount); //at line instruction
                var functionToDelete = _function.GetChild(instructionLevelToDelete);

                if (JavascriptAstCodeGenerator.IsFunction(functionToDelete))
                {
                    if (JavascriptAstCodeGenerator.IsFunction(functionToDelete.Parent) && functionToDelete.Parent.Type != 113) //Not block and is a Function? Delete entire line
                    {
                        _function.Parent.DeleteChild(0);
                        sinal = false;
                    }
                    else
                    {
                        _function.DeleteChild(instructionLevelToDelete);
                        sinal = false;
                    }
                }
            }
        }

        /// <summary>
        /// Crossover operator
        /// </summary>
        public  void Crossover(IChromosome pair)
        {
            //var javascriptChromosomePair = (JavascriptChromosome)pair;
            //int cutPointInstructionDad = Rand.Next(0, javascriptChromosomePair.Root.Children.Count); //at line instruction
            //int cutPointInstructionMom = Rand.Next(0, _root.Children.Count); //at line instruction

            //var functionNodeDad = javascriptChromosomePair.Root.Children[cutPointInstructionDad] as GPTreeNode;
            //var functionNodeMom = javascriptChromosomePair.Root.Children[cutPointInstructionMom] as GPTreeNode;

            //int cutPointDad = Rand.Next(0, functionNodeDad.Children.Count); //at instruction
            //int cutPointMom = Rand.Next(0, functionNodeMom.Children.Count); //at instruction

            //var functionToCrossDad = functionNodeDad.Children[cutPointDad] as GPTreeNode;
            //var functionToCrossMom = functionNodeMom.Children[cutPointMom] as GPTreeNode;

            ////Change one per another one
            //functionNodeDad.Children[cutPointDad] = functionToCrossMom;
            //functionNodeMom.Children[cutPointMom] = functionToCrossDad;
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
