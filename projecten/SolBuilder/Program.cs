using System;
using System.Collections.Generic;
using System.IO;
using Flowerz.Model;
using Flowerz.Solver;
using Flowerz.Solver.GA.Crossover;
using Flowerz.SolverModel;

namespace SolBuilder {
	public class Program {
		private static int? _passedPopulationSize;
		private static int? _passedNumberOfGenerations;
		private static string _passedFileNameWithSolution = null;
		private static string _geneticLogFilename;
		private static string _crossoverlogFilename;
		private static string _solutionFilename;
		private static string _movesFilename;
		private static bool _createDiagLogs;
		//private static String textPrefix = "";
		private static bool _solveForShovel = false;
		private static int _nestLevel;

		public static void Main( string[] args ) {
			if( args[ 0 ] == null ) {
				Console.WriteLine( "No mode specified. Specify any of the following:" );
				Console.WriteLine( "1. -c [filename] => Creates a save game file based on the data in the file." );
				Console.WriteLine( "2. -s [filename] [-l] [-popSize:N] [-numGen:N] [-solutions:[filename]] [-shovel] => Solve the puzzle with as many squares left over as possible." );
				Console.WriteLine( "Press any key to continue . . ." );
				Console.ReadLine();
				return;
			}
			if( args[ 1 ] == null ) {
				Console.WriteLine( "No file specified as second parameter. Please supply a full path as second parameter." );
				Console.WriteLine( "Press any key to continue . . ." );
				Console.ReadLine();
				return;
			}

			foreach( var arg in args ) {
				if( arg.Contains( "-l" ) ) {
					_createDiagLogs = true;
				}

				if( arg.Contains( "-shovel" ) ) {
					_solveForShovel = true;
				}

				if( arg.Contains( "-popSize" ) || arg.Contains( "-numGen" ) || arg.Contains( "-solutions" ) ) {
					var kvl = arg.Split( ':' );
					ProcessArgument( kvl[ 0 ], kvl[ 1 ] );
				}
			}

			if( _solveForShovel ) {
				Console.WriteLine( "Solving this with shovel. This could take exceptionally long." );
			} else {
				Console.WriteLine( "Not solving for the shovel" );
			}

			var mode = args[ 0 ];
			var fileLocation = args[ 1 ];

			if( _createDiagLogs ) {
				_geneticLogFilename = "GeneticLog" + DateTime.Now.ToString( "yyyyMMdd-HHmmss" ) + ".txt";
				_crossoverlogFilename = "CrossoverHistoryLog" + DateTime.Now.ToString( "yyyyMMdd-HHmmss" ) + ".txt";
				Console.WriteLine( "Genetic log stored at " + _geneticLogFilename );
				Console.WriteLine( "Crossover log stored at " + _crossoverlogFilename );
			}

			_solutionFilename = "Solution" + DateTime.Now.ToString( "yyyyMMdd-HHmmss" ) + ".txt";
			_movesFilename = "Solution" + DateTime.Now.ToString( "yyyyMMdd-HHmmss" ) + "-moves.txt";

			if( File.Exists( fileLocation ) ) {
			    var game = FlowerzGame.CreateGameFromFile( fileLocation );

				if( mode == "-c" ) {
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
				} else if( mode == "-s" ) {
					//Solve
					var solver = SolverFactory.GetSolver( SolverType.GeneticAlgorithm );
					var gasolver = solver as Flowerz.Solver.GA.Solver;
					if( gasolver != null ) {
						gasolver.GenerationCreated += gasolver_GenerationCreated;
						gasolver.MovelistCreated += GasolverMovelistCreated;
						gasolver.CrossoverSelected += gasolver_CrossoverSelected;
						gasolver.SolverCreated += gasolver_SolverCreated;
						gasolver.SolveForShovel = _solveForShovel;

						if( _passedPopulationSize.HasValue ) {
							Console.WriteLine( "Population size = " + _passedPopulationSize.Value + "." );
							gasolver.PopulationSize = _passedPopulationSize.Value;
						}

						if( _passedNumberOfGenerations.HasValue ) {
							Console.WriteLine( "Algorithm will terminate after " + _passedNumberOfGenerations.Value + " generations" );
							gasolver.NumberOfGenerationsToCreate = _passedNumberOfGenerations.Value;
						}

						if( _passedFileNameWithSolution != null ) {
							if( File.Exists( _passedFileNameWithSolution ) ) {
								Console.WriteLine( "Importing existing solutions . . ." );
								var numberOfImportedSolutions = 0;
								using( var sr = new StreamReader( new FileStream( _passedFileNameWithSolution, FileMode.Open ) ) ) {
									string s;
									while( ( s = sr.ReadLine() ) != null ) {
										gasolver.ImportMovelist( s );
										numberOfImportedSolutions++;
									}
								}
								Console.WriteLine( numberOfImportedSolutions + " solutions imported" );
							} else {
								Console.WriteLine( "Unable to import solutions. Could not find file '" + _passedFileNameWithSolution + "'." );
							}
						}
					}

					var board = TranslateToSolverBoard( game.Board );
					var queue = TranslateToPiecesQueue( game.Queue );
					var solution = solver.Solve( board, queue );
					board.BoardChanged += board_BoardChanged;
					board.ApplyMoveList( solution );
					board.Reset();

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
						sw.WriteLine( "Leftover spaces after applying solution = " + solution.Score );
						sw.WriteLine( @"    A  B  C  D  E  F  G" );
						sw.WriteLine( @"01 __ __ __ __ __ __ __" );
						sw.WriteLine( @"02 __ __ __ __ __ __ __" );
						sw.WriteLine( @"03 __ __ __ __ __ __ __" );
						sw.WriteLine( @"04 __ __ __ __ __ __ __" );
						sw.WriteLine( @"05 __ __ __ __ __ __ __" );
						sw.WriteLine( @"06 __ __ __ __ __ __ __" );
						sw.WriteLine( @"07 __ __ __ __ __ __ __" );
						Console.WriteLine( solution.ToString() );
						sw.WriteLine();
						sw.WriteLine();
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
					Console.ReadLine();
				} else {
					Console.WriteLine( "Invalid mode specified. Please use -s or -c" );
					Console.WriteLine( "Press any key to continue . . ." );
					Console.ReadLine();
				}
			} else {
				Console.WriteLine( "File not found:" + fileLocation );
				Console.WriteLine( "Press any key to continue . . . " );
				Console.ReadLine();
			}
		}

		static void board_BoardChanged( BoardChangedEventArgs args ) {
			var linesPreviousState = args.PreviousState.Split( new string[] { Environment.NewLine }, StringSplitOptions.None );
			var linesCurrentState = args.CurrentState.Split( new string[] { Environment.NewLine }, StringSplitOptions.None );
			Console.WriteLine( "Board has changed: " + args.Move );
			Console.WriteLine( linesPreviousState[ 0 ] + "      " + linesCurrentState[ 0 ] );
			Console.WriteLine( linesPreviousState[ 1 ] + "      " + linesCurrentState[ 1 ] );
			Console.WriteLine( linesPreviousState[ 2 ] + "      " + linesCurrentState[ 2 ] );
			Console.WriteLine( linesPreviousState[ 3 ] + " ---> " + linesCurrentState[ 3 ] );
			Console.WriteLine( linesPreviousState[ 4 ] + "      " + linesCurrentState[ 4 ] );
			Console.WriteLine( linesPreviousState[ 5 ] + "      " + linesCurrentState[ 5 ] );
			Console.WriteLine( linesPreviousState[ 6 ] + "      " + linesCurrentState[ 6 ] );
			Console.WriteLine( "Spaces left:" + args.NumberOfFreeSpaces );
			Console.WriteLine();

			using( var sw = new StreamWriter( _movesFilename, true ) ) {
				sw.WriteLine( "Board has changed: " + args.Move );
				sw.WriteLine( linesPreviousState[ 0 ] + "      " + linesCurrentState[ 0 ] );
				sw.WriteLine( linesPreviousState[ 1 ] + "      " + linesCurrentState[ 1 ] );
				sw.WriteLine( linesPreviousState[ 2 ] + "      " + linesCurrentState[ 2 ] );
				sw.WriteLine( linesPreviousState[ 3 ] + " ---> " + linesCurrentState[ 3 ] );
				sw.WriteLine( linesPreviousState[ 4 ] + "      " + linesCurrentState[ 4 ] );
				sw.WriteLine( linesPreviousState[ 5 ] + "      " + linesCurrentState[ 5 ] );
				sw.WriteLine( linesPreviousState[ 6 ] + "      " + linesCurrentState[ 6 ] );
				sw.WriteLine( "Spaces left:" + args.NumberOfFreeSpaces );
				sw.WriteLine();
				sw.Flush();
				sw.Close();
			}
		}

		private static void gasolver_SolverCreated( Flowerz.Solver.GA.SolverCreatedEventArgs args ) {
			_nestLevel = args.NestLevel;
			Console.WriteLine( "".PadLeft( _nestLevel * 2, ' ' ) + "Solver created for a shovel. Current depth = " + args.NestLevel );
		}

		private static void ProcessArgument( string arg, string value ) {
			switch( arg ) {
				case "-popSize":
					_passedPopulationSize = Convert.ToInt32( value );
					break;
				case "-numGen":
					_passedNumberOfGenerations = Convert.ToInt32( value );
					break;
				case "-solutions":
					_passedFileNameWithSolution = value;
					break;
			}
		}

		private static void gasolver_CrossoverSelected( CrossoverEventArgs args ) {
			if( _createDiagLogs ) {
				using( var sw = new StreamWriter( _crossoverlogFilename, true ) ) {
					sw.WriteLine( args.MovesParent1 );
					sw.WriteLine( args.MovesParent2 );
					sw.WriteLine( "----------------------------------------------------------------------------------------------------" );
					sw.Flush();
					sw.Close();
				}
			}
		}

		private static void GasolverMovelistCreated( Flowerz.Solver.GA.MovelistCreatedEventArgs args ) {
			if( _createDiagLogs ) {
				using( var sw = new StreamWriter( _geneticLogFilename, true ) ) {
					sw.WriteLine( "Solution ({0}) created. Crossover: {1}. Mutation: {2}. Solution details:",
						//args.NameSolution,
						args.Score,
						( args.CreatedThroughCrossover ? "Yes" : "No" ),
						( args.CreatedThroughMutation ? "Yes" : "No" ) );
					sw.WriteLine( "  Moves: " + args.Moves );
					sw.Flush();
					sw.Close();
				}
			}
		}

		private static void gasolver_GenerationCreated( Flowerz.Solver.GA.GenerationCreatedEventArgs args ) {
			if( _createDiagLogs ) {
				using( var sw = new StreamWriter( _geneticLogFilename, true ) ) {
					sw.WriteLine( "Generation {0} created. Generation details:", args.GenerationNumber );
					sw.WriteLine( "  Highest score this generation: {0}. Highest score previous generation: {1}. Highest score possible: {2}", args.HighestScoreThisGeneration, args.HighestPreviousScore, args.MaximumNumberOfFreeSpaces );
					sw.WriteLine( "  Moves: " + args.HighestScoringMoves );
					sw.WriteLine( "----------------------------------------------------------------------------------------------------" );
					sw.Flush();
					sw.Close();
				}
			}
			if( args.GenerationNumber % 10 == 0 ) {
				Console.WriteLine( "".PadLeft( _nestLevel * 2, ' ' ) + "Current generation = " + args.GenerationNumber + ". Score = " + args.HighestPreviousScore + " out of " + args.MaximumNumberOfFreeSpaces );
			}
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

		private static Flowerz.SolverModel.Board TranslateToSolverBoard( Flowerz.Model.Board board ) {
			var b = new Flowerz.SolverModel.Board( board.GetBoardLayout() );
			return b;
		}
	}
}
