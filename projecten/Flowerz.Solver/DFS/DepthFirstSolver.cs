using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Flowerz.SolverModel;

namespace Flowerz.Solver.DFS {
    public class DepthFirstSolver : IDepthFirstSolver {
        private PiecesQueue PiecesQueue { get; set; }
        private Board CurrentState { get; set; }
        private bool _optimumScoreFound;
        private int _maximumScore;

        public MoveList GetCurrentBest { get; private set; }
        public MoveList LastGeneratedSolution { get; private set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public DepthFirstSearchOptions SearchOptions { private get; set; }
        public event EventHandler<SolutionCreatedEventArgs> SolutionCreated;

        public MoveList Solve( Board board, PiecesQueue queue ) {
            if( queue == null ) {
                throw new ArgumentNullException( "queue" );
            }
            if( board == null ) {
                throw new ArgumentNullException( "board" );
            }

            PiecesQueue = queue;
            CurrentState = board;

            _maximumScore = CurrentState.MaximumNumberOfFreeSpaces;
            var piecesList = PiecesQueue.ToList();
            var headPiece = piecesList.First();
            var tail = piecesList.Skip( 1 ).ToList();
            GetCurrentBest = new MoveList { Score = 0 };
            var ml = new MoveList();
            var random = new System.Random();
            Solve( random, ml, headPiece, tail, 0 );

            return GetCurrentBest;
        }

        private void Solve( System.Random random, MoveList parentMoves, Piece headPiece, IEnumerable<Piece> tail, int depth ) {
            var allFieldsTried = false;
            var tailList = tail.ToList();
            var originalState = CurrentState.GetNewBoardWithCurrentStateAsInitialState();
            var emptyCoordinatesToTry = CurrentState.GetAllEmptySpaces();

            if( !_optimumScoreFound ) {
                do {
                    if( CancellationTokenSource.IsCancellationRequested ) {
                        CancellationTokenSource.Token.ThrowIfCancellationRequested();
                    }

                    var ml = parentMoves.Clone();

                    Coordinate coordinateToTry;
                    if( SearchOptions.SearchRandomly ) {
                        coordinateToTry = CurrentState.GetRandomEmptyCoordinate( random, emptyCoordinatesToTry );
                    } else {
                        coordinateToTry = CurrentState.GetFirstEmptyCoordinateLeftToRightTopToBottom( emptyCoordinatesToTry );
                    }

                    emptyCoordinatesToTry.Remove( coordinateToTry );

                    if( coordinateToTry.Equals( Coordinate.InvalidCoordinate ) ) {
                        allFieldsTried = true;
                    } else {
                        var move = new Move( headPiece, coordinateToTry );
                        CurrentState.DoMove( move );
                        ml.Add( move );

                        if( CurrentState.IsFull() ) {
                            // You lose!
                            ml = null;
                        } else {
                            if( tailList.Any() ) {
                                var newHead = tailList.First();
                                var newTail = tailList.Skip( 1 );
                                Solve( random, ml, newHead, newTail, depth + 1 );
                            } else {
                                LastGeneratedSolution = ml;
                                ml.Score = CurrentState.GetNumberOfFreeSpaces();
                                if( ml.Score == _maximumScore ) {
                                    _optimumScoreFound = true;
                                    GetCurrentBest = ml;
                                } else {
                                    if( ml.Score > GetCurrentBest.Score ) {
                                        GetCurrentBest = ml;
                                    } else {
                                        ml = null;
                                    }
                                }

                                if( SolutionCreated != null ) {
                                    SolutionCreated( this, new SolutionCreatedEventArgs {
                                        CurrentBestMoveList = GetCurrentBest,
                                        MaxScore = _maximumScore
                                    } );
                                }
                            }
                        }
                    }

                    CurrentState.OverwriteBoard( originalState );
                } while( !allFieldsTried );
            }
        }
    }
}
