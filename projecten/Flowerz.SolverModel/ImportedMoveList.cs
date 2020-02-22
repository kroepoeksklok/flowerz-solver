using System;
using System.Collections.Generic;
using System.Linq;

namespace Flowerz.SolverModel {
    public class ImportedMoveList : List<Coordinate> {
        public static ImportedMoveList FromImportableString( string importableString ) {
            var chunks = Enumerable.Range( 0, importableString.Length / 2 ).Select( i => importableString.Substring( i * 2, 2 ) );
            var l = new ImportedMoveList();

            foreach( var chunk in chunks ) {
                var col = chunk[ 0 ];
                var row = Convert.ToInt32( Char.GetNumericValue( chunk[ 1 ] ) );
                var c = CoordinateProvider.GetCoordinate( col, row );
                l.Add( c );
            }

            return l;
        }
    }
}