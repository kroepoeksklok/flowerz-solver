using System;

namespace Flowerz.Solver.GA {
	public interface IRandomizer {
		int Next( int maxValue );
		int Next( int minValue, int maxValue );
		double NextDouble();
		bool NextBoolean();
		/// <summary>
		/// Gets the underlying randomizer. Added this to prevent a reference from the solvermodel to the solver.
		/// </summary>
		System.Random GetRandom { get; }
	}
}