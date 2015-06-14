using System;
using AForge.Genetic;

namespace ConsoleApplication1
{
    /// <summary>
    /// Representa um Gene com as funções do Javascript
    /// </summary>
    public class JavascriptGene : IGPGene
    {
        // random number generator for chromosoms generation
        protected static Random Rand = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Nome da Função
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gene type
        /// </summary>
        public GPGeneType GeneType { get;  set; }
        
        /// <summary>
        /// Quantos argumento esse gene possui
        /// </summary>
        public int ArgumentsCount { get;  set; }
        
        /// <summary>
        /// Numero máximo de argumentos do gene
        /// </summary>
        public int MaxArgumentsCount { get;  set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public JavascriptGene()
        {
            Generate();
        }

        /// <summary>
        /// Clona o gene
        /// </summary>
        /// <returns></returns>
        public IGPGene Clone()
        {
            var clone = new JavascriptGene();
            clone.GeneType = this.GeneType;
            clone.ArgumentsCount = this.ArgumentsCount;
            clone.MaxArgumentsCount = this.MaxArgumentsCount;
            clone.Name = this.Name;

            return clone;
        }

        /// <summary>
        /// Randomize gene with random type and value
        /// </summary>
        public void Generate()
        {
            this.GeneType = (Rand.Next(4) == 3) ? GPGeneType.Argument : GPGeneType.Function;

            //TODO: se for função definir alguma da gramática?
            //TODO: Definir nós?

            //TODO: Esse método é chamado pela classe população para criar novos individuos a cada iteração




        }

        /// <summary>
        /// Randomize gene with random value
        /// </summary>
        public void Generate(GPGeneType type)
        {
            this.GeneType = type;
        }

        /// <summary>
        /// Creates new gene with random type
        /// </summary>
        public IGPGene CreateNew()
        {
            return new JavascriptGene();
        }

        /// <summary>
        /// Creates new gene with certain type
        /// </summary>
        public IGPGene CreateNew(GPGeneType type)
        {
            return new JavascriptGene(){GeneType = type};
        }

        /// <summary>
        /// Escrever a árvore do individuo
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

    }
}
