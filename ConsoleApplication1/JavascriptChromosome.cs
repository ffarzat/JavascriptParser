using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Genetic;

namespace ConsoleApplication1
{
    public class JavascriptChromosome : IChromosome
    {
        /// <summary>
        /// Chromosome's fintess value
        /// </summary>
        public double Fitness { get; private set; }

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
            throw new NotImplementedException();
        }
    }
}
