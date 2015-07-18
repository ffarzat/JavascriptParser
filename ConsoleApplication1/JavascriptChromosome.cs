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
            get { return Refresh(_tree); }
        }

        /// <summary>
        /// generated code for Chromosome
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// List of javascript instructions
        /// </summary>
        private List<ITree> _possibleFunctions;
        
        // random number generator for chromosoms generation
        protected static Random Rand = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Id of individual inside a population
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Javascript File path
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Generation Parent Id
        /// </summary>
        public int GenerationId { get; set; }

        /// <summary>
        /// Chromosome's fintess value
        /// </summary>
        public double Fitness { get; set; }

        /// <summary>
        /// Starts a new instance based on Tree AST
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="functionName"></param>
        public JavascriptChromosome(CommonTree tree, string functionName)
        {
            Fitness = 0;
            _tree = JavascriptAstCodeGenerator.DeepClone(tree);
            _functionName = functionName;
            _possibleFunctions = JavascriptAstCodeGenerator.Functions;
            _function = JavascriptAstCodeGenerator.DeepClone(JavascriptAstCodeGenerator.FindFunctionTree(_tree,functionName) as CommonTree);
        }

        /// <summary>
        /// Overrides ToString 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Code;
        }

        /// <summary>
        /// Replaces the Function Node inside tree (after operators)
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private CommonTree Refresh(CommonTree tree)
        {
            var function = JavascriptAstCodeGenerator.FindFunctionTree(tree, _functionName);

            function.ReplaceChildren(2, 2, _function.GetChild(2));

            return tree;
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

            var opt = Rand.Next(0, 2);

            if(opt == 0)
                newChromosome.Mutate();
            else
                newChromosome.Delete();
            
            return newChromosome;
        }

        /// <summary>
        /// Clone the chromosome
        /// </summary>
        public IChromosome Clone()
        {
            return new JavascriptChromosome(_tree, _functionName ){ Fitness = 0, Id = Guid.NewGuid(), GenerationId = this.GenerationId};
        }

        /// <summary>
        /// Mutation operator (replaces a function node by another one)
        /// </summary>
        public void Mutate()
        {
            
            int tries = 10;
            Fitness = 0;

            bool sinal = true;
            int count = 0;

            //Try to cross a function node
            while (sinal)
            {
                if (count >= tries)
                {
                    Log.WriteLine(string.Format("       Desistiu da mutação após {0}", count), LogType.Debug);
                    sinal = false;
                }

                int functionRand = Rand.Next(0, _possibleFunctions.Count); //choose a function from entire body

                var blockDad = _function.GetChild(2);
                var blockMom = _possibleFunctions[functionRand].GetChild(2);

                if (blockDad.ChildCount == 0 || blockMom.ChildCount == 0)
                    break;

                int dadLine = Rand.Next(0, blockDad.ChildCount); //at line instruction
                int momLine = Rand.Next(0, blockMom.ChildCount); //at line instruction


                if (blockDad.GetChild(dadLine).ChildCount == 0 || blockMom.GetChild(momLine).ChildCount == 0)
                    break;

                int dadPoint = Rand.Next(0, blockDad.GetChild(dadLine).ChildCount); //at instruction Level
                int momPoint = Rand.Next(0, blockMom.GetChild(momLine).ChildCount); //at instruction Level


                var functionNodeDad = blockDad.GetChild(dadLine).GetChild(dadPoint);//function node at Dad
                var functionNodeMom = blockMom.GetChild(momLine).GetChild(momPoint);//function node at Mom

                if (JavascriptAstCodeGenerator.IsFunction(functionNodeDad) && (JavascriptAstCodeGenerator.IsFunction(functionNodeMom)))
                {
                    blockDad.GetChild(dadLine).ReplaceChildren(dadPoint, dadPoint, functionNodeMom);
                    Log.WriteLine(string.Format("       Mutação da instrução {0} pela {1}", functionNodeMom.ToStringTree(), functionNodeDad.ToStringTree()), LogType.Debug);
                    sinal = false;
                }

                count++;
            }
        }

        /// <summary>
        /// Delete operator
        /// </summary>
        public void Delete()
        {
            Fitness = 0;
            int tries = 10;
            int count = 0;
            bool sinal = true;

            while (sinal)
            {
                sinal = DeleteInsideBlock(_function.GetChild(2));
                count++;
                if (count >= tries)
                {
                    Log.WriteLine(string.Format("       Desistiu do delete após {0} tentativas", count), LogType.Debug);
                    sinal = false;
                }
            }
        }

        /// <summary>
        /// Finds a node to delete
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        private bool DeleteInsideBlock(ITree function)
        {
            var block = function;
            bool sinal = true;

            int lineLevelToDelete = Rand.Next(0, block.ChildCount); //at line Level

            if (block.ChildCount == 0)
                return true;

            int instructionLevelToDelete = Rand.Next(0, block.GetChild(lineLevelToDelete).ChildCount); //at instruction Level

            var functionToDelete = block.GetChild(lineLevelToDelete).GetChild(instructionLevelToDelete);

            if (JavascriptAstCodeGenerator.IsFunction(functionToDelete))
            {
                if (JavascriptAstCodeGenerator.IsFunction(functionToDelete.Parent) && functionToDelete.Parent.Type != 113) //Not block and is a Function? Delete entire line
                {

                    if (functionToDelete.Type == 113)
                    {
                       sinal = DeleteInsideBlock(functionToDelete);
                    }
                    else
                    {
                        block.DeleteChild(lineLevelToDelete);
                        sinal = false;
                        Log.WriteLine(string.Format("       Delete da instrução {0}", functionToDelete.ToStringTree()), LogType.Debug);
                    }

                }
                else
                {
                    block.GetChild(lineLevelToDelete).DeleteChild(instructionLevelToDelete);
                    sinal = false;
                    Log.WriteLine(string.Format("       Delete da instrução {0}", functionToDelete.ToStringTree()), LogType.Debug);
                }
            }

            return sinal;
        }

        /// <summary>
        /// Crossover operator
        /// </summary>
        /// <remarks>
        /// Do the crossover (function) between pair
        /// </remarks>
        public  void Crossover(IChromosome pair)
        {
            var javascriptChromosomePair = (JavascriptChromosome)pair;
            int tries = 10;
            Fitness = 0;
            javascriptChromosomePair.Fitness = 0;

            bool sinal = true;
            int count = 0;
            
            //Try to cross a function node
            while (sinal)
            {
                if (count >= tries)
                {
                    Log.WriteLine(string.Format("       Desistiu do crossover após {0} tentativas", count), LogType.Debug);
                    sinal = false; 
                }


                var blockDad = _function.GetChild(2);
                var blockMom = javascriptChromosomePair.Function.GetChild(2);

                if (blockDad.ChildCount == 0 || blockMom.ChildCount == 0)
                    break;
                
                int dadLine = Rand.Next(0, blockDad.ChildCount); //at line instruction
                int momLine = Rand.Next(0, blockMom.ChildCount); //at line instruction

                if (blockDad.GetChild(dadLine).ChildCount == 0 || blockMom.GetChild(momLine).ChildCount == 0)
                    break;

                int dadPoint = Rand.Next(0, blockDad.GetChild(dadLine).ChildCount); //at instruction Level
                int momPoint = Rand.Next(0, blockMom.GetChild(momLine).ChildCount); //at instruction Level
                
                var functionNodeDad = blockDad.GetChild(dadLine).GetChild(dadPoint);//function node at Dad
                var functionNodeMom = blockMom.GetChild(momLine).GetChild(momPoint);//function node at Mom

                if (JavascriptAstCodeGenerator.IsFunction(functionNodeDad) && (JavascriptAstCodeGenerator.IsFunction(functionNodeMom)))
                {
                    blockDad.GetChild(dadLine).ReplaceChildren(dadPoint, dadPoint, functionNodeMom);

                    blockMom.GetChild(momLine).ReplaceChildren(momPoint, momPoint, functionNodeDad);

                    sinal = false;
                    Log.WriteLine(string.Format("       Crossover Mãe: '{0}' - Pai: '{1}'", functionNodeMom.ToStringTree(), functionNodeDad.ToStringTree()), LogType.Debug);
                }

                count++;
            }

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
