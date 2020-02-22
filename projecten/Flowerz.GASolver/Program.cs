using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Flowerz.Model;
using Flowerz.Solver;
using Flowerz.Solver.DFS;
using Flowerz.Solver.GA;
using Flowerz.SolverModel;

namespace Flowerz.GASolver {
    class Program {
        private const int DefaultPopulationSize = 2500;
        private const int DefaultNumberOfGenerations = 2800;
        private const int DefaultMutationChance = 6;
        private const int DefaultCrossoverChance = 70;
        private static readonly DepthFirstSearchOptions DepthFirstSearchOptions = new DepthFirstSearchOptions();
        private static readonly GeneticAlgorithmOptions GeneticAlgorithmOptions = new GeneticAlgorithmOptions();

        private static string _solutionFilename;
        private static string _movesFilename;
        private static int _nestLevel;
        private static ulong _solutionsGenerated = 0;
        private const ulong PrintStatsPerNumberOfSolutions = 200000;
        private static StreamWriter _movesStreamWriter;
        private static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        private static DateTime _start;
        private static SolverType _solverType;
        private const string GaImportFileName = "GA_Import.txt";

        static void Main( string[] args ) {
            if( args.Length == 0 || args[ 0 ] == null ) {
                PrintUsage();
                Console.WriteLine( "Press any key to continue . . ." );
                Console.ReadLine();
                return;
            }

            _solverType = GetCharInput( @"Please select search mode. Default: Genetic Algorithm
1. Depth First Search
2. Genetic Algorithm", new Dictionary<int, SolverType> {
                { 1, SolverType.DepthFirstSearch },
                { 2, SolverType.GeneticAlgorithm }
            }, SolverType.GeneticAlgorithm );

            var fileLocation = args[ 0 ];

            _solutionFilename = "Solution" + DateTime.Now.ToString( "yyyyMMdd-HHmmss" ) + ".txt";
            _movesFilename = "Solution" + DateTime.Now.ToString( "yyyyMMdd-HHmmss" ) + "-moves.txt";

            if( File.Exists( fileLocation ) ) {
                var game = FlowerzGame.CreateGameFromFile( fileLocation );

                if( _solverType == SolverType.GeneticAlgorithm ) {
                    var solver = SolverFactory.GetGeneticAlgorithmSolver();

                    solver.GenerationCreated += gasolver_GenerationCreated;
                    solver.SolverCreated += gasolver_SolverCreated;

                    GeneticAlgorithmOptions.PopulationSize = GetNumberInput( $"What should the size of the population be? Default: {DefaultPopulationSize}", DefaultPopulationSize );
                    GeneticAlgorithmOptions.NumberOfGenerationsToCreate = GetNumberInput( $"How many generations should be created? Default: {DefaultNumberOfGenerations}", DefaultNumberOfGenerations );
                    GeneticAlgorithmOptions.CrossOverChance = GetNumberInput( $"How big should the chance be to select the crossover operation? Default: {DefaultCrossoverChance}%", DefaultCrossoverChance );
                    GeneticAlgorithmOptions.MutationChance = GetNumberInput( $"How big should the chance be that a mutation occurs? Default: {DefaultMutationChance}%", DefaultMutationChance );

                    var useGaImporttxt = GetCharInput( @"Use GA_Import.txt? Default: True
1. Yes
2. No", new Dictionary<int, bool> {
    { 1, true },
    { 2, false }
}, true );

                    if( useGaImporttxt ) {
                        if( File.Exists( GaImportFileName ) ) {
                            Console.WriteLine( "Importing existing solutions . . ." );
                            var numberOfImportedSolutions = 0;
                            using( var sr = new StreamReader( new FileStream( GaImportFileName, FileMode.Open ) ) ) {
                                string s;
                                while( ( s = sr.ReadLine() ) != null ) {
                                    solver.ImportMovelist( s );
                                    numberOfImportedSolutions++;
                                }
                            }
                            Console.WriteLine( numberOfImportedSolutions + " solutions imported" );
                        } else {
                            Console.WriteLine( "Unable to import solutions. Could not find file '" + GaImportFileName + "'." );
                        }
                    } else {
                        Console.WriteLine( "Not importing solutions" );
                    }

                    Console.WriteLine( GeneticAlgorithmOptions );
                    solver.GeneticAlgorithmOptions = GeneticAlgorithmOptions;
                    Solve( solver, game );
                } else if( _solverType == SolverType.DepthFirstSearch ) {
                    DepthFirstSearchOptions.SearchRandomly = GetCharInput( @"Use random search? Default: no
1. Yes
2. No", new Dictionary<int, bool> {
                        {1, true},
                        {2 , false}
                    }, false );
                    var solver = SolverFactory.GetDfsSolver();
                    solver.SearchOptions = DepthFirstSearchOptions;
                    Solve( solver, game );
                } else {
                    Console.WriteLine( "No mode specified. Exiting." );
                    Console.WriteLine( "Press any key to continue . . . " );
                    Console.ReadLine();
                }
            } else {
                Console.WriteLine( "No file found at '{0}' ", fileLocation );
                Console.WriteLine( "Press any key to continue . . . " );
                Console.ReadLine();
            }
        }

        private static void Solve( ISolver solver, FlowerzGame game ) {
            var board = TranslateToSolverBoard( game.Board );
            var queue = TranslateToPiecesQueue( game.Queue );

            var ct = CancellationTokenSource.Token;
            solver.CancellationTokenSource = CancellationTokenSource;

            solver.SolutionCreated += solver_SolutionCreated;

            try {
                Task.Factory.StartNew( ListenForEscape );
                _start = DateTime.Now;
                Task.Factory.StartNew( () => {
                    var solution = solver.Solve( board, queue );

                    OpenMovesStreamWriter();
                    board.Reset();
                    board.BoardChanged += WriteMoveToFile;
                    board.ApplyMoveList( solution );
                    board.Reset();
                    CloseMovesStreamWriter();

                    WriteBestSolution( solution );
                    var totalTime = DateTime.Now.Subtract( _start );
                    Console.WriteLine( "Finished in {0}.", totalTime.ToString( "G", new CultureInfo( "nl-Nl" ) ) );
                    Console.WriteLine( "Done. Press enter to finish " );
                    Console.ReadLine();
                }, CancellationTokenSource.Token ).Wait( ct );
            } catch( OperationCanceledException e ) {
                Console.WriteLine( "Operation was canceled. Writing current best solution to file." );

                var currentBest = solver.GetCurrentBest;
                OpenMovesStreamWriter();
                board.Reset();
                board.BoardChanged += WriteMoveToFile;
                board.ApplyMoveList( currentBest );
                board.Reset();
                CloseMovesStreamWriter();

                var lastsolution = solver.LastGeneratedSolution;
                Console.WriteLine( lastsolution.ToImportableString() );

                WriteBestSolution( currentBest );
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


        static void ListenForEscape() {
            ConsoleKeyInfo keyinfo;
            do {
                keyinfo = Console.ReadKey();
                if( keyinfo.Key == ConsoleKey.Escape ) {
                    CancellationTokenSource.Cancel();
                }
            }
            while( keyinfo.Key != ConsoleKey.Escape );
        }

        private static void WriteBestSolution( MoveList solution ) {
            using( var sw = new StreamWriter( _solutionFilename, true ) ) {
                Console.WriteLine( "Leftover spaces after applying solution = " + solution.Score );
                Console.WriteLine( @"    A  B  C  D  E  F  G" );
                Console.WriteLine( @"01 __ __ __ __ __ __ __" );
                Console.WriteLine( @"02 __ __ __ __ __ __ __" );
                Console.WriteLine( @"03 __ __ __ __ __ __ __" );
                Console.WriteLine( @"04 __ __ __ __ __ __ __" );
                Console.WriteLine( @"05 __ __ __ __ __ __ __" );
                Console.WriteLine( @"06 __ __ __ __ __ __ __" );
                Console.WriteLine( @"07 __ __ __ __ __ __ __" );
                Console.WriteLine( solution.ToString() );

                sw.WriteLine( "Leftover spaces after applying solution = " + solution.Score );
                sw.WriteLine( @"    A  B  C  D  E  F  G" );
                sw.WriteLine( @"01 __ __ __ __ __ __ __" );
                sw.WriteLine( @"02 __ __ __ __ __ __ __" );
                sw.WriteLine( @"03 __ __ __ __ __ __ __" );
                sw.WriteLine( @"04 __ __ __ __ __ __ __" );
                sw.WriteLine( @"05 __ __ __ __ __ __ __" );
                sw.WriteLine( @"06 __ __ __ __ __ __ __" );
                sw.WriteLine( @"07 __ __ __ __ __ __ __" );
                sw.WriteLine( solution.ToString() );
                sw.WriteLine( "You can use the following line to import this solution into a new run" );

                for( var i = 0; i < solution.Count; i++ ) {
                    if( solution[ i ].Piece.Type == PieceType.Flower || solution[ i ].Piece.Type == PieceType.Butterfly ) {
                        if( i == 0 ) {
                            sw.Write( solution.Score + "|" );
                        }
                        if( i <= solution.Count - 2 ) {
                            sw.Write( solution[ i ] + ";" );
                        } else {
                            sw.Write( solution[ i ] );
                        }
                    }
                }

                sw.Flush();
                sw.Close();
            }

            Console.WriteLine( "Solution stored at " + _solutionFilename );
        }

        static void solver_SolutionCreated( object sender, SolutionCreatedEventArgs e ) {
            _solutionsGenerated++;
            if( _solutionsGenerated % PrintStatsPerNumberOfSolutions == 0 ) {
                var now = DateTime.Now;
                var totalTime = now.Subtract( _start );
                var totalSeconds = totalTime.TotalSeconds;
                var avgNumberOfSolutionsPerSecond = Convert.ToDouble( _solutionsGenerated ) / totalSeconds;
                Console.WriteLine( "{0} solutions created in {1:N} seconds. {2:N} solutions / second", _solutionsGenerated, totalSeconds, avgNumberOfSolutionsPerSecond );
                Console.WriteLine( "Best movelist: {0}", e.CurrentBestMoveList );
                Console.WriteLine( "Score: {0} / {1}: ", e.CurrentBestMoveList.Score, e.MaxScore );
                Console.WriteLine();
            }
        }

        private static void PrintUsage() {
            Console.WriteLine( "No filename specified." );
            Console.WriteLine( "Usage: Flowerz.GASolver [filename]" );
            Console.WriteLine( "  [filename] is the path to the file containing the board and flower data. If it contains spaces, then surround the name with double quotes" );
        }


        private static T GetCharInput<T>( string optionText, IReadOnlyDictionary<int, T> validEntries, T defaultValue ) {
            Console.WriteLine( optionText );
            while( true ) {
                var key = Console.ReadKey();
                if( key.Key == ConsoleKey.Enter ) {
                    return defaultValue;
                }

                var choice = key.KeyChar - '0';

                if( validEntries.ContainsKey( choice ) ) {
                    Console.WriteLine();
                    return validEntries[ choice ];
                } else {
                    Console.WriteLine( "Invalid option specified. Please choose one of the following: {0}", String.Join( ", ", validEntries.Select( kvp => kvp.Key ) ) );
                }
            }
        }

        private static int GetNumberInput( string optionText, int defaultValue ) {
            Console.WriteLine( optionText );
            var input = Console.ReadLine();
            int readValue;

            if( Int32.TryParse( input, out readValue ) ) {
                return readValue;
            }

            return defaultValue;
        }


        #region GA events
        private static void gasolver_GenerationCreated( Flowerz.Solver.GA.GenerationCreatedEventArgs args ) {
            if( args.GenerationNumber % 10 == 0 ) {
                Console.WriteLine( "".PadLeft( _nestLevel * 2, ' ' ) + "Current generation = " + args.GenerationNumber + ". Score = " + args.HighestPreviousScore + " out of " + args.MaximumNumberOfFreeSpaces );
            }
        }

        private static void gasolver_SolverCreated( Flowerz.Solver.GA.SolverCreatedEventArgs args ) {
            _nestLevel = args.NestLevel;
            Console.WriteLine( "".PadLeft( _nestLevel * 2, ' ' ) + "Solver created for a shovel. Current depth = " + args.NestLevel );
        }
        #endregion


        static void WriteMoveToFile( BoardChangedEventArgs args ) {
            var s = FormatBoardChangedEventArgs( args );
            _movesStreamWriter.WriteLine( s );
        }

        static void OpenMovesStreamWriter() {
            _movesStreamWriter = new StreamWriter( _movesFilename, true );
        }

        static void CloseMovesStreamWriter() {
            _movesStreamWriter.Flush();
            _movesStreamWriter.Close();
        }

        private static string FormatBoardChangedEventArgs( BoardChangedEventArgs args ) {
            if( args.PreviousState != null ) {
                var linesPreviousState = args.PreviousState.Split( new string[] { Environment.NewLine }, StringSplitOptions.None );
                var linesCurrentState = args.CurrentState.Split( new string[] { Environment.NewLine }, StringSplitOptions.None );

                return String.Format( @"Board has changed: " + args.Move + Environment.NewLine +
                                      linesPreviousState[ 0 ] + "      " + linesCurrentState[ 0 ] + Environment.NewLine +
                                      linesPreviousState[ 1 ] + "      " + linesCurrentState[ 1 ] + Environment.NewLine +
                                      linesPreviousState[ 2 ] + "      " + linesCurrentState[ 2 ] + Environment.NewLine +
                                      linesPreviousState[ 3 ] + " ---> " + linesCurrentState[ 3 ] + Environment.NewLine +
                                      linesPreviousState[ 4 ] + "      " + linesCurrentState[ 4 ] + Environment.NewLine +
                                      linesPreviousState[ 5 ] + "      " + linesCurrentState[ 5 ] + Environment.NewLine +
                                      linesPreviousState[ 6 ] + "      " + linesCurrentState[ 6 ] + Environment.NewLine +
                                      "Spaces left:" + args.NumberOfFreeSpaces + Environment.NewLine + Environment.NewLine );
            }
            return "No previous state?";
        }

        private static PiecesQueue TranslateToPiecesQueue( IEnumerable<Flowerz.Model.Piece> queue ) {
            var q = new PiecesQueue();
            foreach( var flower in queue ) {
                q.Add( new Flowerz.SolverModel.Piece( flower ) );
            }

            Console.WriteLine( "Queue info: " );
            foreach( var piece in q ) {
                Console.Write( piece + ", " );
            }
            Console.WriteLine();
            return q;
        }

        private static SolverModel.Board TranslateToSolverBoard( Flowerz.Model.Board board ) {
            var b = new SolverModel.Board( board.GetBoardLayout() );
            return b;
        }
    }
}
