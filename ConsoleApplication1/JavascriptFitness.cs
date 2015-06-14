using AForge.Genetic;

namespace ConsoleApplication1
{
    /// <summary>
    /// Representa uma função de fitness do 
    /// </summary>
    public class JavascriptFitness : IFitnessFunction
    {

        /// <summary>
        /// Evaluates chromosome
        /// </summary>
        public double Evaluate(IChromosome chromosome)
        {

            //TODO: gerar o código em um diretório temporário
            //TODO: copiar os testes para lá
            //TODO: Executar os testes no novo Js
            //TODO: Medir o tempo de execução

            //Penalizar o tempo x Qtd de Testes passando

            return 0.5;
        }

        /// <summary>
        /// Translates genotype to phenotype 
        /// </summary>
        public object Translate(IChromosome chromosome)
        {
            return chromosome.ToString();
        }
    }
}
