using System;
using System.Collections.Generic;

namespace Flowerz.Model {
	public class Board {
		private readonly Field[ , ] _boardLayout = new Field[ 7, 7 ];

		public Board() {
			for( var i = 0; i < 7; i++ ) {
				for( var j = 0; j < 7; j++ ) {
					_boardLayout[ i, j ] = new Field();
				}
			}
		}

		public Field[ , ] GetBoardLayout() {
			return _boardLayout;	
		}

		public void SetPieceToField( int row, int col, Piece piece ) {
			_boardLayout[ row, col ].Piece = piece;
		}

		public byte[] ToByteArray() {
			var boardBytes = new List<Byte> {
				0x62, 0x6F, 0x61, 0x72, 0x64, 0x4C, 0x61, 0x79, 0x6F, 0x75, 0x74, 0x08, 0x00, 0x00, 0x00, 0x31, 0x00, 0x01
			};
			var boardNumber = 0;
			var tens = 0;
			var digit = 0;
			for( var i = 0; i < 7; i++ ) {
				for( var j = 0; j < 7; j++ ) {
					if( tens > 0 ) {
						boardBytes.Add( DigitTranslator.IntToByte( tens ) );
					}

					boardBytes.Add( DigitTranslator.IntToByte( digit ) );

					if( digit == 9 ) {
						tens++;
						digit = 0;
					} else {
						digit++;
					}

					var bytes = _boardLayout[ i, j ].GetBytes();

					boardBytes.AddRange( bytes );
					boardBytes.Add( 0 );
					boardBytes.Add( 0 );
					boardBytes.Add( 0 );
					boardBytes.Add( 0 );
					boardBytes.Add( 0 );
					boardBytes.Add( 0 );

					if( boardNumber <= 8 ) {
						boardBytes.Add( 0x01 );
					} else if( boardNumber == 48 ) {
						boardBytes.Add( 0x00 );
						boardBytes.Add( 0x09 );
						boardBytes.Add( 0x00 );
						boardBytes.Add( 0x00 );
						boardBytes.Add( 0x0B );
					} else {
						boardBytes.Add( 0x02 );
					}

					boardNumber++;
				}
			}

			return boardBytes.ToArray();
		}
	}
}
