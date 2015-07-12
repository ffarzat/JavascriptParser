// AForge Genetic Library
//
// Copyright © Andrew Kirillov, 2006
// andrew.kirillov@gmail.com
//

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace AForge.Genetic
{
	using System;
	using System.Collections;

	/// <summary>
	/// Population of chromosomes
	/// </summary>
	public class Population
	{

        [DllImport("ntdll.dll", CharSet = CharSet.Auto)]
        public static extern uint NtGetCurrentProcessorNumber();

	    private int[] processorsToUse;
        public bool Parallelism { get; set; }
        
        /// <summary>
        /// Timeout for evaluate an indivudual
        /// </summary>
        public int TimeOut { get; set; }


	    private IFitnessFunction    fitnessFunction;
		private ISelectionMethod    selectionMethod;
        private List<IChromosome>   population = new List<IChromosome>();
		private int			size;
	    private int         _generationCount = 0;
		private double		randomSelectionPortion = 0.6;

		// population parameters
		private double		crossOverRate	= 0.75;
		private double		mutationRate	= 0.10;
        private double      deleteRate      = 0.10;

		// random number generator
		private static Random rand = new Random( (int) DateTime.Now.Ticks );

		//
		private double		fitnessMin = double.MaxValue;
		private double		fitnessSum = 0;
		private double		fitnessAvg = 0;
		private IChromosome	bestChromosome = null;

        /// <summary>
        /// #Processors
        /// </summary>
	    private int _processorsCount;

		/// <summary>
		/// Maximum fitness of the population
		/// </summary>
		public double FitnessMin
		{
			get { return fitnessMin; }
		}

		/// <summary>
		/// Summary fitness of the population
		/// </summary>
		public double FitnessSum
		{
			get { return fitnessSum; }
		}

		/// <summary>
		/// Average fitness of the population
		/// </summary>
		public double FitnessAvg
		{
			get { return fitnessAvg; }
		}

		/// <sumary>
		/// Best chromosome of the population
		/// </sumary>
		public IChromosome BestChromosome
		{
			get { return bestChromosome; }
		}

		/// <sumary>
		/// Size of the population
		/// </sumary>
		public int Size
		{
			get { return size; }
		}

        /// <summary>
        /// Total generations executed
        /// </summary>
	    public int GenerationCount
	    {
	        get { return _generationCount; }
	    }

	    /// <sumary>
		/// Get chromosome with specified index
		/// </sumary>
		public IChromosome this[int index]
		{
			get { return (IChromosome) population[index]; }
		}

	    /// <summary>
		/// Constructor
		/// </summary>
		public Population( int size,IChromosome ancestor, IFitnessFunction fitnessFunction, ISelectionMethod selectionMethod, bool parallel, int timeout)
		{
			this.fitnessFunction = fitnessFunction;
			this.selectionMethod = selectionMethod;
			this.size	= size;
	        this.Parallelism = parallel;
	        this.TimeOut = timeout;

            DiscoverProcessors();
            
		    // add ancestor to the population
			ancestor.Evaluate( fitnessFunction );
	        fitnessMin = ancestor.Fitness; //First Best is the original software
			population.Add( ancestor );

            Console.WriteLine(" Limite superior de valor de Fitness (software original) {0}", fitnessMin);

			// add more chromosomes to the population
			for ( int i = 1; i < size; i++ )
			{
				// create new chromosome
				IChromosome c = ancestor.CreateOffspring( );
			    c.Id = Guid.NewGuid();
			    c.GenerationId = _generationCount;
				// calculate it's fitness
                //c.Evaluate( fitnessFunction ); 
				// add it to population
				population.Add( c );
			}

            //Fit Evaluation in as multiThreading
            ExecuteFitEvaluation(true);



		}

        /// <summary>
        /// #Processors Count
        /// </summary>
	    private void DiscoverProcessors()
	    {
	        #region CPU

            _processorsCount = 1;

	        string processorsStr = System.Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS");

	        if (processorsStr != null)
                _processorsCount = int.Parse(processorsStr);

            Console.WriteLine("{0} processadores detectados", _processorsCount);

            processorsToUse = new int[100]; 

            for (int i = 0; i < _processorsCount-1; i++)
            {
                processorsToUse[i] = i+1;
            }

            Array.Resize(ref processorsToUse, _processorsCount -1);

	        #endregion
	    }

	    /// <summary>
		/// Constructor
		/// </summary>
		public Population( int size,
			IChromosome ancestor,
			IFitnessFunction fitnessFunction,
			ISelectionMethod selectionMethod,
			double randomSelectionPortion ) : this ( size, ancestor, fitnessFunction, selectionMethod, false, 1 )
		{
			this.randomSelectionPortion = Math.Max( 0, Math.Min( 0.5, randomSelectionPortion ) );
		}

		/// <summary>
		/// Regenerate population - feel it with random chromosomes
		/// </summary>
		public void Regenerate( )
		{
			IChromosome ancestor = (IChromosome) population[0];

			// clear population
			population.Clear( );
			// add chromosomes to the population
			for ( int i = 0; i < size; i++ )
			{
				// create new chromosome
				IChromosome c = ancestor.CreateOffspring( );
				// calculate it's fitness
				//c.Evaluate( fitnessFunction );
				// add it to population
				population.Add( c );
			}

            //Fit Evaluation in as multiThreading
            ExecuteFitEvaluation(false);

		}

		/// <summary>
		/// Do crossover in the population
		/// </summary>
		public virtual void Crossover( )
		{
		    int count = 0;

			// crossover
			for ( int i = 1; i < size; i += 2 )
			{
				// generate next random number and check if we need to do crossover
				if ( rand.NextDouble( ) <= crossOverRate )
				{
					// clone both ancestors
					IChromosome c1 = ((IChromosome) population[i - 1]).Clone( );
					IChromosome c2 = ((IChromosome) population[i]).Clone( );

					// do crossover
					c1.Crossover( c2 );

					// calculate fitness of these two offsprings
					//c1.Evaluate( fitnessFunction );
					//c2.Evaluate( fitnessFunction );

					// add two new offsprings to the population
					population.Add( c1 );
					population.Add( c2 );
				    count++;
				}
			}

            Console.WriteLine(" {0} crossover(s) : {1} novo(s) individuo(s)", count, count *2);

		}

		/// <summary>
		/// Do mutation in the population
		/// </summary>
		public virtual void Mutate( )
		{
		    int count = 0;
			// mutate
			for ( int i = 0; i < size; i++ )
			{
				// generate next random number and check if we need to do mutation
				if ( rand.NextDouble( ) <= mutationRate )
				{
					// clone the chromosome
					IChromosome c = ((IChromosome) population[i]).Clone( );
					// mutate it
					c.Mutate( );
					// calculate fitness of the mutant
					//c.Evaluate( fitnessFunction );
					// add mutant to the population
					population.Add( c );
				    count++;
				}
			}

            Console.WriteLine(" {0} mutation(s) : {1} novo(s) individuo(s)", count, count);
		}

        /// <summary>
        /// Do Delete in the population
        /// </summary>
        public virtual void Delete()
        {
            int count = 0;

            // Delte
            for (int i = 0; i < size; i++)
            {
                // generate next random number and check if we need to do the delete operator
                if (rand.NextDouble() <= deleteRate)
                {
                    // clone the chromosome
                    IChromosome c = ((IChromosome)population[i]).Clone();
                    // mutate it
                    c.Delete();
                    // calculate fitness of the mutant
                    //c.Evaluate(fitnessFunction);
                    // add mutant to the population
                    population.Add(c);
                    count++;
                }
            }

            Console.WriteLine(" {0} delete(s) : {1} novo(s) individuo(s)", count, count);
        }

		/// <summary>
		/// Do selection
		/// </summary>
		public virtual void Selection( )
		{
			// amount of random chromosomes in the new population
			int randomAmount = (int)( randomSelectionPortion * size );

            Console.WriteLine(" {0} individuos(s) selecionados pelo metodo {1}", (size - randomAmount), selectionMethod.ToString() );

			// do selection
			selectionMethod.ApplySelection(ref population, size - randomAmount );

			// add random chromosomes
			if ( randomAmount > 0 )
			{
				IChromosome ancestor = (IChromosome) population[0];

				for ( int i = 0; i < randomAmount; i++ )
				{
					// create new chromosome
					IChromosome c = ancestor.CreateOffspring( );
				    c.GenerationId = _generationCount; 
					// calculate it's fitness
					//c.Evaluate( fitnessFunction );
					// add it to population
					population.Add( c );
				}
            }
        }

		/// <summary>
		/// Run one epoch of the population - crossover, mutation and selection
		/// </summary>
		public void RunEpoch( )
        {
            #region Apply operators
            Refresh();
			Crossover();
			Mutate();
		    Delete();
            #endregion

            //Fit Evaluation in as multiThreading
		    ExecuteFitEvaluation(true);
            
            //Selection method
            Selection();

            //Selection Method regenarates a lot od Chromosomes
            ExecuteFitEvaluation(false);

            //Finalize run
            FindBestChromosomeOfRun();	    
		}

        /// <summary>
        /// Start and synchronize Fits Evaluations 
        /// </summary>
	    private void ExecuteFitEvaluation(bool isFirstTime)
        {
            var sw = new Stopwatch();
            var taskList = population.Where(c => c.Fitness.Equals(0)).ToList();
            Console.WriteLine(" {2}valiando {0} Chromossomos na geração {1} - {3}", taskList.Count, GenerationCount, (isFirstTime == true ? "A" : "Rea"), DateTime.Now.ToString("HH:mm:ss"));
            TimeSpan span = DateTime.Now.AddMinutes(TimeOut) - DateTime.Now;

            sw.Start();
            if (Parallelism)
            {
                #region  Parallelism
                var resultList = taskList.Select(chromosome => new Thread(() => Start(chromosome, fitnessFunction.Clone()))
                            {
                                IsBackground = false,
                                Priority = ThreadPriority.Highest
                            }).ToList();

                resultList.ForEach(t => t.Start());
                resultList.ForEach(t => t.Join(span));
                #endregion
            }
            else
            {
                #region single thread
                foreach (var chromosome in population)
                {
                    Action action = () => chromosome.Evaluate(fitnessFunction);
                    IAsyncResult result = action.BeginInvoke(null, null);

                    //Atualizo o span
                    span = DateTime.Now.AddMinutes(TimeOut) - DateTime.Now;
                    if (!result.AsyncWaitHandle.WaitOne(span))
                    {
                        chromosome.Fitness = double.MaxValue;
                        Console.WriteLine("     Avaliar Fitness do individuo {0} falhou por timeout ({1} minutos) - {2}", chromosome.Id, span.TotalMinutes, DateTime.Now.ToString("HH:mm:ss"));
                    }
                    else
                        action.EndInvoke(result);
                }
                #endregion
            }

            sw.Stop();
            Console.WriteLine(" Geração avaliada em {0} minutos - {1}", sw.Elapsed.ToString("mm\\:ss\\.ff"), DateTime.Now.ToString("HH:mm:ss"));
        }

	    /// <summary>
	    /// Do Parallel Evaluation
	    /// </summary>
	    /// <param name="chromosome"></param>
	    /// <param name="fitnessFunction1"></param>
	    private void Start(IChromosome chromosome, IFitnessFunction fitnessFunction1)
	    {

	        try
	        {
                using (ProcessorAffinity.BeginAffinity(processorsToUse))
                {
                    Console.WriteLine("     Running on CPU #{0} ({1})", NtGetCurrentProcessorNumber(), chromosome.Id);
                    chromosome.Evaluate(fitnessFunction1);

                }
	        }
	        catch (Exception ex)
	        {
                Console.WriteLine("{0}", ex);
	            throw;
	        }

	    }

	    /// <summary>
        /// Finds the best Value
        /// </summary>
	    private void FindBestChromosomeOfRun()
	    {
	        #region find best chromosome

	        fitnessSum = 0;

	        foreach (IChromosome c in population)
	        {
	            double fitness = c.Fitness;

	            // accumulate summary value
	            fitnessSum += fitness;

	            // check for min
	            if (fitness < fitnessMin)
	            {
	                fitnessMin = fitness;
	                bestChromosome = c;
                    Console.WriteLine("============================== Achou melhor novo {0} ==============================", fitnessMin);
	            }
	        }
	        fitnessAvg = fitnessSum/size;

	        #endregion

            //Keeps the best Chromossome of run
            //GenerationsBestChromosomes.Add(GenerationCount, bestChromosome);
	    }

	    /// <summary>
        /// Refresh Chromosomes
        /// </summary>
	    private void Refresh()
	    {
            _generationCount++;
	    }

	    public void Trace( )
		{
			System.Diagnostics.Debug.WriteLine( "Max = " + fitnessMin );
			System.Diagnostics.Debug.WriteLine( "Sum = " + fitnessSum );
			System.Diagnostics.Debug.WriteLine( "Avg = " + fitnessAvg );
			System.Diagnostics.Debug.WriteLine( "--------------------------" );
			foreach ( IChromosome c in population )
			{
				System.Diagnostics.Debug.WriteLine( "genotype = " + c.ToString( ) +
					", phenotype = " + fitnessFunction.Translate( c ) +
					" , fitness = " + c.Fitness );
			}
			System.Diagnostics.Debug.WriteLine( "==========================" );
		}
	}
}
