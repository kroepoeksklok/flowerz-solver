namespace Flowerz.Solver.ExperimentalDFS {
    internal static class FieldValueHelper {
        internal static string GetOuterColour( int value ) {
            if( ( value & ( 1 << 0 ) ) == 1 ) {
                return "R";
            }

            if( ( value & ( 1 << 1 ) ) == 2 ) {
                return "B";
            }

            if( ( value & ( 1 << 2 ) ) == 4 ) {
                return "Y";
            }

            if( ( value & ( 1 << 3 ) ) == 8 ) {
                return "W";
            }

            if( ( value & ( 1 << 4 ) ) == 16 ) {
                return "P";
            }

            if( ( value & ( 1 << 5 ) ) == 32 ) {
                return "C";
            }

            return value.ToString();
        }

        internal static string GetInnerColour( int value ) {
            if( ( value & ( 1 << 6 ) ) == 64 ) {
                return "R";
            }

            if( ( value & ( 1 << 7 ) ) == 128 ) {
                return "B";
            }

            if( ( value & ( 1 << 8 ) ) == 256 ) {
                return "Y";
            }

            if( ( value & ( 1 << 9 ) ) == 512 ) {
                return "W";
            }

            if( ( value & ( 1 << 10 ) ) == 1024 ) {
                return "P";
            }

            if( ( value & ( 1 << 11 ) ) == 2048 ) {
                return "C";
            }

            return "_";
        }
    }
}