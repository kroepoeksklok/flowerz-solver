using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Flowerz.SolverModel {
    /// <summary>
    /// The list of moves that is the solution. Chromosome in a GA.
    /// </summary>
    [DebuggerDisplay( "Name = {Name}. Number of moves = {Count}. Moves = {this}" )]
    public class MoveList : List<Move>, IComparable<MoveList> {
        public int Score { get; set; }

        public int CompareTo( MoveList other ) {
            var areEqual = true;
            for( var i = 0; i < Count; i++ ) {
                var myCoordinate = this[ i ].Coordinate;
                var otherCoordinate = other[ i ].Coordinate;
                areEqual &= myCoordinate.Equals( otherCoordinate );
            }

            if( areEqual ) {
                return 0;
            }

            return -1;
        }

        public string ToImportableString() {
            var sb = new StringBuilder();
            sb.AppendLine();
            foreach( var move in this ) {
                sb.Append( move.Coordinate.ToImportableString() );
            }

            return sb.ToString();
        }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine( "Moves in movelist: " );
            foreach( var move in this ) {
                sb.AppendLine( string.Join( ",", move ) );
            }

            return sb.ToString();
        }

        public MoveList Clone() {
            var ml = new MoveList();

            foreach( var move in this ) {
                ml.Add( move );
            }

            return ml;
        }

        public static MoveList ParseMoveList( String s ) {
            var ml = new MoveList();
            var splitString = s.Split( '|' );
            var stringMoves = splitString[ 1 ].Split( ';' );
            foreach( var stringMove in stringMoves ) {
                var move = Move.FromString( stringMove );
                ml.Add( move );
            }
            ml.Score = Convert.ToInt32( splitString[ 0 ] );
            return ml;
        }
    }
}
