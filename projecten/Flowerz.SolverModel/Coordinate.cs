using System;
using System.Diagnostics;

namespace Flowerz.SolverModel {
    /// <summary>
    /// Represents a coordinate. A single letter is used for the columns (A-G), and 
    /// numbers for the rows (1-7). Top left = A,1 and bottom right = G,7
    /// </summary>
    [DebuggerDisplay( "({Column},{Row}" )]
    public class Coordinate : IEquatable<Coordinate>, IComparable<Coordinate> {

        //public class Coordinate : IComparable<Coordinate> {
        /// <summary>
        /// Char for the column. A through G
        /// </summary>
        public Char Column { get; set; }

        /// <summary>
        /// 1-based integer for the row. 1 = top, 7 = bottom
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Returns the column of the Coordinateas its corresponding array index.
        /// </summary>
        /// <value></value>
        public int ColumnAsArrayIndex {
            get { return CharToArrayIndex( Column ); }
        }

        /// <summary>
        /// Returns the row of the Coordinate as its corresponding array index.
        /// </summary>
        /// <value></value>
        public int RowAsArrayIndex {
            get { return Row - 1; }
        }

        /// <summary>
        /// Creates a new column, based on the indices of the board.
        /// </summary>
        /// <param name="column">Column</param>
        /// <param name="row">0-based row.</param>
        public Coordinate( int column, int row ) {
            Column = ArrayIndexToChar( column );
            Row = row + 1;
        }

        /// <summary>
        /// Copies the coordinate
        /// </summary>
        /// <param name="c"></param>
        public Coordinate( Coordinate c )
            : this( c.Column, c.Row ) {
        }

        public static Coordinate InvalidCoordinate {
            get {
                return new Coordinate( 'Z', -1 );
            }
        }

        /// <summary>
        /// Creates a new Coordinate, based on the passed column and row.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        public Coordinate( Char col, int row ) {
            Column = col;
            Row = row;
        }

        public bool Equals( Coordinate other ) {
            return Column == other.Column && Row == other.Row;
        }

        public override string ToString() {
            return String.Format( "({0},{1})", Column, Row );
        }

        public string ToImportableString() {
            return String.Format( "{0}{1}", Column, Row );
        }

        public static Coordinate FromString( String s ) {
            var col = s.Substring( 1, 1 )[ 0 ];
            var row = Convert.ToInt32( s.Substring( 3, 1 ) );
            return new Coordinate( col, row );
        }

        private static Char ArrayIndexToChar( int i ) {
            switch( i ) {
                case 0:
                    return 'A';
                case 1:
                    return 'B';
                case 2:
                    return 'C';
                case 3:
                    return 'D';
                case 4:
                    return 'E';
                case 5:
                    return 'F';
                case 6:
                    return 'G';
            }
            return 'Z';
        }

        private static int CharToArrayIndex( Char c ) {
            switch( c ) {
                case 'A':
                    return 0;
                case 'B':
                    return 1;
                case 'C':
                    return 2;
                case 'D':
                    return 3;
                case 'E':
                    return 4;
                case 'F':
                    return 5;
                case 'G':
                    return 6;
            }
            return -1;
        }




        public int CompareTo( Coordinate other ) {
            if( other == null ) {
                return 1;
            }


            if( Row < other.Row ) {
                return -1;
            }

            if( Row > other.Row ) {
                return 1;
            }


            if( Column < other.Column ) {
                return -1;
            }

            if( Column > other.Column ) {
                return 1;
            }

            return 0;
        }

        public static bool operator <( Coordinate c1, Coordinate c2 ) {
            return c1.CompareTo( c2 ) < 0;
        }

        public static bool operator >( Coordinate c1, Coordinate c2 ) {
            return c1.CompareTo( c2 ) > 0;
        }
    }
}