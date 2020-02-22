using System;
using System.Collections.Generic;
using System.Threading;
using Flowerz.Solver.ExperimentalDFS;

namespace Flowerz.Solver.Random {
    public class RandomSolver {
        private readonly System.Random _random = new System.Random();
        private ExperimentalDFS.MoveList _currentMoveList;
        private int _maximumScore;

        public ulong NumberOfSolutionsGenerated { get; private set; }
        public ulong NumberOfInvalidSolutions { get; private set; }
        public ExperimentalDFS.MoveList BestMoveList { get; private set; }
        public event EventHandler<ExperimentalDFS.SolutionCreatedEventArgs> NewHighScoreFound;
        public CancellationTokenSource CancellationTokenSource { get; set; }


        public void Solve( int[] board, int[] pieces ) {
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

            var g = new Game( board );
            var loopLength = pieces.Length;

            while( true ) {
                if( CancellationTokenSource.IsCancellationRequested ) {
                    CancellationTokenSource.Token.ThrowIfCancellationRequested();
                }

                var indexes = new List<int>( loopLength );

                for( var i = 0; i < loopLength; i++ ) {
                    indexes.Add( _random.Next( 0, 49 ) );
                }

                var validList = true;
                for( var i = 0; i < loopLength; i++ ) {
                    if( g.IsValidMove( indexes[ i ] ) ) {
                        g.SetPiece( pieces[ i ], indexes[ i ] );
                    } else {
                        NumberOfInvalidSolutions++;
                        validList = false;
                        break;
                    }
                }

                if( validList ) {
                    NumberOfSolutionsGenerated++;
                    var score = g.NumberOfEmptySpaces;
                    _currentMoveList.Score = score;

                    if( score > BestMoveList.Score ) {
                        BestMoveList.Score = score;
                        BestMoveList.SetNewBest( _currentMoveList );

                        if( NewHighScoreFound != null ) {
                            NewHighScoreFound( this, new Flowerz.Solver.ExperimentalDFS.SolutionCreatedEventArgs {
                                GeneratedSolution = BestMoveList,
                                MaxScore = _maximumScore
                            } );
                        }
                    }
                }
            }
        }
    }
}
