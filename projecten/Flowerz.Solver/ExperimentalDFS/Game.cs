using System;
using System.Collections.Generic;
using System.Text;

namespace Flowerz.Solver.ExperimentalDFS {
    public class Game {
        private System.Random _random = new System.Random();

        public int[] Board { get; private set; }
        public int NumberOfEmptySpaces { get; private set; }
        private int[] _originalBoard;

        public bool IsFull { get; private set; }

        public Game( int[] board ) {
            Board = board;
            _originalBoard = new int[ Constants.NumberOfFields ];
            Buffer.BlockCopy( Board, 0, _originalBoard, 0, Constants.GameArrayLength );
        }

        public int[] GetEmptySpaces() {
            var emptySpaces = new List<int>( Constants.NumberOfFields );
            for( var i = 0; i < Constants.NumberOfFields; i++ ) {
                if( Board[ i ] == 0 ) {
                    emptySpaces.Add( i );
                }
            }
            return emptySpaces.ToArray();
        }

        public int[] GetEmptySpacesRandomly() {
            var emptySpaces = new List<int>( Constants.NumberOfFields );
            for( var i = 0; i < Constants.NumberOfFields; i++ ) {
                if( Board[ i ] == 0 ) {
                    emptySpaces.Add( i );
                }
            }

            var n = emptySpaces.Count;
            while( n > 1 ) {
                var k = _random.Next( n-- );
                var temp = emptySpaces[ n ];
                emptySpaces[ n ] = emptySpaces[ k ];
                emptySpaces[ k ] = temp;
            }

            return emptySpaces.ToArray();
        }


        public void SetPiece( int piece, int index ) {
            Board[ index ] = piece;
            ProcessMove();
        }

        public bool IsValidMove( int index ) {
            return Board[ index ] != Constants.Rock;
        }

        private void ProcessMove() {
            while( true ) {
                var matches = new bool[ Constants.NumberOfFields ];
                var matchMade = false;
                FindAllMatches( matches );

                for( var i = 0; i < Constants.NumberOfFields; i++ ) {
                    if( matches[ i ] ) {
                        Board[ i ] = Board[ i ] >> 6;
                        matchMade = true;
                    }
                }

                if( matchMade ) {
                    continue;
                }

                break;
            }
        }

        private void FindAllMatches( bool[] matches ) {
            NumberOfEmptySpaces = 0;
            for( var i = 0; i < Constants.NumberOfFields; i++ ) {
                // 00 01 02 03 04 05 06
                // 07 08 09 10 11 12 13
                // 14 15 16 17 18 19 20
                // 21 22 23 24 25 26 27
                // 28 29 30 31 32 33 34
                // 35 36 37 38 39 40 41
                // 42 43 44 45 46 47 48

                if( Board[ i ] == Constants.Rock ) {

                } else if( Board[ i ] == Constants.Empty ) {
                    IsFull = false;
                    NumberOfEmptySpaces++;
                } else {
                    var indicesOfHorizontalMatch = new List<int>( 5 );
                    var indicesOfVerticalMatch = new List<int>( 5 );
                    indicesOfHorizontalMatch.Add( i );
                    indicesOfVerticalMatch.Add( i );

                    var outerColourOfCurrentIndex = Board[ i ] & ( -Board[ i ] );

                    DetectHorizontalMatches( outerColourOfCurrentIndex, i, indicesOfHorizontalMatch );
                    var numberOfHorizontalMatches = indicesOfHorizontalMatch.Count;
                    if( numberOfHorizontalMatches >= 3 ) {
                        for( var j = 0; j < numberOfHorizontalMatches; j++ ) {
                            matches[ indicesOfHorizontalMatch[ j ] ] = true;
                        }
                    }

                    DetectVerticalMatches( outerColourOfCurrentIndex, i, indicesOfVerticalMatch );
                    var numberOfVerticalMatches = indicesOfVerticalMatch.Count;
                    if( numberOfVerticalMatches >= 3 ) {
                        for( var j = 0; j < numberOfVerticalMatches; j++ ) {
                            matches[ indicesOfVerticalMatch[ j ] ] = true;
                        }
                    }
                }
            }
        }

        private void DetectHorizontalMatches( int outerColourCurrentField, int currentIndex, ICollection<int> indicesWithMatch ) {
            var nextColumn = currentIndex + 1;
            while( true ) {
                if( nextColumn == 7 || nextColumn == 14 || nextColumn == 21 || nextColumn == 28 || nextColumn == 35 || nextColumn == 42 || nextColumn == 49 ) {
                    break;
                }

                var outercolourNextColumn = Board[ nextColumn ] & ( -Board[ nextColumn ] );

                if( outerColourCurrentField == outercolourNextColumn ) {
                    indicesWithMatch.Add( nextColumn );
                    nextColumn++;
                } else {
                    break;
                }
            }
        }

        private void DetectVerticalMatches( int outerColourCurrentField, int currentIndex, ICollection<int> indicesWithMatch ) {
            var nextRow = currentIndex + 7;
            while( true ) {
                if( nextRow < 49 ) {
                    var outercolourNextRow = Board[ nextRow ] & ( -Board[ nextRow ] );

                    if( outerColourCurrentField == outercolourNextRow ) {
                        indicesWithMatch.Add( nextRow );
                        nextRow += 7;
                        continue;
                    }
                }
                break;
            }
        }


        public void Revert() {
            Buffer.BlockCopy( _originalBoard, 0, Board, 0, Constants.GameArrayLength );
        }


        public override string ToString() {
            var sb = new StringBuilder();
            var numberOfFieldOfRowProcessed = 0;

            for( var i = 0; i < Constants.NumberOfFields; i++ ) {
                sb.Append( GetFieldContentAsString( Board[ i ] ) + " " );
                numberOfFieldOfRowProcessed++;

                if( numberOfFieldOfRowProcessed == 7 ) {
                    numberOfFieldOfRowProcessed = 0;
                    sb.Append( Environment.NewLine );
                }
            }

            return sb.ToString();
        }

        private static string GetFieldContentAsString( int fieldContent ) {
            if( fieldContent == Constants.Rock ) {
                return "XX";
            }

            if( fieldContent == Constants.Empty ) {
                return "__";
            }

            return FieldValueHelper.GetOuterColour( fieldContent ) + FieldValueHelper.GetInnerColour( fieldContent );
        }
    }
}