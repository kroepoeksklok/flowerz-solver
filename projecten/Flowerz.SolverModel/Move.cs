using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Flowerz.SolverModel {
	/// <summary>
	/// The move to make. A gene in a GA.
	/// </summary>
	[DebuggerDisplay( "Piece = {Piece} on {Coordinate}" )]
	public class Move {
		public Move( Piece piece, Coordinate coordinate ) {
			Piece = piece;
			Coordinate = coordinate;
		}

		public Coordinate Coordinate { get; set; }

		public Coordinate SecondCoordinate { get; set; }

		/// <summary>
		/// Piece to place on Coordinate.
		/// </summary>
		public Piece Piece { get; set; }

		public static Move FromString( String s ) {
			//RW => (E,2)
			var pieceString = s.Substring( 0, 2 );
			var piece = Piece.FromString( pieceString );
			var coordinate = Coordinate.FromString( s.Substring( 6, 5 ) );
			var m = new Move( piece, coordinate );
			return m;
		}
		
		[ExcludeFromCodeCoverage]
		public override string ToString() {
			if( Piece.Type == PieceType.Flower || Piece.Type == PieceType.Butterfly ) {
				return String.Format( "{0} => {1}", Piece, Coordinate );
			} 
			
            if( Piece.Type == PieceType.Shovel ) {
				return String.Format( "Move flower from {0} to {1}", Coordinate, SecondCoordinate );
			}

			return "Impossible to make move with " + Piece.Type;
		}
	}
}