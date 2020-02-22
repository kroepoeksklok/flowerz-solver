using System;
using System.Collections.Generic;
using System.Linq;
using Flowerz.SolverModel;

namespace Flowerz.Solver.GA.Mutation {
	/// <summary>
	/// Mutates a movelist. The coordinate at index 1 is inserted at the second selected index.
	/// </summary>
	public class OrderedMutation : IMutationStrategy {
		private readonly IRandomizer _randomizer;
		public OrderedMutation( IRandomizer randomizer ) {
			_randomizer = randomizer;
		}

		public MoveList Mutate( MoveList moveListToMutate ) {
			var mutantMoveList = new MoveList();
			var coordinates = moveListToMutate.Select( move => move.Coordinate ).ToList();

			var indexOfCoordinateToMove = _randomizer.Next( moveListToMutate.Count );
			var indexToMoveTo = _randomizer.Next( moveListToMutate.Count );
			var coordinateToMove = coordinates[ indexOfCoordinateToMove ];

			coordinates.RemoveAt( indexOfCoordinateToMove );
			coordinates.Insert( indexToMoveTo, coordinateToMove );

			for( var i = 0; i < moveListToMutate.Count; i++ ) {
				mutantMoveList.Add(
					new Move(
						new Piece( moveListToMutate[ i ].Piece.Type, moveListToMutate[ i ].Piece.OuterColor, moveListToMutate[ i ].Piece.InnerColor ),
						coordinates[ i ] )
					);
			}
			return mutantMoveList;
		}
	}
}