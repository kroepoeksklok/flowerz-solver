using NUnit.Framework;

namespace Flowerz.SolverModel.Tests {
	public class FieldTests {
		private Piece _piece;
		private Piece _onlyOuterColor;

		[SetUp]
		public void SetUp() {
			_piece = new Piece( PieceType.Flower, Color.Red, Color.Blue );
			_onlyOuterColor = new Piece( PieceType.Flower, Color.Red, Color.None );
		}

		[TestFixture]
		public class FieldConstructorTests : FieldTests {
			[Test]
			public void TestConstructor_PieceAndCoordinate_PropertiesSet() {
				var c = new Coordinate( 'A', 1 );
				//var p = new Piece { InnerColor = Color.Blue, OuterColor = Color.Cyan, Type = PieceType.Flower };
				var field = new Field( _piece, c );

				//Should NOT be the same pointer. Fields keep their own copies so they won't influence the queue
				//when processing matches.
				Assert.That( field.Piece != _piece );
				Assert.That( field.Piece.InnerColor == _piece.InnerColor );
				Assert.That( field.Piece.OuterColor == _piece.OuterColor );
				Assert.That( field.Piece.Type == _piece.Type );
				Assert.That( field.Coordinate == c );
				Assert.That( field.Coordinate.Row == c.Row );
				Assert.That( field.Coordinate.Column == c.Column );
				Assert.That( !field.MarkedForMatch );
			}
		}

		[TestFixture]
		public class ProcessMatchesTests : FieldTests {
			[Test]
			public void ProcessMatchTest_DoNothingWhenNotMarked_NothingChanged() {
				var c = new Coordinate( 'A', 1 );
				//var p = new Piece { InnerColor = Color.Blue, OuterColor = Color.Cyan, Type = PieceType.Flower };
				var field = new Field( _piece, c );
				field.ProcessMatch();

				//Should NOT be the same pointer. Fields keep their own copies so they won't influence the queue
				//when processing matches.
				Assert.That( field.Piece != _piece );
				Assert.That( field.Piece.InnerColor == _piece.InnerColor );
				Assert.That( field.Piece.OuterColor == _piece.OuterColor );
				Assert.That( field.Piece.Type == _piece.Type );
				Assert.That( !field.MarkedForMatch );
			}

			[Test]
			public void ProcessMatchTest_ProcessTieredFlowerMatchWhenMarked_FlowerChangedColour() {
				var c = new Coordinate( 'A', 1 );
				//var p = new Piece { InnerColor = Color.Blue, OuterColor = Color.Cyan, Type = PieceType.Flower };
				var field = new Field( _piece, c );
				field.MarkedForMatch = true;
				field.ProcessMatch();

				//Should NOT be the same pointer. Fields keep their own copies so they won't influence the queue
				//when processing matches.
				Assert.That( field.Piece != _piece );
				Assert.That( field.Piece.InnerColor == Color.None );
				Assert.That( field.Piece.OuterColor == Color.Blue );
				Assert.That( field.Piece.Type == _piece.Type );
				Assert.That( !field.MarkedForMatch );
			}

			[Test]
			public void ProcessMatchTest_ProcessSingleFlowerMatchWhenMarked_FlowerChangedColour() {
				var c = new Coordinate( 'A', 1 );
				//var p = new Piece { InnerColor = Color.None, OuterColor = Color.Cyan, Type = PieceType.Flower };
				var field = new Field( _onlyOuterColor, c );
				field.MarkedForMatch = true;
				field.ProcessMatch();

				//Should NOT be the same pointer. Fields keep their own copies so they won't influence the queue
				//when processing matches.
				Assert.That( field.Piece != _piece );
				Assert.That( field.Piece.InnerColor == Color.None );
				Assert.That( field.Piece.OuterColor == Color.None );
				Assert.That( field.Piece.Type == PieceType.Empty );
				Assert.That( !field.MarkedForMatch );
			}
		}
	}
}