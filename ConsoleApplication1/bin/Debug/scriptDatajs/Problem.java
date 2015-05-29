package ec.app.scriptDatajs;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;

import javax.script.ScriptEngine;
import javax.script.ScriptEngineManager;
import javax.script.ScriptException;

import ec.EvolutionState;
import ec.Individual;
import ec.gp.GPIndividual;
import ec.gp.GPProblem;
import ec.gp.koza.KozaFitness;
import ec.simple.SimpleFitness;
import ec.simple.SimpleProblemForm;
import ec.util.Parameter;

public class Problem extends GPProblem implements SimpleProblemForm
{
	public String scriptOrigem;
	public String scriptTestes;
	
    public void setup(final EvolutionState state,
            final Parameter base)
            {
    	
	        // very important, remember this
	        super.setup(state,base);

	        scriptOrigem = state.parameters.getStringWithDefault(base.push("scriptOrigem"), null, "");
	        scriptTestes = state.parameters.getStringWithDefault(base.push("scriptTestes"),null,"");
	        
	        /*
			if (scriptOrigem=="")
	            state.output.error("N�o foi definido o script alvo da otimiza��o");
	        
	        if (scriptTestes=="")
	            state.output.error("N�o foi definido script de Testes");
				*/
    }
	
	@Override
	public void evaluate(EvolutionState state, Individual ind, int subpopulation, int threadnum) 
	{

		if (!ind.evaluated) 
		{
			double fitness = 100000.0;
			
			
			long time = System.nanoTime();
            
			((GPIndividual)ind).trees[0].child.eval(state, threadnum, input, stack, (GPIndividual) ind, this);
			
			long totalTime = (System.nanoTime() - time); 
			
			//Exec do testes. Corretude e tempo de execu��o
			ScriptEngineManager engineManager = new ScriptEngineManager();
			ScriptEngine engine = engineManager.getEngineByName("nashorn");
			try {
				
			 //File file = new File(scriptTestes);
			 //System.out.println(file.getAbsolutePath());

			 engine.eval(new FileReader(scriptTestes));
			 
			} catch (FileNotFoundException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (ScriptException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
			
						
			//if(input.inteiro == 10)
			//{	
				//fitness = ((GPIndividual)ind).trees[0].child.children.length;
				//fitness = 10000 - fitness;

				fitness = totalTime ;
				
				ind.printIndividualForHumans(state, threadnum);
				//System.out.println("fitness=" + fitness );


				//System.out.println("(input.inteiro=" + input.inteiro );
				//System.out.println("(total=" + total);
				//System.out.println("X=" + currentX );
				//System.out.println("Y=" + currentY );
			//}
			
			// the fitness better be KozaFitness!
	        KozaFitness f = ((KozaFitness)ind.fitness);
	        f.setStandardizedFitness(state,(fitness));
	        //f.hits = Integer.parseInt(String.valueOf(totalTime));
			
			//SimpleFitness f = ((SimpleFitness)ind.fitness);
			//f.setFitness(state, fitness);
			ind.evaluated = true;
			
			//
		
		}
		
		
		
		
	}

	
   
	
}
