using System;

namespace Flowerz.Model {
    public static class PieceStrings {
        public const string Empty = "__";
        public const string Rock = "XX";
    }
	public class PieceTranslator {
		public static byte[] PieceToByte( Piece piece ) {
			switch( piece ) {
				case Piece.Shovel:
				case Piece.Empty:
					return GetBytes( 0x000000 );

				case Piece.Blue:
					return GetBytes( 0x403900 );
				case Piece.BlueButterfly:
					return GetBytes( 0x400000 );
				case Piece.BlueCyan:
					return GetBytes( 0x403500 );
				case Piece.BluePink:
					return GetBytes( 0x403100 );
				case Piece.BlueRed:
					return GetBytes( 0x402200 );
				case Piece.BlueWhite:
					return GetBytes( 0x402A00 );
				case Piece.BlueYellow:
					return GetBytes( 0x401400 );

				case Piece.Yellow:
					return GetBytes( 0x404C80 );
				case Piece.YellowButterfly:
					return GetBytes( 0x401800 );
				case Piece.YellowBlue:
					return GetBytes( 0x404080 );
				case Piece.YellowCyan:
					return GetBytes( 0x404A80 );
				case Piece.YellowPink:
					return GetBytes( 0x404880 );
				case Piece.YellowRed:
					return GetBytes( 0x404480 );
				case Piece.YellowWhite:
					return GetBytes( 0x404680 );

				case Piece.Red:
					return GetBytes( 0x405640 );
				case Piece.RedButterfly:
					return GetBytes( 0x402400 );
				case Piece.RedBlue:
					return GetBytes( 0x405040 );
				case Piece.RedCyan:
					return GetBytes( 0x405540 );
				case Piece.RedPink:
					return GetBytes( 0x405440 );
				case Piece.RedWhite:
					return GetBytes( 0x405340 );
				case Piece.RedYellow:
					return GetBytes( 0x405140 );

				case Piece.White:
					return GetBytes( 0x405E40 );
				case Piece.WhiteButterfly:
					return GetBytes( 0x402C00 );
				case Piece.WhiteBlue:
					return GetBytes( 0x405840 );
				case Piece.WhiteCyan:
					return GetBytes( 0x405D40 );
				case Piece.WhitePink:
					return GetBytes( 0x405C40 );
				case Piece.WhiteRed:
					return GetBytes( 0x405A40 );
				case Piece.WhiteYellow:
					return GetBytes( 0x405940 );

				case Piece.Pink:
					return GetBytes( 0x406320 );
				case Piece.PinkButterfly:
					return GetBytes( 0x403200 );
				case Piece.PinkBlue:
					return GetBytes( 0x406020 );
				case Piece.PinkCyan:
					return GetBytes( 0x4062A0 );
				case Piece.PinkRed:
					return GetBytes( 0x406120 );
				case Piece.PinkWhite:
					return GetBytes( 0x4061A0 );
				case Piece.PinkYellow:
					return GetBytes( 0x4060A0 );

				case Piece.Cyan:
					return GetBytes( 0x406720 );
				case Piece.CyanButterfly:
					return GetBytes( 0x403600 );
				case Piece.CyanBlue:
					return GetBytes( 0x406420 );
				case Piece.CyanPink:
					return GetBytes( 0x406620 );
				case Piece.CyanRed:
					return GetBytes( 0x406520 );
				case Piece.CyanWhite:
					return GetBytes( 0x4065A0 );
				case Piece.CyanYellow:
					return GetBytes( 0x4064A0 );

				case Piece.Rock:
					return GetBytes( 0x400000 );

				default:
					throw new ArgumentException( "Invalid piece." );
			}
		}

		private static byte[] GetBytes( int i ) {
			var bytes = BitConverter.GetBytes( i );
			Array.Reverse( bytes );
			return bytes;
		}

		public static Piece StringToPiece( String s ) {
			if( s == PieceStrings.Empty ) {
				return Piece.Empty;
			}

			if( s == "XX" ) {
				return Piece.Rock;
			}

			if( s == "S" ) {
				return Piece.Shovel;
			}

			#region Red
			if( s == "R_" ) {
				return Piece.Red;
			}
			if( s == "RF" ) {
				return Piece.RedButterfly;
			}
			if( s == "RB" ) {
				return Piece.RedBlue;
			}
			if( s == "RC" ) {
				return Piece.RedCyan;
			}
			if( s == "RP" ) {
				return Piece.RedPink;
			}
			if( s == "RW" ) {
				return Piece.RedWhite;
			}
			if( s == "RY" ) {
				return Piece.RedYellow;
			}
			#endregion

			#region Blue
			if( s == "B_" ) {
				return Piece.Blue;
			}
			if( s == "BF" ) {
				return Piece.BlueButterfly;
			}
			if( s == "BC" ) {
				return Piece.BlueCyan;
			}
			if( s == "BP" ) {
				return Piece.BluePink;
			}
			if( s == "BR" ) {
				return Piece.BlueRed;
			}
			if( s == "BW" ) {
				return Piece.BlueWhite;
			}
			if( s == "BY" ) {
				return Piece.BlueYellow;
			}
			#endregion

			#region White
			if( s == "W_" ) {
				return Piece.White;
			}
			if( s == "WF" ) {
				return Piece.WhiteButterfly;
			}
			if( s == "WB" ) {
				return Piece.WhiteBlue;
			}
			if( s == "WC" ) {
				return Piece.WhiteCyan;
			}
			if( s == "WP" ) {
				return Piece.WhitePink;
			}
			if( s == "WR" ) {
				return Piece.WhiteRed;
			}
			if( s == "WY" ) {
				return Piece.WhiteYellow;
			}
			#endregion

			#region Cyan
			if( s == "C_" ) {
				return Piece.Cyan;
			}
			if( s == "CF" ) {
				return Piece.CyanButterfly;
			}
			if( s == "CB" ) {
				return Piece.CyanBlue;
			}
			if( s == "CP" ) {
				return Piece.CyanPink;
			}
			if( s == "CR" ) {
				return Piece.CyanRed;
			}
			if( s == "CW" ) {
				return Piece.CyanWhite;
			}
			if( s == "CY" ) {
				return Piece.CyanYellow;
			}
			#endregion

			#region Pink
			if( s == "P_" ) {
				return Piece.Pink;
			}
			if( s == "PF" ) {
				return Piece.PinkButterfly;
			}
			if( s == "PB" ) {
				return Piece.PinkBlue;
			}
			if( s == "PC" ) {
				return Piece.PinkCyan;
			}
			if( s == "PR" ) {
				return Piece.PinkRed;
			}
			if( s == "PW" ) {
				return Piece.PinkWhite;
			}
			if( s == "PY" ) {
				return Piece.PinkYellow;
			}
			#endregion

			#region Yellow
			if( s == "Y_" ) {
				return Piece.Yellow;
			}
			if( s == "YF" ) {
				return Piece.YellowButterfly;
			}
			if( s == "YB" ) {
				return Piece.YellowBlue;
			}
			if( s == "YC" ) {
				return Piece.YellowCyan;
			}
			if( s == "YP" ) {
				return Piece.YellowPink;
			}
			if( s == "YR" ) {
				return Piece.YellowRed;
			}
			if( s == "YW" ) {
				return Piece.YellowWhite;
			}
			#endregion

			throw new ArgumentException( "Invalid string: " + s );
		}
	}
}