// AForge Genetic Library
//
// Copyright © Andrew Kirillov, 2006
// andrew.kirillov@gmail.com
//

using System.Collections.Generic;

namespace AForge.Genetic
{
	using System;
	using System.Collections;

	/// <summary>
	/// Selection method interface
	/// </summary>
	public interface ISelectionMethod
	{
		/// <summary>
		/// Apply selection to the population
		/// </summary>
		void ApplySelection(ref List<IChromosome> chromosomes, int size );
	}
}