using System;
using System.IO;

using Flowerz.Model;

namespace Flowerz.SaveGameBuilder {
    class Program {
        static void Main( string[] args ) {
            if( args.Length == 0 || args[ 0 ] == null ) {
                PrintUsage();
                Console.WriteLine( "Press any key to continue . . ." );
                Console.ReadLine();
                return;
            }

            var fileLocation = args[ 0 ];

            if( File.Exists( fileLocation ) ) {
                var game = FlowerzGame.CreateGameFromFile( fileLocation );

                using( var bw = new BinaryWriter( File.Open( @"C:\tmp\flowerzsolution.sol", FileMode.OpenOrCreate ) ) ) {
                    var builder = new SavegameBuilder( game );
                    var bytes = builder.ToByteArray();

                    foreach( var b in bytes ) {
                        bw.Write( b );
                    }

                    bw.Flush();
                    bw.Close();
                }
                Console.WriteLine( "File created." );
                Console.WriteLine( "Press any key to continue . . ." );
                Console.ReadLine();
            } else {
                Console.WriteLine( "No file found at '{0}' ", fileLocation );
                Console.WriteLine( "Press any key to continue . . . " );
                Console.ReadLine();
            }
        }

        private static void PrintUsage() {
            Console.WriteLine( "No filename specified." );
            Console.WriteLine( "Usage: Flowerz.SaveGameBuilder [filename] [outputfile]" );
            Console.WriteLine( "  [filename] is the path to the file containing the board and flower data. If it contains spaces, then surround the name with double quotes" );
            Console.WriteLine( "  [outputfile] is where the save gamefile should be written to. The directories must exist, but the file itself need not exist. If an existing file is chosen, then it is overwritten. If the path contains spaces, then surround the name with double quotes" );
        }
    }
}
