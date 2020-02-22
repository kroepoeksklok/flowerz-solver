using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Flowerz.SolverModel {
	/// <summary>
	/// A piece. It can be placed on the board, on in the queue.
	/// </summary>
	[DebuggerDisplay( "Type = {Type}, Outer = {OuterColor}, Inner = {InnerColor}" )]
	public class Piece {

		/// <summary>
		/// The type of the piece
		/// </summary>
		public PieceType Type { get; set; }

		/// <summary>
		/// The outer colour. Only applicable to flowers and butterflies.
		/// </summary>
		public Color OuterColor { get; set; }

		/// <summary>
		/// The inner colour. Only applicable to compound flowers.
		/// </summary>
		public Color InnerColor { get; set; }

		public Piece( Model.Piece flower ) {
			if( flower == Model.Piece.Shovel ) {
				Type = PieceType.Shovel;
			}

			if( flower == Model.Piece.Rock ) {
				Type = PieceType.Rock;
			}

			if( flower == Model.Piece.Empty ) {
				Type = PieceType.Empty;
			}

			#region Blue
			if( flower == Model.Piece.Blue ) {
				Type = PieceType.Flower;
				OuterColor = Color.Blue;
				InnerColor = Color.None;
			}

			if( flower == Model.Piece.BlueButterfly ) {
				Type = PieceType.Butterfly;
				OuterColor = Color.Blue;
				InnerColor = Color.None;
			}

			if( flower == Model.Piece.BlueCyan ) {
				Type = PieceType.Flower;
				OuterColor = Color.Blue;
				InnerColor = Color.Cyan;
			}

			if( flower == Model.Piece.BluePink ) {
				Type = PieceType.Flower;
				OuterColor = Color.Blue;
				InnerColor = Color.Pink;
			}

			if( flower == Model.Piece.BlueRed ) {
				Type = PieceType.Flower;
				OuterColor = Color.Blue;
				InnerColor = Color.Red;
			}

			if( flower == Model.Piece.BlueWhite ) {
				Type = PieceType.Flower;
				OuterColor = Color.Blue;
				InnerColor = Color.White;
			}

			if( flower == Model.Piece.BlueYellow ) {
				Type = PieceType.Flower;
				OuterColor = Color.Blue;
				InnerColor = Color.Yellow;
			}
			#endregion

			#region Cyan
			if( flower == Model.Piece.Cyan ) {
				Type = PieceType.Flower;
				OuterColor = Color.Cyan;
				InnerColor = Color.None;
			}

			if( flower == Model.Piece.CyanButterfly ) {
				Type = PieceType.Butterfly;
				OuterColor = Color.Cyan;
				InnerColor = Color.None;
			}

			if( flower == Model.Piece.CyanBlue ) {
				Type = PieceType.Flower;
				OuterColor = Color.Cyan;
				InnerColor = Color.Blue;
			}

			if( flower == Model.Piece.CyanPink ) {
				Type = PieceType.Flower;
				OuterColor = Color.Cyan;
				InnerColor = Color.Pink;
			}

			if( flower == Model.Piece.CyanRed ) {
				Type = PieceType.Flower;
				OuterColor = Color.Cyan;
				InnerColor = Color.Red;
			}

			if( flower == Model.Piece.CyanWhite ) {
				Type = PieceType.Flower;
				OuterColor = Color.Cyan;
				InnerColor = Color.White;
			}

			if( flower == Model.Piece.CyanYellow ) {
				Type = PieceType.Flower;
				OuterColor = Color.Cyan;
				InnerColor = Color.Yellow;
			}
			#endregion

			#region Pink
			if( flower == Model.Piece.Pink ) {
				Type = PieceType.Flower;
				OuterColor = Color.Pink;
				InnerColor = Color.None;
			}

			if( flower == Model.Piece.PinkButterfly ) {
				Type = PieceType.Butterfly;
				OuterColor = Color.Pink;
				InnerColor = Color.None;
			}

			if( flower == Model.Piece.PinkBlue ) {
				Type = PieceType.Flower;
				OuterColor = Color.Pink;
				InnerColor = Color.Blue;
			}

			if( flower == Model.Piece.PinkCyan ) {
				Type = PieceType.Flower;
				OuterColor = Color.Pink;
				InnerColor = Color.Cyan;
			}

			if( flower == Model.Piece.PinkRed ) {
				Type = PieceType.Flower;
				OuterColor = Color.Pink;
				InnerColor = Color.Red;
			}

			if( flower == Model.Piece.PinkWhite ) {
				Type = PieceType.Flower;
				OuterColor = Color.Pink;
				InnerColor = Color.White;
			}

			if( flower == Model.Piece.PinkYellow ) {
				Type = PieceType.Flower;
				OuterColor = Color.Pink;
				InnerColor = Color.Yellow;
			}
			#endregion

			#region Red
			if( flower == Model.Piece.Red ) {
				Type = PieceType.Flower;
				OuterColor = Color.Red;
				InnerColor = Color.None;
			}

			if( flower == Model.Piece.RedButterfly ) {
				Type = PieceType.Butterfly;
				OuterColor = Color.Red;
				InnerColor = Color.None;
			}

			if( flower == Model.Piece.RedBlue ) {
				Type = PieceType.Flower;
				OuterColor = Color.Red;
				InnerColor = Color.Blue;
			}

			if( flower == Model.Piece.RedCyan ) {
				Type = PieceType.Flower;
				OuterColor = Color.Red;
				InnerColor = Color.Cyan;
			}

			if( flower == Model.Piece.RedPink ) {
				Type = PieceType.Flower;
				OuterColor = Color.Red;
				InnerColor = Color.Pink;
			}

			if( flower == Model.Piece.RedWhite ) {
				Type = PieceType.Flower;
				OuterColor = Color.Red;
				InnerColor = Color.White;
			}

			if( flower == Model.Piece.RedYellow ) {
				Type = PieceType.Flower;
				OuterColor = Color.Red;
				InnerColor = Color.Yellow;
			}
			#endregion

			#region White
			if( flower == Model.Piece.White ) {
				Type = PieceType.Flower;
				OuterColor = Color.White;
				InnerColor = Color.None;
			}

			if( flower == Model.Piece.WhiteButterfly ) {
				Type = PieceType.Butterfly;
				OuterColor = Color.White;
				InnerColor = Color.None;
			}

			if( flower == Model.Piece.WhiteBlue ) {
				Type = PieceType.Flower;
				OuterColor = Color.White;
				InnerColor = Color.Blue;
			}

			if( flower == Model.Piece.WhiteCyan ) {
				Type = PieceType.Flower;
				OuterColor = Color.White;
				InnerColor = Color.Cyan;
			}

			if( flower == Model.Piece.WhitePink ) {
				Type = PieceType.Flower;
				OuterColor = Color.White;
				InnerColor = Color.Pink;
			}

			if( flower == Model.Piece.WhiteRed ) {
				Type = PieceType.Flower;
				OuterColor = Color.White;
				InnerColor = Color.Red;
			}

			if( flower == Model.Piece.WhiteYellow ) {
				Type = PieceType.Flower;
				OuterColor = Color.White;
				InnerColor = Color.Yellow;
			}
			#endregion

			#region Yellow
			if( flower == Model.Piece.Yellow ) {
				Type = PieceType.Flower;
				OuterColor = Color.Yellow;
				InnerColor = Color.None;
			}

			if( flower == Model.Piece.YellowButterfly ) {
				Type = PieceType.Butterfly;
				OuterColor = Color.Yellow;
				InnerColor = Color.None;
			}

			if( flower == Model.Piece.YellowBlue ) {
				Type = PieceType.Flower;
				OuterColor = Color.Yellow;
				InnerColor = Color.Blue;
			}

			if( flower == Model.Piece.YellowCyan ) {
				Type = PieceType.Flower;
				OuterColor = Color.Yellow;
				InnerColor = Color.Cyan;
			}

			if( flower == Model.Piece.YellowPink ) {
				Type = PieceType.Flower;
				OuterColor = Color.Yellow;
				InnerColor = Color.Pink;
			}

			if( flower == Model.Piece.YellowRed ) {
				Type = PieceType.Flower;
				OuterColor = Color.Yellow;
				InnerColor = Color.Red;
			}

			if( flower == Model.Piece.YellowWhite ) {
				Type = PieceType.Flower;
				OuterColor = Color.Yellow;
				InnerColor = Color.White;
			}
			#endregion
		}

		public Piece( PieceType type, Color outerColor, Color innerColor ) {
			Type = type;
			OuterColor = outerColor;
			InnerColor = innerColor;
		}

		//Below functions are not tested: they're purely for debugging
		[ExcludeFromCodeCoverage]
		public override string ToString() {
			var sb = new StringBuilder();

			if( Type == PieceType.Butterfly ) {
				AppendColour( sb, OuterColor );
				sb.Append( "F" );
			}

			if( Type == PieceType.Flower ) {
				AppendColour( sb, OuterColor );
				AppendColour( sb, InnerColor );
			}

			if( Type == PieceType.Shovel ) {
				sb.Append( "S" );
			}

			if( Type == PieceType.Rock ) {
				sb.Append( "ROCK" );
			}

			if( Type == PieceType.Empty ) {
				sb.Append( "EMPTY" );
			}
			return sb.ToString();
		}
		[ExcludeFromCodeCoverage]
		private static void AppendColour( StringBuilder sb, Color c ) {
			if( c == Color.Blue ) {
				sb.Append( "B" );
			}
			if( c == Color.Cyan ) {
				sb.Append( "C" );
			}
			if( c == Color.Pink ) {
				sb.Append( "P" );
			}
			if( c == Color.Red ) {
				sb.Append( "R" );
			}
			if( c == Color.White ) {
				sb.Append( "W" );
			}
			if( c == Color.Yellow ) {
				sb.Append( "Y" );
			}
			if( c == Color.None ) {
				sb.Append( "_" );
			}
		}

		//Not tested, because this function delegates to another class
		[ExcludeFromCodeCoverage]
		public static Piece FromString( string s ) {
			var modelPiece = Model.PieceTranslator.StringToPiece( s );
			return new Piece( modelPiece );
		}
	}
}