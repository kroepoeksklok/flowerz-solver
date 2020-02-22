using System;

namespace Flowerz.Solver.GA {
	public class RandomizerFactory {
		public static IRandomizer CreateRandomizer() {
			return new DefaultRandomizer( new System.Random() );
		}
	}
}