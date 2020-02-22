using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flowerz.Solver.ExperimentalDFS;
using Flowerz.Solver.Random;

namespace Flowerz.Random {
    class Program {
        private static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        private static readonly RandomSolver Solver = new RandomSolver();
        private static DateTime _start;
        private static readonly CultureInfo Nl = new CultureInfo( "nl-NL" );

        static void Main( string[] args ) {
            if( args.Length == 0 || args[ 0 ] == null ) {
                PrintUsage();
                Console.WriteLine( "Press any key to continue . . ." );
                Console.ReadLine();
                return;
            }

            var fileLocation = args[ 0 ];
            if( File.Exists( fileLocation ) ) {
                var data = GetData( fileLocation );

                Solver.NewHighScoreFound += s_NewHighScoreFound;

                var ct = CancellationTokenSource.Token;
                Solver.CancellationTokenSource = CancellationTokenSource;

                try {
                    Task.Factory.StartNew( ListenForKeys );
                    _start = DateTime.Now;
                    Task.Factory.StartNew( () => {
                        Solver.Solve( data.Item1, data.Item2 );
                        Console.WriteLine( "Press enter to close." );
                        Console.ReadLine();
                    }, CancellationTokenSource.Token ).Wait( ct );
                } catch( OperationCanceledException e ) {
                    Console.WriteLine( "Operation was canceled. Writing current best solution to file." );

                    var currentBest = Solver.BestMoveList;
                    Console.WriteLine( currentBest.ToString( data.Item2.ToList() ) );
                    //OpenMovesStreamWriter();
                    //board.Reset();
                    //board.BoardChanged += WriteMoveToFile;
                    //board.ApplyMoveList( currentBest );
                    //board.Reset();
                    //CloseMovesStreamWriter();

                    //var lastsolution = solver.LastGeneratedSolution;
                    //Console.WriteLine( lastsolution.ToImportableString() );

                    //WriteBestSolution( currentBest );
                    Console.WriteLine( "Press enter to close." );
                    Console.ReadLine();
                } catch( AggregateException e ) {
                    foreach( var v in e.InnerExceptions ) {
                        Console.WriteLine( e.Message + " " + v.Message );
                    }
                } finally {
                    CancellationTokenSource.Dispose();
                }
            }
        }

        static void s_NewHighScoreFound( object sender, Solver.ExperimentalDFS.SolutionCreatedEventArgs e ) {
            Console.WriteLine( "[{0}] New high score found: Score: {1} / {2}", DateTime.Now.ToString( "G", Nl ), e.GeneratedSolution.Score, e.MaxScore );
        }

        static void ListenForKeys() {
            ConsoleKeyInfo keyinfo;
            do {
                keyinfo = Console.ReadKey( true );
                if( keyinfo.Key == ConsoleKey.Escape ) {
                    CancellationTokenSource.Cancel();
                } else if( keyinfo.Key == ConsoleKey.Spacebar ) {
                    var numberOfSolutionsGenerated = Solver.NumberOfSolutionsGenerated;
                    var now = DateTime.Now;
                    var totalTime = now.Subtract( _start );
                    var totalSeconds = totalTime.TotalSeconds;
                    var avgNumberOfSolutionsPerSecond = Convert.ToDouble( numberOfSolutionsGenerated ) / totalSeconds;

                    Console.WriteLine( "[{0}] Created {1} solutions in {2}. Avg: {3}. There were {4} invalid solutions", DateTime.Now.ToString( "G", Nl ), numberOfSolutionsGenerated.ToString( "N0", Nl ), totalTime.ToString( "G", Nl ), avgNumberOfSolutionsPerSecond, Solver.NumberOfInvalidSolutions );
                }
            }
            while( keyinfo.Key != ConsoleKey.Escape || keyinfo.Key != ConsoleKey.Spacebar );
        }

        private static Tuple<int[], int[]> GetData( string fileLocation ) {
            var boardData = new List<int>();
            var pieces = new List<int>();

            using( var stringReader = new StreamReader( new FileStream( fileLocation, FileMode.Open ) ) ) {
                string s;
                var lineNumber = 1;
                while( ( s = stringReader.ReadLine() ) != null ) {
                    if( lineNumber == 1 ) {
                        //Line 1 -> Queue, Items are separates by spaces
                        //Cannot contain Rock (XX) or empty field (__)
                        var queueItems = s.Split( ' ' );

                        // Skips flowers and shovels in order to require less decisions when solving
                        foreach( var queueItem in queueItems.Where( q => !q.EndsWith( "F" ) ) ) {
                            var value = 0;

                            value += GetValueIfMatch( queueItem[ 1 ], 'R', 64 );
                            value += GetValueIfMatch( queueItem[ 1 ], 'B', 128 );
                            value += GetValueIfMatch( queueItem[ 1 ], 'Y', 256 );
                            value += GetValueIfMatch( queueItem[ 1 ], 'W', 512 );
                            value += GetValueIfMatch( queueItem[ 1 ], 'P', 1024 );
                            value += GetValueIfMatch( queueItem[ 1 ], 'C', 2048 );

                            value += GetValueIfMatch( queueItem[ 0 ], 'R', 1 );
                            value += GetValueIfMatch( queueItem[ 0 ], 'B', 2 );
                            value += GetValueIfMatch( queueItem[ 0 ], 'Y', 4 );
                            value += GetValueIfMatch( queueItem[ 0 ], 'W', 8 );
                            value += GetValueIfMatch( queueItem[ 0 ], 'P', 16 );
                            value += GetValueIfMatch( queueItem[ 0 ], 'C', 32 );

                            pieces.Add( value );
                        }
                    } else if( lineNumber > 1 ) {
                        //Lines 2-8 -> board layout
                        //Cannot contain shovel (S) or Butterfly (*F)
                        var rowData = s.Split( ' ' );

                        foreach( var field in rowData ) {
                            var value = 0;

                            if( field == "XX" ) {
                                value = Constants.Rock;
                            } else if( field == "__" ) {
                                value = 0;
                            } else {
                                value += GetValueIfMatch( field[ 1 ], 'R', 64 );
                                value += GetValueIfMatch( field[ 1 ], 'B', 128 );
                                value += GetValueIfMatch( field[ 1 ], 'Y', 256 );
                                value += GetValueIfMatch( field[ 1 ], 'W', 512 );
                                value += GetValueIfMatch( field[ 1 ], 'P', 1024 );
                                value += GetValueIfMatch( field[ 1 ], 'C', 2048 );

                                value += GetValueIfMatch( field[ 0 ], 'R', 1 );
                                value += GetValueIfMatch( field[ 0 ], 'B', 2 );
                                value += GetValueIfMatch( field[ 0 ], 'Y', 4 );
                                value += GetValueIfMatch( field[ 0 ], 'W', 8 );
                                value += GetValueIfMatch( field[ 0 ], 'P', 16 );
                                value += GetValueIfMatch( field[ 0 ], 'C', 32 );
                            }

                            boardData.Add( value );
                        }

                    }

                    lineNumber++;
                }
            }

            var t = new Tuple<int[], int[]>( boardData.ToArray(), pieces.ToArray() );
            return t;
        }

        private static int GetValueIfMatch( char toCheck, char expected, int value ) {
            if( toCheck == expected ) {
                return value;
            }

            return 0;
        }


        private static void PrintUsage() {
            Console.WriteLine( "No filename specified." );
            Console.WriteLine( "Usage: Flowerz.GASolver [filename]" );
            Console.WriteLine( "  [filename] is the path to the file containing the board and flower data. If it contains spaces, then surround the name with double quotes" );
        }
    }
}
