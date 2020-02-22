using System;
using System.Collections.Generic;
using System.Text;

namespace Flowerz.SolverModel {
    public delegate void BoardChangedEventHandler( BoardChangedEventArgs args );

    /// <summary>
    /// The playing field. Uses a cartesian coordinate system.
    ///     A  B  C  D  E  F  G
    /// 01 __ __ __ __ __ __ __
    /// 02 __ __ __ __ __ __ __
    /// 03 __ __ __ __ __ __ __
    /// 04 __ __ __ __ __ __ __
    /// 05 __ __ __ __ __ __ __
    /// 06 __ __ __ __ __ __ __
    /// 07 __ __ __ __ __ __ __
    /// </summary>
    public class Board {
        public const int RowLength = 7;
        public const int ColLength = 7;
        private readonly Field[ , ] _initialLayout = new Field[ RowLength, ColLength ];
        private readonly Field[ , ] _boardLayout = new Field[ RowLength, ColLength ];
        private readonly int _maximumFreeSpaces;

        public event BoardChangedEventHandler BoardChanged;

        public int MaximumNumberOfFreeSpaces {
            get { return _maximumFreeSpaces; }
        }

        public Board( Model.Field[ , ] initialLayout ) {
            for( var rowIndex = 0; rowIndex < RowLength; rowIndex++ ) {
                for( var columnIndex = 0; columnIndex < ColLength; columnIndex++ ) {
                    var originalField = initialLayout[ rowIndex, columnIndex ];
                    var coordinate = CoordinateProvider.GetCoordinate( columnIndex, rowIndex );

                    var newField = new Field( new Piece( originalField.Piece ), coordinate );
                    var newField1 = new Field( new Piece( originalField.Piece ), coordinate );

                    _initialLayout[ rowIndex, columnIndex ] = newField1;
                    _boardLayout[ rowIndex, columnIndex ] = newField;

                    if( newField1.Piece.Type != PieceType.Rock ) {
                        _maximumFreeSpaces++;
                    }
                }
            }
        }

        public Board( Field[ , ] initialLayout ) {
            for( var rowIndex = 0; rowIndex < RowLength; rowIndex++ ) {
                for( var columnIndex = 0; columnIndex < ColLength; columnIndex++ ) {
                    var originalField = initialLayout[ rowIndex, columnIndex ];
                    var coordinate = CoordinateProvider.GetCoordinate( columnIndex, rowIndex );

                    var newField = new Field( new Piece( originalField.Piece.Type, originalField.Piece.OuterColor, originalField.Piece.InnerColor ), coordinate );
                    var newField1 = new Field( new Piece( originalField.Piece.Type, originalField.Piece.OuterColor, originalField.Piece.InnerColor ), coordinate );

                    _initialLayout[ rowIndex, columnIndex ] = newField1;
                    _boardLayout[ rowIndex, columnIndex ] = newField;

                    if( newField1.Piece.Type != PieceType.Rock ) {
                        _maximumFreeSpaces++;
                    }
                }
            }
        }

        public void Reset() {
            for( var rowIndex = 0; rowIndex < RowLength; rowIndex++ ) {
                for( var columnIndex = 0; columnIndex < ColLength; columnIndex++ ) {
                    var originalField = _initialLayout[ rowIndex, columnIndex ];
                    var currentField = _boardLayout[ rowIndex, columnIndex ];
                    currentField.Piece = new Piece( originalField.Piece.Type, originalField.Piece.OuterColor, originalField.Piece.InnerColor );
                }
            }
        }

        public void OverwriteBoard( Board b ) {
            for( var rowIndex = 0; rowIndex < RowLength; rowIndex++ ) {
                for( var columnIndex = 0; columnIndex < ColLength; columnIndex++ ) {
                    var fieldToReplaceWith = b._initialLayout[ rowIndex, columnIndex ];
                    var currentField = _boardLayout[ rowIndex, columnIndex ];
                    currentField.Piece = new Piece( fieldToReplaceWith.Piece.Type, fieldToReplaceWith.Piece.OuterColor, fieldToReplaceWith.Piece.InnerColor );
                }
            }
        }

        public Board GetNewBoardWithCurrentStateAsInitialState() {
            var b = new Board( _boardLayout );
            return b;
        }

        public Piece RemovePieceFromBoard( Coordinate c ) {
            var pieceOnField = this[ c ].Piece;
            if( pieceOnField.Type != PieceType.Flower ) {
                return null;
            }
            var removedPiece = new Piece( pieceOnField.Type, pieceOnField.OuterColor, pieceOnField.InnerColor );
            pieceOnField.Type = PieceType.Empty;
            pieceOnField.InnerColor = Color.None;
            pieceOnField.OuterColor = Color.None;
            return removedPiece;
        }

        /// <summary>
        /// Calculates the number of free spaces. Fitness function.
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfFreeSpaces() {
            var numberOfFreeSpaces = 0;

            for( var rowIndex = 0; rowIndex < RowLength; rowIndex++ ) {
                for( var columnIndex = 0; columnIndex < ColLength; columnIndex++ ) {
                    var field = _boardLayout[ rowIndex, columnIndex ];
                    if( field.Piece.Type == PieceType.Empty ) {
                        numberOfFreeSpaces++;
                    }
                }
            }

            return numberOfFreeSpaces;
        }

        /// <summary>
        /// Applies all of the moves in <tt>moves</tt>.
        /// </summary>
        /// <param name="moves">The list of moves to apply.</param>
        /// <returns>True if all of the moves could be applied. False if not.</returns>
        public Boolean ApplyMoveList( MoveList moves ) {
            var allMovesApplied = true;
            foreach( var move in moves ) {
                allMovesApplied = DoMove( move );
                if( !allMovesApplied ) {
                    break;
                }
            }
            return allMovesApplied;
        }

        public Boolean DoMove( Move move ) {
            var columnIndex = move.Coordinate.ColumnAsArrayIndex;
            var rowIndex = move.Coordinate.RowAsArrayIndex;
            var fieldOnBoard = _boardLayout[ rowIndex, columnIndex ];
            var boardChangedArgs = new BoardChangedEventArgs();
            if( BoardChanged != null ) {
                boardChangedArgs.PreviousState = ToString();
                boardChangedArgs.Move = move;
            }

            #region Using Shovel on flower
            if( move.SecondCoordinate != null && move.Piece.Type == PieceType.Shovel ) {
                var fieldToMoveTo = this[ move.SecondCoordinate ];
                if( fieldOnBoard.Piece.Type == PieceType.Flower && fieldToMoveTo.Piece.Type == PieceType.Empty ) {
                    var p = RemovePieceFromBoard( move.Coordinate );
                    fieldToMoveTo.Piece = new Piece( p.Type, p.OuterColor, p.InnerColor );
                    ApplyMatches();
                    if( BoardChanged != null ) {
                        boardChangedArgs.CurrentState = ToString();
                        boardChangedArgs.NumberOfFreeSpaces = GetNumberOfFreeSpaces();
                        BoardChanged( boardChangedArgs );
                    }
                    return true;
                }
            }
            #endregion

            #region Shovel on empty
            if( move.Piece.Type == PieceType.Shovel && fieldOnBoard.Piece.Type == PieceType.Empty ) {
                ApplyMatches();
                if( BoardChanged != null ) {
                    boardChangedArgs.CurrentState = ToString();
                    boardChangedArgs.NumberOfFreeSpaces = GetNumberOfFreeSpaces();
                    BoardChanged( boardChangedArgs );
                }
                return true;
            }
            #endregion

            #region Butterfly move
            if( move.Piece.Type == PieceType.Butterfly && fieldOnBoard.Piece.Type == PieceType.Flower ) {
                //Placing a butterfly on a flower
                var field = _boardLayout[ rowIndex, columnIndex ];
                field.Piece.Type = PieceType.Flower;
                field.Piece.OuterColor = move.Piece.OuterColor;
                field.Piece.InnerColor = Color.None;
                ApplyMatches();
                if( BoardChanged != null ) {
                    boardChangedArgs.CurrentState = ToString();
                    boardChangedArgs.NumberOfFreeSpaces = GetNumberOfFreeSpaces();
                    BoardChanged( boardChangedArgs );
                }
                return true;
            }

            if( move.Piece.Type == PieceType.Butterfly && fieldOnBoard.Piece.Type == PieceType.Empty ) {
                //Placing a butterfly on an empty place
                ApplyMatches();
                if( BoardChanged != null ) {
                    boardChangedArgs.CurrentState = ToString();
                    boardChangedArgs.NumberOfFreeSpaces = GetNumberOfFreeSpaces();
                    BoardChanged( boardChangedArgs );
                }
                return true;
            }
            #endregion

            #region Flower on empty
            if( move.Piece.Type == PieceType.Flower && fieldOnBoard.Piece.Type == PieceType.Empty ) {
                //Placing a flower on an empty spot.
                var field = _boardLayout[ rowIndex, columnIndex ];
                field.Piece = new Piece( move.Piece.Type, move.Piece.OuterColor, move.Piece.InnerColor );
                ApplyMatches();
                if( BoardChanged != null ) {
                    boardChangedArgs.CurrentState = ToString();
                    boardChangedArgs.NumberOfFreeSpaces = GetNumberOfFreeSpaces();
                    BoardChanged( boardChangedArgs );
                }
                return true;
            }
            #endregion

            //Could not apply move
            return false;
        }

        internal void ApplyMatches() {
            for( var rowIndex = 0; rowIndex < RowLength; rowIndex++ ) {
                for( var columnIndex = 0; columnIndex < ColLength; columnIndex++ ) {
                    var field = _boardLayout[ rowIndex, columnIndex ];
                    if( field.Piece.Type == PieceType.Flower ) {
                        var matchesToTheRight = DetectHorizontalMatches( rowIndex, columnIndex, field.Piece );
                        matchesToTheRight.Add( new Tuple<int, int>( rowIndex, columnIndex ) );

                        var matchesBelow = DetectVerticalMatches( rowIndex, columnIndex, field.Piece );
                        matchesBelow.Add( new Tuple<int, int>( rowIndex, columnIndex ) );

                        if( matchesToTheRight.Count >= 3 ) {
                            foreach( var tuple in matchesToTheRight ) {
                                _boardLayout[ tuple.Item1, tuple.Item2 ].MarkedForMatch = true;
                            }
                        }

                        if( matchesBelow.Count >= 3 ) {
                            foreach( var tuple in matchesBelow ) {
                                _boardLayout[ tuple.Item1, tuple.Item2 ].MarkedForMatch = true;
                            }
                        }
                    }
                }
            }
            var matchesOccured = false;
            for( var rowIndex = 0; rowIndex < RowLength; rowIndex++ ) {
                for( var columnIndex = 0; columnIndex < ColLength; columnIndex++ ) {
                    var field = _boardLayout[ rowIndex, columnIndex ];
                    if( field.MarkedForMatch ) {
                        field.ProcessMatch();
                        if( !matchesOccured ) {
                            matchesOccured = true;
                        }
                    }
                }
            }

            if( matchesOccured ) {
                ApplyMatches();
            }
        }

        #region Vertical matches
        private List<Tuple<int, int>> DetectVerticalMatches( int row, int col, Piece originalPiece ) {
            var coordinatesListDown = new List<Tuple<int, int>>();
            var newRowIndex = row + 1;
            if( newRowIndex < RowLength ) {
                var fieldBelow = _boardLayout[ newRowIndex, col ];
                if( fieldBelow.Piece.Type == PieceType.Flower &&
                    originalPiece.OuterColor == fieldBelow.Piece.OuterColor ) {
                    coordinatesListDown.Add( new Tuple<int, int>( newRowIndex, col ) );
                    DetectVerticalMatches( newRowIndex, col, originalPiece, coordinatesListDown );
                }
            }
            return coordinatesListDown;
        }

        private void DetectVerticalMatches( int row, int col, Piece originalPiece, List<Tuple<int, int>> coordinatesList ) {
            var newRowIndex = row + 1;
            if( newRowIndex < RowLength ) {
                var fieldBelow = _boardLayout[ newRowIndex, col ];
                if( fieldBelow.Piece.Type == PieceType.Flower &&
                    originalPiece.OuterColor == fieldBelow.Piece.OuterColor ) {
                    coordinatesList.Add( new Tuple<int, int>( newRowIndex, col ) );
                    DetectVerticalMatches( newRowIndex, col, originalPiece, coordinatesList );
                }
            }
        }
        #endregion

        #region Horizontal matches
        private List<Tuple<int, int>> DetectHorizontalMatches( int row, int col, Piece originalPiece ) {
            var coordinatesListHorizontal = new List<Tuple<int, int>>();
            var newColIndex = col + 1;
            if( newColIndex < ColLength ) {
                var fieldRight = _boardLayout[ row, newColIndex ];
                if( fieldRight.Piece.Type == PieceType.Flower &&
                    originalPiece.OuterColor == fieldRight.Piece.OuterColor ) {
                    coordinatesListHorizontal.Add( new Tuple<int, int>( row, newColIndex ) );
                    DetectHorizontalMatches( row, newColIndex, originalPiece, coordinatesListHorizontal );
                }
            }
            return coordinatesListHorizontal;
        }

        private void DetectHorizontalMatches( int row, int col, Piece originalPiece, List<Tuple<int, int>> coordinatesList ) {
            var newColIndex = col + 1;
            if( newColIndex < ColLength ) {
                var fieldRight = _boardLayout[ row, newColIndex ];
                if( fieldRight.Piece.Type == PieceType.Flower &&
                    originalPiece.OuterColor == fieldRight.Piece.OuterColor ) {
                    coordinatesList.Add( new Tuple<int, int>( row, newColIndex ) );
                    DetectHorizontalMatches( row, newColIndex, originalPiece, coordinatesList );
                }
            }
        }
        #endregion

        #region Coordinate retrieval
        public Coordinate GetRandomEmptyCoordinate( Random r ) {
            if( GetNumberOfFreeSpaces() > 0 ) {
                do {
                    var randomColumn = r.Next( 0, 7 );
                    var randomRow = r.Next( 0, 7 );

                    var field = _boardLayout[ randomRow, randomColumn ];
                    if( field.Piece.Type == PieceType.Empty ) {
                        return CoordinateProvider.GetCoordinate( randomColumn, randomRow );
                    }
                } while( true );
            }
            return Coordinate.InvalidCoordinate;
        }

        public Coordinate GetRandomNonRockCoordinate( Random r ) {
            do {
                var randomColumn = r.Next( 0, 7 );
                var randomRow = r.Next( 0, 7 );

                var field = _boardLayout[ randomRow, randomColumn ];
                if( field.Piece.Type != PieceType.Rock ) {
                    return CoordinateProvider.GetCoordinate( randomColumn, randomRow );
                }
            } while( true );
        }

        public List<Coordinate> GetAllEmptySpaces() {
            var emptyCoordinates = new List<Coordinate>();
            
            if( IsFull() ) {
                return emptyCoordinates;
            }

            for( var rowIndex = 0; rowIndex < RowLength; rowIndex++ ) {
                for( var columnIndex = 0; columnIndex < ColLength; columnIndex++ ) {
                    var field = _boardLayout[ rowIndex, columnIndex ];
                    if( field.Piece.Type == PieceType.Empty ) {
                        emptyCoordinates.Add( CoordinateProvider.GetCoordinate( columnIndex, rowIndex ) );
                    }
                }
            }

            return emptyCoordinates;
        }

        public Coordinate GetRandomEmptyCoordinate( Random r, List<Coordinate> coordinatesToTry ) {
            if( coordinatesToTry.Count == 0 ) {
                return Coordinate.InvalidCoordinate;
            }

            var nextIndex = r.Next( 0, coordinatesToTry.Count );
            return coordinatesToTry[ nextIndex ];
        }

        public Coordinate GetFirstEmptyCoordinateLeftToRightTopToBottom( List<Coordinate> coordinatesToTry ) {
            if( coordinatesToTry.Count == 0 ) {
                return Coordinate.InvalidCoordinate;
            }

            return coordinatesToTry[ 0 ];
        }
        #endregion

        #region Field retrieval
        public Field this[ Coordinate c ] {
            get { return _boardLayout[ c.RowAsArrayIndex, c.ColumnAsArrayIndex ]; }
        }
        #endregion

        public Boolean FlowersOnBoard() {
            for( var rowIndex = 0; rowIndex < RowLength; rowIndex++ ) {
                for( var columnIndex = 0; columnIndex < ColLength; columnIndex++ ) {
                    if( _boardLayout[ rowIndex, columnIndex ].Piece.Type == PieceType.Flower ) {
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean IsFull() {
            for( var rowIndex = 0; rowIndex < RowLength; rowIndex++ ) {
                for( var columnIndex = 0; columnIndex < ColLength; columnIndex++ ) {
                    if( _boardLayout[ rowIndex, columnIndex ].Piece.Type == PieceType.Empty ) {
                        return false;
                    }
                }
            }
            return true;
        }

        public override string ToString() {
            var sb = new StringBuilder();
            for( var rowIndex = 0; rowIndex < RowLength; rowIndex++ ) {
                for( var columnIndex = 0; columnIndex < ColLength; columnIndex++ ) {
                    var field = _boardLayout[ rowIndex, columnIndex ];
                    if( field.Piece.Type == PieceType.Rock ) {
                        sb.Append( "XX " );
                    } else if( field.Piece.Type == PieceType.Empty ) {
                        sb.Append( "__ " );
                    } else if( field.Piece.Type == PieceType.Flower ) {
                        sb.Append( field.Piece + " " );
                    }
                }
                sb.AppendLine( "" );
            }

            return sb.ToString();
        }
    }
}