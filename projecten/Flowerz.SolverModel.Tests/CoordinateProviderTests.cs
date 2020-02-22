using NUnit.Framework;

namespace Flowerz.SolverModel.Tests {
	public class CoordinateProviderTests {
		[TestFixture]
		public class TestGetCoordinate {
			[Test]
			public void GetValidCoordinate_ByBoardIndex() {
				var c = CoordinateProvider.GetCoordinate( 0, 0 );
				Assert.That( c.Column == 'A' );
				Assert.That( c.Row == 1 );

				var c1 = CoordinateProvider.GetCoordinate( 3, 6 );
				Assert.That( c1.Column == 'D' );
				Assert.That( c1.Row == 7 );
			}

			[Test]
			public void GetCoordinateTwice_SamePointers() {
				var c = CoordinateProvider.GetCoordinate( 0, 0 );
				var c1 = CoordinateProvider.GetCoordinate( 0, 0 );
				Assert.That( c == c1 );
			}

			[Test]
			public void GetValidCoordinateBySystem() {
				var c = CoordinateProvider.GetCoordinate( 'A', 1 );
				Assert.That( c.RowAsArrayIndex == 0 );
				Assert.That( c.ColumnAsArrayIndex == 0 );
			}

			[Test]
			[ExpectedException( typeof( InvalidCoordinateException ) )]
			public void GetInvalidCoordinates() {
				CoordinateProvider.GetCoordinate( 8, 8 );
			}
		}
	}
}