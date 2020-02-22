using System;
using System.Collections.Generic;
using System.Linq;

namespace Flowerz.SolverModel {
	public class CoordinateProvider {
		private static List<Tuple<int, int, Char, int, Coordinate>> _coordinates;

		public static Coordinate GetCoordinate( int columnIndex, int rowIndex ) {
			if( _coordinates == null ) {
				InstantiateCoordinates();
			}
			var c = _coordinates.SingleOrDefault( t => t.Item1 == columnIndex && t.Item2 == rowIndex );
			if( c == null ) {
				throw new InvalidCoordinateException( "Invalid coordinates passed. Coordinates must be between 0 and 6, inclusive." );
			}
			return c.Item5;
		}

		public static Coordinate GetCoordinate( Char column, int row ) {
			if( _coordinates == null ) {
				InstantiateCoordinates();
			}
			var c = _coordinates.SingleOrDefault( t => t.Item3 == column && t.Item4 == row );
			if( c == null ) {
				throw new InvalidCoordinateException( "Invalid coordinates passed. Column =  " + column + ", row = " + row + "." );
			}
			return c.Item5;
		}

		private static void InstantiateCoordinates() {
			_coordinates = new List<Tuple<int, int, Char, int, Coordinate>>();
			for( var rowIndex = 0; rowIndex < 7; rowIndex++ ) {
				for( var columnIndex = 0; columnIndex < 7; columnIndex++ ) {
					if( columnIndex == 0 ) {
						_coordinates.Add( new Tuple<int, int, Char, int, Coordinate>( columnIndex, rowIndex, 'A', rowIndex + 1, new Coordinate( columnIndex, rowIndex ) ) );
					}
					if( columnIndex == 1 ) {
						_coordinates.Add( new Tuple<int, int, Char, int, Coordinate>( columnIndex, rowIndex, 'B', rowIndex + 1, new Coordinate( columnIndex, rowIndex ) ) );
					}
					if( columnIndex == 2 ) {
						_coordinates.Add( new Tuple<int, int, Char, int, Coordinate>( columnIndex, rowIndex, 'C', rowIndex + 1, new Coordinate( columnIndex, rowIndex ) ) );
					}
					if( columnIndex == 3 ) {
						_coordinates.Add( new Tuple<int, int, Char, int, Coordinate>( columnIndex, rowIndex, 'D', rowIndex + 1, new Coordinate( columnIndex, rowIndex ) ) );
					}
					if( columnIndex == 4 ) {
						_coordinates.Add( new Tuple<int, int, Char, int, Coordinate>( columnIndex, rowIndex, 'E', rowIndex + 1, new Coordinate( columnIndex, rowIndex ) ) );
					}
					if( columnIndex == 5 ) {
						_coordinates.Add( new Tuple<int, int, Char, int, Coordinate>( columnIndex, rowIndex, 'F', rowIndex + 1, new Coordinate( columnIndex, rowIndex ) ) );
					}
					if( columnIndex == 6 ) {
						_coordinates.Add( new Tuple<int, int, Char, int, Coordinate>( columnIndex, rowIndex, 'G', rowIndex + 1, new Coordinate( columnIndex, rowIndex ) ) );
					}
				}
			}
		}
	}
}