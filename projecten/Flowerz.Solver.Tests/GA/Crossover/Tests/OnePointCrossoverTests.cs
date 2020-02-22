using Flowerz.Solver.GA.Crossover;
using Flowerz.SolverModel;
using NUnit.Framework;

namespace Flowerz.Solver.Tests.GA.Crossover.Tests {
	public class TwoPointCrossoverTests {
		private MoveList _moveList4Moves1;
		private MoveList _moveList4Moves2;

		[SetUp]
		public void SetUp() {
			_moveList4Moves1 = new MoveList{
				new Move(new Piece(PieceType.Flower, Color.Blue, Color.Red), CoordinateProvider.GetCoordinate('F', 7)),
				new Move(new Piece(PieceType.Flower, Color.White, Color.Cyan), CoordinateProvider.GetCoordinate('B', 4)),
				new Move(new Piece(PieceType.Flower, Color.Red, Color.Blue), CoordinateProvider.GetCoordinate('G', 4)),
				new Move(new Piece(PieceType.Flower, Color.Blue, Color.Red), CoordinateProvider.GetCoordinate('C', 6))
			};

			_moveList4Moves2 = new MoveList{
				new Move(new Piece(PieceType.Flower, Color.Blue, Color.Red), CoordinateProvider.GetCoordinate('F', 2)),
				new Move(new Piece(PieceType.Flower, Color.White, Color.Cyan), CoordinateProvider.GetCoordinate('E', 7)),
				new Move(new Piece(PieceType.Flower, Color.Red, Color.Blue), CoordinateProvider.GetCoordinate('A', 5)),
				new Move(new Piece(PieceType.Flower, Color.Blue, Color.Red), CoordinateProvider.GetCoordinate('B', 7))
			};
		}

		[TestFixture]
		public class TwoPointCrossoverTest : TwoPointCrossoverTests {
			[Test]
			public void TestTwoPointCrossOver_ItemsIndex1And2SwappedInChildren() {
				var r = new InsertNextItemRandomizer( 1, 3 );
				var tpco = new TwoPointCrossover( r );
				var c = tpco.DoCrossover( _moveList4Moves1, _moveList4Moves2 );

				AssertMove( c.Item1[ 0 ], Color.Blue, Color.Red, CoordinateProvider.GetCoordinate( 'F', 7 ) );
				AssertMove( c.Item1[ 1 ], Color.White, Color.Cyan, CoordinateProvider.GetCoordinate( 'E', 7 ) );
				AssertMove( c.Item1[ 2 ], Color.Red, Color.Blue, CoordinateProvider.GetCoordinate( 'A', 5 ) );
				AssertMove( c.Item1[ 3 ], Color.Blue, Color.Red, CoordinateProvider.GetCoordinate( 'C', 6 ) );

				AssertMove( c.Item2[ 0 ], Color.Blue, Color.Red, CoordinateProvider.GetCoordinate( 'F', 2 ) );
				AssertMove( c.Item2[ 1 ], Color.White, Color.Cyan, CoordinateProvider.GetCoordinate( 'B', 4 ) );
				AssertMove( c.Item2[ 2 ], Color.Red, Color.Blue, CoordinateProvider.GetCoordinate( 'G', 4 ) );
				AssertMove( c.Item2[ 3 ], Color.Blue, Color.Red, CoordinateProvider.GetCoordinate( 'B', 7 ) );

			}

			private static void AssertMove( Move c, Color requiredOuterColor, Color requiredInnerColor, Coordinate requiredCoordinate ) {
				Assert.That( c.Coordinate.Equals( requiredCoordinate ) );
				Assert.That( c.Piece.Type == PieceType.Flower );
				Assert.That( c.Piece.OuterColor == requiredOuterColor );
				Assert.That( c.Piece.InnerColor == requiredInnerColor );
			}
		}
	}

	public class OnePointCrossoverTests {
		private MoveList _parentWithSizeTwoA;
		private MoveList _parentWithSizeTwoB;

		private MoveList _parentWithSizeOneA;
		private MoveList _parentWithSizeOneB;


		// _parentA
		//   PR -> A1
		//   BY -> A2

		// _parentB
		//   PR -> E1
		//   BY -> G2

		[SetUp]
		public void SetUp() {
			_parentWithSizeTwoA = new MoveList{
				new Move(new Piece(PieceType.Flower, Color.Pink, Color.Red), CoordinateProvider.GetCoordinate('A', 1)),
				new Move(new Piece(PieceType.Flower, Color.Blue, Color.Yellow), CoordinateProvider.GetCoordinate('A', 2))
			};

			_parentWithSizeTwoB = new MoveList{
				new Move(new Piece(PieceType.Flower, Color.Pink, Color.Red), CoordinateProvider.GetCoordinate('E', 1)),
				new Move(new Piece(PieceType.Flower, Color.Blue, Color.Yellow), CoordinateProvider.GetCoordinate('G', 2))
			};

			_parentWithSizeOneA = new MoveList{
				new Move(new Piece(PieceType.Flower, Color.Pink, Color.Red), CoordinateProvider.GetCoordinate('D', 3)),
			};

			_parentWithSizeOneB = new MoveList{
				new Move(new Piece(PieceType.Flower, Color.Pink, Color.Red), CoordinateProvider.GetCoordinate('B', 6)),
			};
		}

		[TestFixture]
		public class MovelistLengthTwoTests : OnePointCrossoverTests {
			[Test]
			public void TestMoveListLengthTwo_SwapFirstElements() {
				var opc = new OnePointCrossover( new OneRandomizer() );
				var children = opc.DoCrossover( _parentWithSizeTwoA, _parentWithSizeTwoB );

				Assert.That( children.Item1[ 0 ].Coordinate == CoordinateProvider.GetCoordinate( 'E', 1 ) );
				Assert.That( children.Item1[ 1 ].Coordinate == CoordinateProvider.GetCoordinate( 'A', 2 ) );

				Assert.That( children.Item2[ 0 ].Coordinate == CoordinateProvider.GetCoordinate( 'A', 1 ) );
				Assert.That( children.Item2[ 1 ].Coordinate == CoordinateProvider.GetCoordinate( 'G', 2 ) );
			}

			[Test]
			public void TestMoveListLengthTwo_SwapLastElements() {
				var opc = new OnePointCrossover( new ZeroRandomizer() );
				var children = opc.DoCrossover( _parentWithSizeTwoA, _parentWithSizeTwoB );

				Assert.That( children.Item1[ 0 ].Coordinate == CoordinateProvider.GetCoordinate( 'A', 1 ) );
				Assert.That( children.Item1[ 1 ].Coordinate == CoordinateProvider.GetCoordinate( 'G', 2 ) );

				Assert.That( children.Item2[ 0 ].Coordinate == CoordinateProvider.GetCoordinate( 'E', 1 ) );
				Assert.That( children.Item2[ 1 ].Coordinate == CoordinateProvider.GetCoordinate( 'A', 2 ) );
			}
		}

		[TestFixture]
		public class MovelistLengthOneTests : OnePointCrossoverTests {
			[Test]
			public void TestMovelistLengthOne_SameLists() {
				var opc = new OnePointCrossover( new ZeroRandomizer() ); //Randomizer doesn't matter
				var children = opc.DoCrossover( _parentWithSizeOneA, _parentWithSizeOneB );

				Assert.That( children.Item1[ 0 ].Coordinate == CoordinateProvider.GetCoordinate( 'D', 3 ) );
				Assert.That( children.Item2[ 0 ].Coordinate == CoordinateProvider.GetCoordinate( 'B', 6 ) );
			}
		}
	}
}
