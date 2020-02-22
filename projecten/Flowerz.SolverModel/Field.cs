using System;
using System.Diagnostics;

namespace Flowerz.SolverModel {
	/// <summary>
	/// Represents a field on the board.
	/// </summary>
	[DebuggerDisplay( "{Coordinate} => {Piece}" )]
	public class Field {
		public Piece Piece { get; set; }
		public Boolean MarkedForMatch { get; set; }
		public Coordinate Coordinate { get; set; }

		public Field( Piece piece, Coordinate coordinate ) {
			Piece = new Piece( piece.Type, piece.OuterColor, piece.InnerColor );
			Coordinate = coordinate;
		}

		public void ProcessMatch() {
			if( MarkedForMatch ) {
				if( Piece.Type == PieceType.Flower ) {
					if( Piece.InnerColor != Color.None ) {
						Piece.OuterColor = Piece.InnerColor;
						Piece.InnerColor = Color.None;
					} else {
						Piece.OuterColor = Color.None;
						Piece.Type = PieceType.Empty;
					}
					MarkedForMatch = false;
				}
			}
		}
	}
}