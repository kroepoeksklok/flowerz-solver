using Flowerz.Solver.GA.Mutation;
using Flowerz.SolverModel;
using NUnit.Framework;

namespace Flowerz.Solver.Tests.GA.Mutation.Tests {
	public class OrderedMutationTests {
		private MoveList _movelist;

		[SetUp]
		public void SetUp() {
			_movelist = new MoveList{
				new Move( new Piece( PieceType.Flower, Color.Blue, Color.Cyan ), CoordinateProvider.GetCoordinate( 'A', 1 ) ),   //1
				new Move( new Piece( PieceType.Flower, Color.Red, Color.Cyan ), CoordinateProvider.GetCoordinate( 'B', 1 ) ),    //2
				new Move( new Piece( PieceType.Flower, Color.White, Color.Cyan ), CoordinateProvider.GetCoordinate( 'C', 1 ) ),  //3
				new Move( new Piece( PieceType.Flower, Color.Yellow, Color.Cyan ), CoordinateProvider.GetCoordinate( 'D', 1 ) ), //4
				new Move( new Piece( PieceType.Flower, Color.Pink, Color.Cyan ), CoordinateProvider.GetCoordinate( 'G', 1 ) ),   //5
				new Move( new Piece( PieceType.Flower, Color.Blue, Color.None ), CoordinateProvider.GetCoordinate( 'A', 2 ) ),   //6
				new Move( new Piece( PieceType.Flower, Color.Blue, Color.Red ), CoordinateProvider.GetCoordinate( 'A', 5 ) ),    //7
				new Move( new Piece( PieceType.Flower, Color.Blue, Color.White ), CoordinateProvider.GetCoordinate( 'A', 3 ) ),  //8
				new Move( new Piece( PieceType.Flower, Color.Blue, Color.Yellow ), CoordinateProvider.GetCoordinate( 'B', 4 ) ), //9
				new Move( new Piece( PieceType.Flower, Color.Blue, Color.Pink ), CoordinateProvider.GetCoordinate( 'D', 4 ) )    //10
			};
		}

		[TestFixture]
		public class MutationTest : OrderedMutationTests {
			[Test]
			public void TestOrderedMutationFirstHighThenLow() {
				var orderedMutation = new OrderedMutation( new InsertNextItemRandomizer( 6, 2 ) );
				//RandomizerFactory.CreateRandomizer( RandomizerType.TestHighLow ) );
				//randomizer returns 6 and 2
				var mutant = orderedMutation.Mutate( _movelist );

				//   0     1     2     3     4     5     6     7     8     9
				//(A,1);(B,1);(C,1);(D,1);(G,1);(A,2);(A,5);(A,3);(B,4);(D,4)
				// move Coordinate at 6 to 2
				//(A,1);(B,1);(A,5);(C,1);(D,1);(G,1);(A,2);(A,3);(B,4);(D,4)
				TestHelper( mutant[ 0 ], PieceType.Flower, Color.Blue, Color.Cyan, CoordinateProvider.GetCoordinate( 'A', 1 ) );
				TestHelper( mutant[ 1 ], PieceType.Flower, Color.Red, Color.Cyan, CoordinateProvider.GetCoordinate( 'B', 1 ) );
				TestHelper( mutant[ 2 ], PieceType.Flower, Color.White, Color.Cyan, CoordinateProvider.GetCoordinate( 'A', 5 ) );
				TestHelper( mutant[ 3 ], PieceType.Flower, Color.Yellow, Color.Cyan, CoordinateProvider.GetCoordinate( 'C', 1 ) );
				TestHelper( mutant[ 4 ], PieceType.Flower, Color.Pink, Color.Cyan, CoordinateProvider.GetCoordinate( 'D', 1 ) );
				TestHelper( mutant[ 5 ], PieceType.Flower, Color.Blue, Color.None, CoordinateProvider.GetCoordinate( 'G', 1 ) );
				TestHelper( mutant[ 6 ], PieceType.Flower, Color.Blue, Color.Red, CoordinateProvider.GetCoordinate( 'A', 2 ) );
				TestHelper( mutant[ 7 ], PieceType.Flower, Color.Blue, Color.White, CoordinateProvider.GetCoordinate( 'A', 3 ) );
				TestHelper( mutant[ 8 ], PieceType.Flower, Color.Blue, Color.Yellow, CoordinateProvider.GetCoordinate( 'B', 4 ) );
				TestHelper( mutant[ 9 ], PieceType.Flower, Color.Blue, Color.Pink, CoordinateProvider.GetCoordinate( 'D', 4 ) );
			}

			[Test]
			public void TestOrderedMutationFirstLowThenHigh() {
				var orderedMutation = new OrderedMutation( new InsertNextItemRandomizer( 2, 6 ) );
				//RandomizerFactory.CreateRandomizer( RandomizerType.TestLowHigh ) );
				//randomizer returns 2 and 6
				var mutant = orderedMutation.Mutate( _movelist );

				//   0     1     2     3     4     5     6     7     8     9
				//(A,1);(B,1);(C,1);(D,1);(G,1);(A,2);(A,5);(A,3);(B,4);(D,4)
				// move Coordinate at 2 to 6
				//(A,1);(B,1);(D,1);(G,1);(A,2);(A,5);(C,1);(A,3);(B,4);(D,4)
				TestHelper( mutant[ 0 ], PieceType.Flower, Color.Blue, Color.Cyan, CoordinateProvider.GetCoordinate( 'A', 1 ) );   //1
				TestHelper( mutant[ 1 ], PieceType.Flower, Color.Red, Color.Cyan, CoordinateProvider.GetCoordinate( 'B', 1 ) );    //2
				TestHelper( mutant[ 2 ], PieceType.Flower, Color.White, Color.Cyan, CoordinateProvider.GetCoordinate( 'D', 1 ) );  //3
				TestHelper( mutant[ 3 ], PieceType.Flower, Color.Yellow, Color.Cyan, CoordinateProvider.GetCoordinate( 'G', 1 ) ); //4 
				TestHelper( mutant[ 4 ], PieceType.Flower, Color.Pink, Color.Cyan, CoordinateProvider.GetCoordinate( 'A', 2 ) );   //5
				TestHelper( mutant[ 5 ], PieceType.Flower, Color.Blue, Color.None, CoordinateProvider.GetCoordinate( 'A', 5 ) );   //6
				TestHelper( mutant[ 6 ], PieceType.Flower, Color.Blue, Color.Red, CoordinateProvider.GetCoordinate( 'C', 1 ) );    //7
				TestHelper( mutant[ 7 ], PieceType.Flower, Color.Blue, Color.White, CoordinateProvider.GetCoordinate( 'A', 3 ) );  //8
				TestHelper( mutant[ 8 ], PieceType.Flower, Color.Blue, Color.Yellow, CoordinateProvider.GetCoordinate( 'B', 4 ) ); //9
				TestHelper( mutant[ 9 ], PieceType.Flower, Color.Blue, Color.Pink, CoordinateProvider.GetCoordinate( 'D', 4 ) );   //10
			}

			// ReSharper disable UnusedParameter.Local
			private static void TestHelper( Move move, PieceType requiredType, Color requiredOuterColor, Color requiredInnerColor, Coordinate requiredCoordinate ) {
				Assert.That( move.Coordinate.Column == requiredCoordinate.Column, " Expected " + requiredCoordinate.Column + ". but was " + move.Coordinate.Column );
				Assert.That( move.Coordinate.Row == requiredCoordinate.Row, " Expected " + requiredCoordinate.Row + ". but wWas " + move.Coordinate.Row );
				Assert.That( move.Piece.OuterColor == requiredOuterColor );
				Assert.That( move.Piece.InnerColor == requiredInnerColor );
				Assert.That( move.Piece.Type == requiredType );
			}
			// ReSharper restore UnusedParameter.Local
		}
	}
}
