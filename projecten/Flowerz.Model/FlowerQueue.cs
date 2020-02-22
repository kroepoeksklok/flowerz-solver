using System;
using System.Collections;
using System.Collections.Generic;

namespace Flowerz.Model {
	public class FlowerQueue : IEnumerable<Piece> {
		public FlowerQueue() {
			_piecesInQueue = new Queue<Piece>();
		}

		private readonly Queue<Piece> _piecesInQueue;

		public void AddToQueue( Piece piece ) {
			_piecesInQueue.Enqueue( piece );
		}

		public byte[] ToByteArray() {
			var queueBytes = new List<Byte> {
				0x70,
				0x69,
				0x65,
				0x63,
				0x65,
				0x73,
				0x51,
				0x75,
				0x65,
				0x75,
				0x65,
				0x08,
				0x00,
				0x00,
				0x00,
			};

			queueBytes.Add( Convert.ToByte( _piecesInQueue.Count ) );
			queueBytes.Add( 0x00 );
			queueBytes.Add( 0x01 );

			var tens = 0;
			var digit = 0;
			var boardNumber = 0;

			foreach( var piece in _piecesInQueue ) {
				if( tens > 0 ) {
					queueBytes.Add( DigitTranslator.IntToByte( tens ) );
				}

				queueBytes.Add( DigitTranslator.IntToByte( digit ) );

				if( digit == 9 ) {
					tens++;
					digit = 0;
				} else {
					digit++;
				}

				var pieceBytes = PieceTranslator.PieceToByte( piece );
				queueBytes.AddRange( pieceBytes );

				queueBytes.Add( 0 );
				queueBytes.Add( 0 );
				queueBytes.Add( 0 );
				queueBytes.Add( 0 );
				queueBytes.Add( 0 );
				queueBytes.Add( 0 );

				if( boardNumber == _piecesInQueue.Count - 1 ) {
					//Laatste
					queueBytes.Add( 0x00 );
					queueBytes.Add( 0x09 );
					queueBytes.Add( 0x00 );
					queueBytes.Add( 0x00 );
					queueBytes.Add( 0x11 );
				} else if( boardNumber <= 8 ) {
					queueBytes.Add( 0x01 );
				} else {
					queueBytes.Add( 0x02 );
				}

				boardNumber++;

			}

			return queueBytes.ToArray();
		}

		public IEnumerator<Piece> GetEnumerator() {
			return _piecesInQueue.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}