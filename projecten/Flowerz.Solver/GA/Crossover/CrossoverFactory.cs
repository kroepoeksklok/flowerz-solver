namespace Flowerz.Solver.GA.Crossover {
	public class CrossoverFactory {
		public static ICrossoverStrategy GetCrossoverStrategy( IRandomizer r ) {
			var x = r.Next( 101 );

			if( x >= 0 && x <= 50 ) {
				return new OrderedCrossover( r );
			}

			if( x >= 51 && x <= 75 ) {
				return new OnePointCrossover( r );
			}

			if( x >= 76 ) {
				return new TwoPointCrossover( r );
			}

			return new OrderedCrossover( r );
		}
	}
}