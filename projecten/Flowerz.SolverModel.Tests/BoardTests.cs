using NUnit.Framework;

namespace Flowerz.SolverModel.Tests {
	public class BoardTests {
		//     A  B  C  D  E  F  G
		// 01 XX XX __ __ __ XX XX
		// 02 XX __ __ __ __ __ XX
		// 03 
		// 04 
		// 05 
		// 06 XX __ __ __ __ __ XX
		// 07 XX XX __ __ __ XX XX
		private Model.Field[ , ] _testBoard;
		private Model.Field[ , ] _horizontalMatchTestBoard;
		private Model.Field[ , ] _verticalMatchTestBoard;
		private Model.Field[ , ] _emptyBoard;
		private Model.Field[ , ] _failBoard;

		[SetUp]
		public void SetUp() {
			#region _testBoard
			_testBoard = new Model.Field[ 7, 7 ];
			for( var rowIndex = 0; rowIndex < 7; rowIndex++ ) {
				for( var columnIndex = 0; columnIndex < 7; columnIndex++ ) {
					_testBoard[ columnIndex, rowIndex ] = new Model.Field( Model.Piece.Empty );
				}
			}
			_testBoard[ 0, 0 ].Piece = Model.Piece.Rock;
			_testBoard[ 1, 0 ].Piece = Model.Piece.Rock;
			_testBoard[ 0, 1 ].Piece = Model.Piece.Rock;

			_testBoard[ 3, 2 ].Piece = Model.Piece.BlueWhite;

			_testBoard[ 6, 0 ].Piece = Model.Piece.Rock;
			_testBoard[ 5, 0 ].Piece = Model.Piece.Rock;
			_testBoard[ 6, 1 ].Piece = Model.Piece.Rock;

			_testBoard[ 0, 5 ].Piece = Model.Piece.Rock;
			_testBoard[ 0, 6 ].Piece = Model.Piece.Rock;
			_testBoard[ 1, 6 ].Piece = Model.Piece.Rock;

			_testBoard[ 6, 6 ].Piece = Model.Piece.Rock;
			_testBoard[ 6, 5 ].Piece = Model.Piece.Rock;
			_testBoard[ 5, 6 ].Piece = Model.Piece.Rock;
			#endregion

			#region _horizontalMatchTestBoard
			_horizontalMatchTestBoard = new Model.Field[ 7, 7 ];
			for( var rowIndex = 0; rowIndex < 7; rowIndex++ ) {
				for( var columnIndex = 0; columnIndex < 7; columnIndex++ ) {
					_horizontalMatchTestBoard[ columnIndex, rowIndex ] = new Model.Field( Model.Piece.Empty );
				}
			}
			_horizontalMatchTestBoard[ 1, 1 ].Piece = Model.Piece.White;
			_horizontalMatchTestBoard[ 1, 2 ].Piece = Model.Piece.White;
			#endregion

			#region _verticalMatchTestBoard
			_verticalMatchTestBoard = new Model.Field[ 7, 7 ];
			for( var rowIndex = 0; rowIndex < 7; rowIndex++ ) {
				for( var columnIndex = 0; columnIndex < 7; columnIndex++ ) {
					_verticalMatchTestBoard[ columnIndex, rowIndex ] = new Model.Field( Model.Piece.Empty );
				}
			}
			_verticalMatchTestBoard[ 1, 1 ].Piece = Model.Piece.White;
			_verticalMatchTestBoard[ 2, 1 ].Piece = Model.Piece.White;
			#endregion

			#region _emptyBoard
			_emptyBoard = new Model.Field[ 7, 7 ];
			for( var rowIndex = 0; rowIndex < 7; rowIndex++ ) {
				for( var columnIndex = 0; columnIndex < 7; columnIndex++ ) {
					_emptyBoard[ columnIndex, rowIndex ] = new Model.Field( Model.Piece.Empty );
				}
			}

			#endregion

			#region _failboard
			_failBoard = new Model.Field[ 7, 7 ];
			for( var rowIndex = 0; rowIndex < 7; rowIndex++ ) {
				for( var columnIndex = 0; columnIndex < 7; columnIndex++ ) {
					_failBoard[ columnIndex, rowIndex ] = new Model.Field( Model.Piece.Empty );
				}
			}

			#region Row 0
			_failBoard[ 0, 0 ].Piece = Model.Piece.Rock;
			_failBoard[ 0, 1 ].Piece = Model.Piece.Empty;
			_failBoard[ 0, 2 ].Piece = Model.Piece.Cyan;
			_failBoard[ 0, 3 ].Piece = Model.Piece.Red;
			_failBoard[ 0, 4 ].Piece = Model.Piece.Yellow;
			_failBoard[ 0, 5 ].Piece = Model.Piece.Empty;
			_failBoard[ 0, 6 ].Piece = Model.Piece.Rock;
			#endregion

			#region Row 1
			_failBoard[ 1, 0 ].Piece = Model.Piece.Blue;
			_failBoard[ 1, 1 ].Piece = Model.Piece.YellowBlue;
			_failBoard[ 1, 2 ].Piece = Model.Piece.PinkBlue;
			_failBoard[ 1, 3 ].Piece = Model.Piece.Pink;
			_failBoard[ 1, 4 ].Piece = Model.Piece.BlueRed;
			_failBoard[ 1, 5 ].Piece = Model.Piece.White;
			_failBoard[ 1, 6 ].Piece = Model.Piece.Empty;
			#endregion

			#region Row 2
			_failBoard[ 2, 0 ].Piece = Model.Piece.Pink;
			_failBoard[ 2, 1 ].Piece = Model.Piece.RedBlue;
			_failBoard[ 2, 2 ].Piece = Model.Piece.Empty;
			_failBoard[ 2, 3 ].Piece = Model.Piece.Blue;
			_failBoard[ 2, 4 ].Piece = Model.Piece.Pink;
			_failBoard[ 2, 5 ].Piece = Model.Piece.Empty;
			_failBoard[ 2, 6 ].Piece = Model.Piece.Red;
			#endregion

			#region Row 3
			_failBoard[ 3, 0 ].Piece = Model.Piece.Cyan;
			_failBoard[ 3, 1 ].Piece = Model.Piece.Empty;
			_failBoard[ 3, 2 ].Piece = Model.Piece.Red;
			_failBoard[ 3, 3 ].Piece = Model.Piece.CyanBlue;
			_failBoard[ 3, 4 ].Piece = Model.Piece.Empty;
			_failBoard[ 3, 5 ].Piece = Model.Piece.Empty;
			_failBoard[ 3, 6 ].Piece = Model.Piece.Pink;
			#endregion

			#region Row 4
			_failBoard[ 4, 0 ].Piece = Model.Piece.Empty;
			_failBoard[ 4, 1 ].Piece = Model.Piece.Empty;
			_failBoard[ 4, 2 ].Piece = Model.Piece.Pink;
			_failBoard[ 4, 3 ].Piece = Model.Piece.Cyan;
			_failBoard[ 4, 4 ].Piece = Model.Piece.Empty;
			_failBoard[ 4, 5 ].Piece = Model.Piece.Empty;
			_failBoard[ 4, 6 ].Piece = Model.Piece.Empty;
			#endregion

			#region Row 5
			_failBoard[ 5, 0 ].Piece = Model.Piece.Empty;
			_failBoard[ 5, 1 ].Piece = Model.Piece.Empty;
			_failBoard[ 5, 2 ].Piece = Model.Piece.Pink;
			_failBoard[ 5, 3 ].Piece = Model.Piece.Empty;
			_failBoard[ 5, 4 ].Piece = Model.Piece.Empty;
			_failBoard[ 5, 5 ].Piece = Model.Piece.Empty;
			_failBoard[ 5, 6 ].Piece = Model.Piece.Cyan;
			#endregion

			#region Row 6
			_failBoard[ 6, 0 ].Piece = Model.Piece.Rock;
			_failBoard[ 6, 1 ].Piece = Model.Piece.YellowWhite;
			_failBoard[ 6, 2 ].Piece = Model.Piece.YellowPink;
			_failBoard[ 6, 3 ].Piece = Model.Piece.BlueYellow;
			_failBoard[ 6, 4 ].Piece = Model.Piece.RedWhite;
			_failBoard[ 6, 5 ].Piece = Model.Piece.WhiteRed;
			_failBoard[ 6, 6 ].Piece = Model.Piece.Rock;
			#endregion

			#endregion
		}

		[TestFixture]
		public class ConstructorTests : BoardTests {
			[Test]
			public void TestMaximumNumberFreeFields_UsingTestBoard_37Fields() {
				var board = new Board( _testBoard );
				Assert.That( board.MaximumNumberOfFreeSpaces == 37 );
			}

			[Test]
			public void TestConstructor_UsingTestBoard_ShouldBeSame() {
				var board = new Board( _testBoard );
				var field = board[ CoordinateProvider.GetCoordinate( 'A', 1 ) ];
				Assert.That( field.Piece.Type == PieceType.Rock );
				field = board[ CoordinateProvider.GetCoordinate( 'B', 1 ) ];
				Assert.That( field.Piece.Type == PieceType.Rock );
				field = board[ CoordinateProvider.GetCoordinate( 'F', 1 ) ];
				Assert.That( field.Piece.Type == PieceType.Rock );
				field = board[ CoordinateProvider.GetCoordinate( 'G', 1 ) ];
				Assert.That( field.Piece.Type == PieceType.Rock );
				field = board[ CoordinateProvider.GetCoordinate( 'A', 2 ) ];
				Assert.That( field.Piece.Type == PieceType.Rock );
				field = board[ CoordinateProvider.GetCoordinate( 'G', 2 ) ];
				Assert.That( field.Piece.Type == PieceType.Rock );
				field = board[ CoordinateProvider.GetCoordinate( 'A', 6 ) ];
				Assert.That( field.Piece.Type == PieceType.Rock );
				field = board[ CoordinateProvider.GetCoordinate( 'G', 6 ) ];
				Assert.That( field.Piece.Type == PieceType.Rock );
				field = board[ CoordinateProvider.GetCoordinate( 'A', 7 ) ];
				Assert.That( field.Piece.Type == PieceType.Rock );
				field = board[ CoordinateProvider.GetCoordinate( 'B', 7 ) ];
				Assert.That( field.Piece.Type == PieceType.Rock );
				field = board[ CoordinateProvider.GetCoordinate( 'F', 7 ) ];
				Assert.That( field.Piece.Type == PieceType.Rock );
				field = board[ CoordinateProvider.GetCoordinate( 'G', 7 ) ];
				Assert.That( field.Piece.Type == PieceType.Rock );
				field = board[ CoordinateProvider.GetCoordinate( 'C', 4 ) ];
				Assert.That( field.Piece.Type == PieceType.Flower );
			}
		}

		[TestFixture]
		public class ScoreTests : BoardTests {
			[Test]
			public void TestScore_UsingTestBoard_36() {
				var board = new Board( _testBoard );
				Assert.That( board.GetNumberOfFreeSpaces() == 36 );
			}

			[Test]
			public void Test() {
				var board = new Board( _failBoard );
				Assert.That( board.GetNumberOfFreeSpaces() == 18, "Expected: 18. Was: " + board.GetNumberOfFreeSpaces() );
			}
		}

		[TestFixture]
		public class ResetTests : BoardTests {
			[Test]
			public void TestReset_ShouldBeSameAsStartBoardAfterReset() {
				var board = new Board( _testBoard );
				var newBoard = new Board( _testBoard );
				board.DoMove( new Move( new Piece( Model.Piece.YellowRed ), CoordinateProvider.GetCoordinate( 4, 4 ) ) );
				Assert.That( board.GetNumberOfFreeSpaces() == newBoard.GetNumberOfFreeSpaces() - 1 ); //One less
				board.Reset();
				Assert.That( board[ CoordinateProvider.GetCoordinate( 'E', 5 ) ].Piece.Type == PieceType.Empty );
			}
		}

		[TestFixture]
		public class HorizontalTests : BoardTests {
			//     A  B  C  D  E  F  G
			// 01 __ __ __ __ __ __ __
			// 02 __ W_ W_ __ __ __ __
			// 03 __ __ __ __ __ __ __
			// 04 __ __ __ __ __ __ __
			// 05 __ __ __ __ __ __ __
			// 06 __ __ __ __ __ __ __
			// 07 __ __ __ __ __ __ __
			[Test]
			public void HorizontalThreeMatchTest_BoardEmpty() {
				var move = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'D', 2 ) );
				var playBoard = new Board( _horizontalMatchTestBoard );
				playBoard.DoMove( move );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'D', 2 ) ].Piece.Type == PieceType.Empty );
			}

			[Test]
			public void HorizontalThreeMatchInterruptedByFlowerTest_RedLeft() {
				var move1 = new Move( new Piece( PieceType.Flower, Color.Red, Color.None ), CoordinateProvider.GetCoordinate( 'E', 2 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'D', 2 ) );

				var playBoard = new Board( _horizontalMatchTestBoard );
				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );

				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'D', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'E', 2 ) ].Piece.Type == PieceType.Flower );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'E', 2 ) ].Piece.OuterColor == Color.Red );
			}

			[Test]
			public void HorizontalThreeMatchInterruptedByRock_RedLeft() {
				_horizontalMatchTestBoard[ 1, 4 ].Piece = Model.Piece.Rock;
				var playBoard = new Board( _horizontalMatchTestBoard );
				var move = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'D', 2 ) );
				playBoard.DoMove( move );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'D', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'E', 2 ) ].Piece.Type == PieceType.Rock );
			}

			[Test]
			public void HorizontalFourMatchTest_BoardEmpty() {
				var move1 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'E', 2 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'D', 2 ) );

				var playBoard = new Board( _horizontalMatchTestBoard );
				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );

				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'D', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'E', 2 ) ].Piece.Type == PieceType.Empty );
			}

			[Test]
			public void HorizontalFiveMatchTest_BoardEmpty() {
				var move1 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'E', 2 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'F', 2 ) );
				var move3 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'D', 2 ) );
				var playBoard = new Board( _horizontalMatchTestBoard );
				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				playBoard.DoMove( move3 );

				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'D', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'E', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'F', 2 ) ].Piece.Type == PieceType.Empty );
			}
		}

		[TestFixture]
		public class VerticalTests : BoardTests {
			//     A  B  C  D  E  F  G
			// 01 __ __ __ __ __ __ __
			// 02 __ W_ __ __ __ __ __
			// 03 __ W_ __ __ __ __ __
			// 04 __ __ __ __ __ __ __
			// 05 __ __ __ __ __ __ __
			// 06 __ __ __ __ __ __ __
			// 07 __ __ __ __ __ __ __

			[Test]
			public void VerticalThreeMatchTest_BoardEmpty() {
				var move = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 4 ) );
				var playBoard = new Board( _verticalMatchTestBoard );
				playBoard.DoMove( move );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 4 ) ].Piece.Type == PieceType.Empty );
			}

			[Test]
			public void VerticalThreeMatchInterruptedByFlowerTest_RedLeft() {
				_verticalMatchTestBoard[ 4, 1 ].Piece = Model.Piece.Red;
				var move = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 4 ) );
				var playBoard = new Board( _verticalMatchTestBoard );
				playBoard.DoMove( move );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 4 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 5 ) ].Piece.Type == PieceType.Flower );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 5 ) ].Piece.OuterColor == Color.Red );
			}

			[Test]
			public void VerticalThreeMatchInterruptedByRock_RedLeft() {
				_verticalMatchTestBoard[ 4, 1 ].Piece = Model.Piece.Rock;
				var move = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 4 ) );
				var playBoard = new Board( _verticalMatchTestBoard );
				playBoard.DoMove( move );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 4 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 5 ) ].Piece.Type == PieceType.Rock );
			}

			[Test]
			public void VerticalFourMatchTest_BoardEmpty() {
				var move1 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 5 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 4 ) );
				var playBoard = new Board( _verticalMatchTestBoard );
				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 4 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 5 ) ].Piece.Type == PieceType.Empty );
			}

			[Test]
			public void VerticalFiveMatchTest_BoardEmpty() {
				var move1 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 5 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 6 ) );
				var move3 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 4 ) );
				var playBoard = new Board( _verticalMatchTestBoard );
				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				playBoard.DoMove( move3 );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 4 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 5 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 6 ) ].Piece.Type == PieceType.Empty );
			}
		}

		[TestFixture]
		public class LShapeTests : BoardTests {
			//     A  B  C  D  E  F  G
			// 01 __ __ __ __ __ __ __
			// 02 __ __ __ __ __ __ __
			// 03 __ __ __ __ __ __ __
			// 04 __ __ __ __ __ __ __
			// 05 __ __ __ __ __ __ __
			// 06 __ __ __ __ __ __ __
			// 07 __ __ __ __ __ __ __

			// XX XX XX
			// XX
			// XX
			[Test]
			public void TestLShape1() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 3 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 2 ) );
				var move3 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 1 ) );
				var move4 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 1 ) );
				var move5 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 1 ) );

				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				playBoard.DoMove( move3 );
				playBoard.DoMove( move4 );
				playBoard.DoMove( move5 );

				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 1 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 1 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.Type == PieceType.Empty );
			}

			// XX XX XX
			//       XX
			//       XX
			[Test]
			public void TestLShape2() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 3 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 2 ) );
				var move3 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 1 ) );
				var move4 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 1 ) );
				var move5 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 1 ) );

				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				playBoard.DoMove( move3 );
				playBoard.DoMove( move4 );
				playBoard.DoMove( move5 );

				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 1 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 1 ) ].Piece.Type == PieceType.Empty );
			}

			// XX
			// XX
			// XX XX XX
			[Test]
			public void TestLShape3() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 1 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 2 ) );
				var move3 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 3 ) );
				var move4 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 3 ) );
				var move5 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 3 ) );

				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				playBoard.DoMove( move3 );
				playBoard.DoMove( move4 );
				playBoard.DoMove( move5 );

				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 3 ) ].Piece.Type == PieceType.Empty );
			}

			//       XX
			//       XX
			// XX XX XX
			[Test]
			public void TestLShape4() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 1 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 2 ) );
				var move3 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 3 ) );
				var move4 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 3 ) );
				var move5 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 3 ) );

				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				playBoard.DoMove( move3 );
				playBoard.DoMove( move4 );
				playBoard.DoMove( move5 );

				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 1 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 3 ) ].Piece.Type == PieceType.Empty );
			}
		}

		[TestFixture]
		public class TShapeTests : BoardTests {
			//     A  B  C  D  E  F  G
			// 01 __ __ __ __ __ __ __
			// 02 __ __ __ __ __ __ __
			// 03 __ __ __ __ __ __ __
			// 04 __ __ __ __ __ __ __
			// 05 __ __ __ __ __ __ __
			// 06 __ __ __ __ __ __ __
			// 07 __ __ __ __ __ __ __

			// XX XX XX
			//    XX
			//    XX
			[Test]
			public void TestTShape1() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 1 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 1 ) );
				var move3 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 3 ) );
				var move4 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 2 ) );
				var move5 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 1 ) );

				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				playBoard.DoMove( move3 );
				playBoard.DoMove( move4 );
				playBoard.DoMove( move5 );

				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 1 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 1 ) ].Piece.Type == PieceType.Empty );
			}

			//    XX 
			//    XX
			// XX XX XX
			[Test]
			public void TestTShape2() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 3 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 3 ) );
				var move3 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 1 ) );
				var move4 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 2 ) );
				var move5 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 3 ) );

				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				playBoard.DoMove( move3 );
				playBoard.DoMove( move4 );
				playBoard.DoMove( move5 );

				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 1 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 3 ) ].Piece.Type == PieceType.Empty );
			}

			// XX    
			// XX XX XX
			// XX 
			[Test]
			public void TestTShape3() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 1 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 3 ) );
				var move3 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 2 ) );
				var move4 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 2 ) );
				var move5 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 2 ) );

				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				playBoard.DoMove( move3 );
				playBoard.DoMove( move4 );
				playBoard.DoMove( move5 );

				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 2 ) ].Piece.Type == PieceType.Empty );
			}

			//       XX
			// XX XX XX
			//       XX
			[Test]
			public void TestTShape4() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 1 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 3 ) );
				var move3 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'A', 2 ) );
				var move4 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'B', 2 ) );
				var move5 = new Move( new Piece( PieceType.Flower, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 2 ) );

				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				playBoard.DoMove( move3 );
				playBoard.DoMove( move4 );
				playBoard.DoMove( move5 );

				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 1 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 3 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'B', 2 ) ].Piece.Type == PieceType.Empty );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 2 ) ].Piece.Type == PieceType.Empty );
			}
		}

		[TestFixture]
		public class ApplyMatchesTests : BoardTests {
			[Test]
			public void PlaceButterFlyOnEmptyField() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Butterfly, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 1 ) );
				var moveSuccessful = playBoard.DoMove( move1 );
				Assert.That( moveSuccessful );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 1 ) ].Piece.Type == PieceType.Empty );
			}

			[Test]
			public void PlaceButterFlyOnFlower() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Flower, Color.Red, Color.Yellow ), CoordinateProvider.GetCoordinate( 'C', 1 ) );
				var move2 = new Move( new Piece( PieceType.Butterfly, Color.White, Color.None ), CoordinateProvider.GetCoordinate( 'C', 1 ) );
				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 1 ) ].Piece.Type == PieceType.Flower );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 1 ) ].Piece.OuterColor == Color.White );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'C', 1 ) ].Piece.InnerColor == Color.None );
			}

			[Test]
			public void PlaceFlowerOnRock() {
				_emptyBoard[ 3, 3 ].Piece = Model.Piece.Rock;
				var v = new Move( new Piece( PieceType.Flower, Color.Red, Color.None ), CoordinateProvider.GetCoordinate( 'D', 4 ) );
				var playBoard = new Board( _emptyBoard );
				var moveSuccessful = playBoard.DoMove( v );
				Assert.That( !moveSuccessful );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'D', 4 ) ].Piece.Type == PieceType.Rock );
			}

			[Test]
			public void PlaceFlowerOnEmptySpace() {
				var playBoard = new Board( _emptyBoard );
				var v = new Move( new Piece( PieceType.Flower, Color.Red, Color.None ), CoordinateProvider.GetCoordinate( 'A', 1 ) );
				var moveSuccessful = playBoard.DoMove( v );
				Assert.That( moveSuccessful );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.Type == PieceType.Flower );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.OuterColor == Color.Red );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.InnerColor == Color.None );
			}

			[Test]
			public void PlaceFlowerOnFlower() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Flower, Color.Red, Color.None ), CoordinateProvider.GetCoordinate( 'A', 1 ) );
				var move2 = new Move( new Piece( PieceType.Flower, Color.Blue, Color.None ), CoordinateProvider.GetCoordinate( 'A', 1 ) );
				playBoard.DoMove( move1 );
				var moveSuccessful = playBoard.DoMove( move2 );
				Assert.That( !moveSuccessful );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.Type == PieceType.Flower );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.OuterColor == Color.Red );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.InnerColor == Color.None );
			}

			[Test]
			public void PlaceNonFlowerOnEmpty_FalseResult() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Rock, Color.Red, Color.None ), CoordinateProvider.GetCoordinate( 'A', 1 ) );
				playBoard.DoMove( move1 );
				var moveSuccessful = playBoard.DoMove( move1 );
				Assert.That( !moveSuccessful );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.Type == PieceType.Empty );
			}

			[Test]
			public void TestFlowersOnBoard_NoFlowers_FalseResult() {
				var playBoard = new Board( _emptyBoard );
				Assert.That( !playBoard.FlowersOnBoard() );
			}

			[Test]
			public void TestFlowersOnBoard_OneOrMoreFlowers_TrueResult() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Flower, Color.Red, Color.None ), CoordinateProvider.GetCoordinate( 'A', 1 ) );
				playBoard.DoMove( move1 );
				Assert.That( playBoard.FlowersOnBoard() );
			}

			[Test]
			public void TestShovel_MoveFlower_FlowerMoved() {
				var playBoard = new Board( _emptyBoard );
				var move1 = new Move( new Piece( PieceType.Flower, Color.Red, Color.None ), CoordinateProvider.GetCoordinate( 'A', 1 ) );
				var move2 = new Move( new Piece( PieceType.Shovel, Color.None, Color.None ), CoordinateProvider.GetCoordinate( 'A', 1 ) ) {
					SecondCoordinate = CoordinateProvider.GetCoordinate( 'E', 4 )
				};

				playBoard.DoMove( move1 );
				playBoard.DoMove( move2 );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'E', 4 ) ].Piece.Type == PieceType.Flower );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'E', 4 ) ].Piece.OuterColor == Color.Red );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'E', 4 ) ].Piece.InnerColor == Color.None );
				Assert.That( playBoard[ CoordinateProvider.GetCoordinate( 'A', 1 ) ].Piece.Type == PieceType.Empty );
			}
		}
	}
}