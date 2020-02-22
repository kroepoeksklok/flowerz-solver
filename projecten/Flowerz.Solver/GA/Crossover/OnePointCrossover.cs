using System;
using System.Linq;
using Flowerz.SolverModel;

namespace Flowerz.Solver.GA.Crossover {
	/// <summary>
	/// A one-point crossover picks one point and swaps everything behind it. This crossover
	/// can only be performed on movelists that have a length longer than 2.
	/// </summary>
	public class OnePointCrossover : ICrossoverStrategy {
		private readonly IRandomizer _randomizer;
		public OnePointCrossover( IRandomizer randomizer ) {
			_randomizer = randomizer;
		}

		public Tuple<MoveList, MoveList> DoCrossover( MoveList firstMovelist, MoveList secondMovelist ) {
			var child1 = new MoveList();
			var child2 = new MoveList();
			if( firstMovelist.Count > 2 ) {
				//If crossover = 0, you simply swap the entire sequence. This doesn't yield a new child
				//Same if the crossoverPoint = length - 1.
				var elementsToSkip = _randomizer.Next( 1, firstMovelist.Count - 2 );

				child1.AddRange( firstMovelist.Take( elementsToSkip ).Concat( secondMovelist.Skip( elementsToSkip ) ) );
				child2.AddRange( secondMovelist.Take( elementsToSkip ).Concat( firstMovelist.Skip( elementsToSkip ) ) );
			} else if( firstMovelist.Count == 1 ) {
				child1.Add( firstMovelist[ 0 ] );
				child2.Add( secondMovelist[ 0 ] );
			} else {
				var swapFirstMove = _randomizer.NextBoolean();
				if( swapFirstMove ) {
					child1.Add( secondMovelist[ 0 ] );
					child1.Add( firstMovelist[ 1 ] );
					child2.Add( firstMovelist[ 0 ] );
					child2.Add( secondMovelist[ 1 ] );
				} else {
					child1.Add( firstMovelist[ 0 ] );
					child1.Add( secondMovelist[ 1 ] );
					child2.Add( secondMovelist[ 0 ] );
					child2.Add( firstMovelist[ 1 ] );
				}
			}
			var children = new Tuple<MoveList, MoveList>( child1, child2 );
			return children;
		}
	}
}