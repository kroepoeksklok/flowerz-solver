using System;
using System.Linq;
using System.Threading;

namespace Flowerz.Solver.ExperimentalDFS {
    public class Solver {
        private MoveList _currentMoveList;
        private int _maximumScore;
        public ulong NumberOfSolutionsGenerated { get; private set; }
        public MoveList BestMoveList { get; private set; }
        public event EventHandler<SolutionCreatedEventArgs> NewHighScoreFound;
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public MoveList Solve( int[] board, int[] pieces, int locationFirstPiece ) {
            for( var i = 0; i < Constants.NumberOfFields; i++ ) {
                if( board[ i ] != Constants.Rock ) {
                    _maximumScore++;
                }
            }

            _currentMoveList = new MoveList( pieces.Length ) {
                Score = 0
            };

            BestMoveList = new MoveList( pieces.Length ) {
                Score = 0
            };

            var head = pieces.First();
            var tail = pieces.Skip( 1 ).ToArray();
            //Solve( board, head, tail, 0 );
            DoFirstMove( board, head, tail, 0, locationFirstPiece );
            return BestMoveList;
        }

        private void DoFirstMove( int[] board, int firstPiece, int[] tailList, int depth, int locationFirstPiece ) {
            var g = new Game( board );
            var emptySpace = g.GetEmptySpaces()[ locationFirstPiece ];
            g.SetPiece( firstPiece, emptySpace );
            _currentMoveList.AddMove( depth, emptySpace );

            var newHead = tailList[ 0 ];
            var remainingTailLength = tailList.Length - 1;
            var remainingTail = new int[ remainingTailLength ];
            Buffer.BlockCopy( tailList, Constants.IntSize, remainingTail, 0, remainingTailLength * Constants.IntSize );
            Solve( g.Board, newHead, remainingTail, depth + 1 );
        }

        private void Solve( int[] board, int currentPiece, int[] tailList, int depth ) {
            if( CancellationTokenSource.IsCancellationRequested ) {
                CancellationTokenSource.Token.ThrowIfCancellationRequested();
            }

            var g = new Game( board );
            var emptySpaces = g.GetEmptySpaces();
            var numberOfEmptySpaces = emptySpaces.Length;

            for( var i = 0; i < numberOfEmptySpaces; i++ ) {
                var emptySpace = emptySpaces[ i ];
                g.SetPiece( currentPiece, emptySpace );
                _currentMoveList.AddMove( depth, emptySpace );

                if( g.IsFull ) {
                    // You lose!
                } else {
                    if( tailList.Length > 0 ) {
                        var head = tailList[ 0 ];
                        var remainingTailLength = tailList.Length - 1;
                        var remainingTail = new int[ remainingTailLength ];
                        Buffer.BlockCopy( tailList, Constants.IntSize, remainingTail, 0, remainingTailLength * Constants.IntSize );
                        Solve( g.Board, head, remainingTail, depth + 1 );
                    } else {
                        NumberOfSolutionsGenerated++;
                        var score = g.NumberOfEmptySpaces;
                        _currentMoveList.Score = score;

                        if( score > BestMoveList.Score ) {
                            BestMoveList.Score = score;
                            BestMoveList.SetNewBest( _currentMoveList );

                            if( NewHighScoreFound != null ) {
                                NewHighScoreFound( this, new SolutionCreatedEventArgs {
                                    GeneratedSolution = BestMoveList,
                                    MaxScore = _maximumScore
                                } );
                            }
                        }
                    }
                }

                g.Revert();
            }
        }
    }
}
