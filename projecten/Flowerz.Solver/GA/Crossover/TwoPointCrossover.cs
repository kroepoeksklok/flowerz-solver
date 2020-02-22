using System;
using System.Linq;
using Flowerz.SolverModel;

namespace Flowerz.Solver.GA.Crossover {
	/// <summary>
	/// Selects two points on the movelists and swaps what's between them. For this strategy to work,
	/// the length needs to be at least 3. If the length is less than three,
	/// an OrderedCrossover or OnePointCrossover will be created randomly to apply instead.
	/// The only items not swapped are the first and the last item.
	/// </summary>
	public class TwoPointCrossover : ICrossoverStrategy {
		private readonly IRandomizer _randomizer;
		public TwoPointCrossover( IRandomizer randomizer ) {
			_randomizer = randomizer;
		}

		public Tuple<MoveList, MoveList> DoCrossover( MoveList firstMovelist, MoveList secondMovelist ) {
			var child1 = new MoveList();
			var child2 = new MoveList();
			if( firstMovelist.Count >= 3 ) {
				var firstCrossoverPoint = _randomizer.Next( 1, firstMovelist.Count - 1 );
				var secondCrossoverPoint = _randomizer.Next( 1, firstMovelist.Count - 1 );
				while( secondCrossoverPoint == firstCrossoverPoint ) {
					secondCrossoverPoint = _randomizer.Next( 1, firstMovelist.Count - 1 );
				}
				var lowestCrossoverPoint = ( firstCrossoverPoint < secondCrossoverPoint ? firstCrossoverPoint : secondCrossoverPoint );
				var highestCrossoverPoint = ( firstCrossoverPoint > secondCrossoverPoint ? firstCrossoverPoint : secondCrossoverPoint );
				var difference = highestCrossoverPoint - lowestCrossoverPoint;

				child1.AddRange(
					firstMovelist.Take( lowestCrossoverPoint )
								 .Concat( secondMovelist.Skip( lowestCrossoverPoint ).Take( difference ) )
								 .Concat( firstMovelist.Skip( highestCrossoverPoint ) )
					);

				child2.AddRange(
					secondMovelist.Take( lowestCrossoverPoint )
								  .Concat( firstMovelist.Skip( lowestCrossoverPoint ).Take( difference ) )
								  .Concat( secondMovelist.Skip( highestCrossoverPoint ) )
					);
				var children = new Tuple<MoveList, MoveList>( child1, child2 );
				return children;
			} else {
				var performOnePointCrossover = _randomizer.NextBoolean();
				//Not using the crossover factory to make sure a TwoPointCrossover won't be created.
				if( performOnePointCrossover ) {
					var opc = new OnePointCrossover( _randomizer );
					var children = opc.DoCrossover( firstMovelist, secondMovelist );
					return children;
				} else {
					var oc = new OrderedCrossover( _randomizer );
					var children = oc.DoCrossover( firstMovelist, secondMovelist );
					return children;
				}
			}
		}
	}
}