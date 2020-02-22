using NUnit.Framework;

namespace Flowerz.SolverModel.Tests {
	public class PieceTests {
		[TestFixture]
		public class ConstructorTests : PieceTests {
			[Test]
			public void TestParserConstructor_ModelPiece_TypeFlowerMatchingColours() {
				var p = new Piece( Model.Piece.YellowWhite );
				Assert.That( p.Type == PieceType.Flower );
				Assert.That( p.InnerColor == Color.White );
				Assert.That( p.OuterColor == Color.Yellow );

				//TODO: write tests for other enum types.
			}

			[Test]
			public void TestRegularConstructor_ValidParams_MatchesParams() {
				var p = new Piece( PieceType.Flower, Color.Blue, Color.Pink );
				Assert.That( p.Type == PieceType.Flower );
				Assert.That( p.InnerColor == Color.Pink );
				Assert.That( p.OuterColor == Color.Blue );
			}
		}
	}
}