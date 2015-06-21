using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Genetic;
using Antlr.Runtime;
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
        private IDictionary<string, string> _possibleFunctions;
        
        // random number generator for chromosoms generation
        protected static Random Rand = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Id of individual inside a population
        /// </summary>
        public int Id { get; set; }

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
            _tree = DeepClone(tree);
            _functionName = functionName;
            _possibleFunctions = BuildFunctionList(); //TODO: rever isso aqui. Pool mais forte de funções tipadas para substituição na mutação
            _function = JavascriptAstCodeGenerator.FindFunctionTree(_tree, functionName).GetChild(2);
        }

        /// <summary>
        /// Creates a Clone of a Tree
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private CommonTree DeepClone(CommonTree tree)
        {
            var root = tree.DupNode();

            for (int i = 0; i < tree.ChildCount; i++)
            {
                var clonedChild = DeepClone(tree.GetChild(i));
                root.AddChild(clonedChild);
            }

            return root as CommonTree;
        }

        /// <summary>
        /// Creates a Deep Clone of a ITree node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private ITree DeepClone(ITree node)
        {
            var cloneNode = node.DupNode();

            for (int i = 0; i < node.ChildCount; i++)
            {
                var cloneChildNode = DeepClone(node.GetChild(i)); //gets a dupNode cloned
                cloneNode.AddChild(cloneChildNode); //adds
            }

            return cloneNode;
        }


        /// <summary>
        /// Reads the Tokens file and process a lista of possible functions
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> BuildFunctionList()
        {
            var sr = System.IO.File.OpenText(InstructionsFile);
            string line = "";
            var list = new Dictionary<string, string>();
            int lineCount = 0;

            while ((line = sr.ReadLine()) != null && lineCount <=168)
            {
                lineCount++;
                var functionName = line.Split(" = ".ToCharArray())[0];
                var functionNumber = line.Split(" = ".ToCharArray())[1];
                
                if (JavascriptAstCodeGenerator.IsFunction(int.Parse(functionNumber)))
                    list.Add(functionName, functionNumber);
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
            #region Defines Target Function to Mutate
            int indexTargetFunction = Rand.Next(0, _possibleFunctions.Count);          //target mutation function
            var targetFunction = _possibleFunctions.ElementAt(indexTargetFunction);
            #endregion

            bool sinal = true;

            while (sinal)
            {
                int instructionLevelToDelete = Rand.Next(0, _function.ChildCount); //at line instruction
                var functionToDelete = _function.GetChild(instructionLevelToDelete);

                if (JavascriptAstCodeGenerator.IsFunction(functionToDelete))
                {

                    #region Keeps all children nodes
                    var listOfChildren = new List<ITree>();

                    for (int i = 0; i < functionToDelete.ChildCount; i++)
                    {
                        listOfChildren.Add(functionToDelete.GetChild(i));

                    }
                    #endregion

                    #region Creates The New one

                    var newOne = new CommonTree(new CommonToken()
                        {
                            Text = targetFunction.Key,
                            Type = int.Parse(targetFunction.Value),
                        });

                    //Add all chidren
                    listOfChildren.ForEach(newOne.AddChild);

                    #endregion

                    #region Replaces it


                    _function.ReplaceChildren(instructionLevelToDelete, instructionLevelToDelete, newOne);

                    sinal = false;

                    #endregion
                }

            }
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
