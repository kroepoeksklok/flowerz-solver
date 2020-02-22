using System;
using System.Collections.Generic;
using Flowerz.SolverModel;

namespace Flowerz.Solver.GA.Crossover {
	public class OrderedCrossover : ICrossoverStrategy {
		private readonly IRandomizer _randomizer;
		public OrderedCrossover( IRandomizer randomizer ) {
			_randomizer = randomizer;
		}

		public Tuple<MoveList, MoveList> DoCrossover( MoveList firstMovelist, MoveList secondMovelist ) {
			var notSelectedFields = new List<int>();
			for( var i = 0; i < firstMovelist.Count; i++ ) {
				var selectField = _randomizer.NextBoolean();
				if( selectField ) {
					notSelectedFields.Add( i );
				}
			}

			var child1 = new MoveList();
			for( var i = 0; i < firstMovelist.Count; i++ ) {
				if( notSelectedFields.Contains( i ) ) {
					child1.Add( secondMovelist[ i ] );
				} else {
					child1.Add( firstMovelist[ i ] );
				}
			}

			notSelectedFields.Clear();

			for( var i = 0; i < firstMovelist.Count; i++ ) {
				var selectField = _randomizer.NextBoolean();
				if( selectField ) {
					notSelectedFields.Add( i );
				}
			}

			var child2 = new MoveList();
			for( var i = 0; i < firstMovelist.Count; i++ ) {
				if( notSelectedFields.Contains( i ) ) {
					child2.Add( firstMovelist[ i ] );
				} else {
					child2.Add( secondMovelist[ i ] );
				}
			}

			return new Tuple<MoveList, MoveList>( child1, child2 );
		}
	}
}