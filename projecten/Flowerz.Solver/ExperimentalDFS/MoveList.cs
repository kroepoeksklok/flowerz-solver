using System;
using System.Collections.Generic;
using System.Text;

namespace Flowerz.Solver.ExperimentalDFS {
    public sealed class MoveList {
        private const int IntSize = sizeof( int );
        private readonly int _length;
        public int Score { get; set; }
        private readonly int[] _indices;

        public MoveList( int numberOfPieces ) {
            _length = IntSize * numberOfPieces;
            _indices = new int[ numberOfPieces ];
        }

        public void AddMove( int moveNumber, int fieldIndex ) {
            _indices[ moveNumber ] = fieldIndex;
        }

        public void SetNewBest( MoveList newBest ) {
            Buffer.BlockCopy( newBest._indices, 0, _indices, 0, _length );
        }

        public string GetLocation( int index ) {
            return GetLocationBasedOnIndex( _indices[ index ] );
        }

        public string ToString( List<int> pieces ) {
            var sb = new StringBuilder();

            for( var i = 0; i < _indices.Length; i++ ) {
                sb.AppendLine( String.Format( "{0} => {1}", GetPieceBasedOnValue( pieces[ i ] ), GetLocationBasedOnIndex( _indices[ i ] ) ) );
            }

            return sb.ToString();
        }

        private static string GetLocationBasedOnIndex( int index ) {
            var row = ( index / 7 ) + 1;
            var col = 'z';

            switch( index % 7 ) {
                case 0:
                    col = 'A';
                    break;
                case 1:
                    col = 'B';
                    break;
                case 2:
                    col = 'C';
                    break;
                case 3:
                    col = 'D';
                    break;
                case 4:
                    col = 'E';
                    break;
                case 5:
                    col = 'F';
                    break;
                case 6:
                    col = 'G';
                    break;
            }

            return String.Format( "({0},{1})", col, row );
        }

        private static string GetPieceBasedOnValue( int value ) {
            var piece = FieldValueHelper.GetOuterColour( value ) + FieldValueHelper.GetInnerColour( value );
            return piece;
        }
    }
}