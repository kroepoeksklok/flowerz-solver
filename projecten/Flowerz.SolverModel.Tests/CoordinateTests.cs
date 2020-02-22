using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Flowerz.SolverModel.Tests {
	public class CoordinateTests {

		#region Row tests
		[TestFixture]
		public class IndexToRowTests : CoordinateTests {
			[Test]
			public void TestIndexToRow_RowIndex0_Row1() {
				var coordinate = new Coordinate( 0, 0 );
				Assert.That( coordinate.Row == 1 );
			}

			[Test]
			public void TestIndexToRow_RowIndex1_Row2() {
				var coordinate = new Coordinate( 0, 1 );
				Assert.That( coordinate.Row == 2 );
			}

			[Test]
			public void TestIndexToRow_RowIndex2_Row3() {
				var coordinate = new Coordinate( 0, 2 );
				Assert.That( coordinate.Row == 3 );
			}

			[Test]
			public void TestIndexToRow_RowIndex3_Row4() {
				var coordinate = new Coordinate( 0, 3 );
				Assert.That( coordinate.Row == 4 );
			}

			[Test]
			public void TestIndexToRow_RowIndex4_Row5() {
				var coordinate = new Coordinate( 0, 4 );
				Assert.That( coordinate.Row == 5 );
			}

			[Test]
			public void TestIndexToRow_RowIndex5_Row6() {
				var coordinate = new Coordinate( 0, 5 );
				Assert.That( coordinate.Row == 6 );
			}

			[Test]
			public void TestIndexToRow_RowIndex6_Row7() {
				var coordinate = new Coordinate( 0, 6 );
				Assert.That( coordinate.Row == 7 );
			}
		}

		[TestFixture]
		public class RowToIndexTests : CoordinateTests {
			[Test]
			public void TestRowToIndex_Row1_Index0() {
				var coordinate = new Coordinate( 'A', 1 );
				Assert.That( coordinate.RowAsArrayIndex == 0 );
			}

			[Test]
			public void TestRowToIndex_Row2_Index1() {
				var coordinate = new Coordinate( 'A', 2 );
				Assert.That( coordinate.RowAsArrayIndex == 1 );
			}

			[Test]
			public void TestRowToIndex_Row3_Index2() {
				var coordinate = new Coordinate( 'A', 3 );
				Assert.That( coordinate.RowAsArrayIndex == 2 );
			}

			[Test]
			public void TestRowToIndex_Row4_Index3() {
				var coordinate = new Coordinate( 'A', 4 );
				Assert.That( coordinate.RowAsArrayIndex == 3 );
			}

			[Test]
			public void TestRowToIndex_Row5_Index4() {
				var coordinate = new Coordinate( 'A', 5 );
				Assert.That( coordinate.RowAsArrayIndex == 4 );
			}

			[Test]
			public void TestRowToIndex_Row6_Index5() {
				var coordinate = new Coordinate( 'A', 6 );
				Assert.That( coordinate.RowAsArrayIndex == 5 );
			}

			[Test]
			public void TestRowToIndex_Row7_Index6() {
				var coordinate = new Coordinate( 'A', 7 );
				Assert.That( coordinate.RowAsArrayIndex == 6 );
			}
		}
		#endregion

		#region Column tests
		[TestFixture]
		public class IndexToColumnTests : CoordinateTests {
			[Test]
			public void TestIndexToColumn_RowIndex0_ColumnA() {
				var coordinate = new Coordinate( 0, 0 );
				Assert.That( coordinate.Column == 'A' );
			}

			[Test]
			public void TestIndexToColumn_RowIndex1_ColumnB() {
				var coordinate = new Coordinate( 1, 0 );
				Assert.That( coordinate.Column == 'B' );
			}

			[Test]
			public void TestIndexToColumn_RowIndex2_ColumnC() {
				var coordinate = new Coordinate( 2, 0 );
				Assert.That( coordinate.Column == 'C' );
			}

			[Test]
			public void TestIndexToColumn_RowIndex3_ColumnD() {
				var coordinate = new Coordinate( 3, 0 );
				Assert.That( coordinate.Column == 'D' );
			}

			[Test]
			public void TestIndexToColumn_RowIndex4_ColumnE() {
				var coordinate = new Coordinate( 4, 0 );
				Assert.That( coordinate.Column == 'E' );
			}

			[Test]
			public void TestIndexToColumn_RowIndex5_ColumnF() {
				var coordinate = new Coordinate( 5, 0 );
				Assert.That( coordinate.Column == 'F' );
			}

			[Test]
			public void TestIndexToColumn_RowIndex6_ColumnG() {
				var coordinate = new Coordinate( 6, 0 );
				Assert.That( coordinate.Column == 'G' );
			}

			[Test]
			public void TestIndexToColumn_InvalidRowIndex_ColumnZ() {
				var coordinate = new Coordinate( 21, 0 );
				Assert.That( coordinate.Column == 'Z' );
			}
		}

		[TestFixture]
		public class ColumnToIndexTests : CoordinateTests {
			[Test]
			public void TestColumnToIndex_ColumnA_RowIndex0() {
				var coordinate = new Coordinate( 'A', 0 );
				Assert.That( coordinate.ColumnAsArrayIndex == 0 );
			}

			[Test]
			public void TestColumnToIndex_ColumnB_RowIndex1() {
				var coordinate = new Coordinate( 'B', 0 );
				Assert.That( coordinate.ColumnAsArrayIndex == 1 );
			}

			[Test]
			public void TestColumnToIndex_ColumnC_RowIndex2() {
				var coordinate = new Coordinate( 'C', 0 );
				Assert.That( coordinate.ColumnAsArrayIndex == 2 );
			}

			[Test]
			public void TestColumnToIndex_ColumnD_RowIndex3() {
				var coordinate = new Coordinate( 'D', 0 );
				Assert.That( coordinate.ColumnAsArrayIndex == 3 );
			}

			[Test]
			public void TestColumnToIndex_ColumnE_RowIndex4() {
				var coordinate = new Coordinate( 'E', 0 );
				Assert.That( coordinate.ColumnAsArrayIndex == 4 );
			}

			[Test]
			public void TestColumnToIndex_ColumnF_RowIndex5() {
				var coordinate = new Coordinate( 'F', 0 );
				Assert.That( coordinate.ColumnAsArrayIndex == 5 );
			}

			[Test]
			public void TestColumnToIndex_ColumnG_RowIndex6() {
				var coordinate = new Coordinate( 'G', 0 );
				Assert.That( coordinate.ColumnAsArrayIndex == 6 );
			}

			[Test]
			public void TestColumnToIndex_ColumnZ_RowIndexNegative() {
				var coordinate = new Coordinate( 'Z', 0 );
				Assert.That( coordinate.ColumnAsArrayIndex < 0 );
			}
		}
		#endregion

		[TestFixture]
		public class ToStringTests : CoordinateTests {
			[Test]
			public void TestToString_ValidCoordinate_CorrectString() {
				var c = new Coordinate( 'A', 1 );
				Assert.That( c.ToString() == "(A,1)" );
			}
		}

		[TestFixture]
		public class CopyConstructorTests : CoordinateTests {
			[Test]
			public void TestCopyConstructor_IdenticalCoordinate() {
				var c = new Coordinate( 'A', 1 );
				var c1 = new Coordinate( c );
				Assert.That( c.Column == c1.Column );
				Assert.That( c.Row == c1.Row );
			}
		}

		[TestFixture]
		public class CompareTests : CoordinateTests {
			[Test]
			public void CompareCoordinates_AreEqual_Result0() {
				var coordinate1 = new Coordinate( 'A', 1 );
				var coordinate2 = new Coordinate( 'A', 1 );
				Assert.That( coordinate1.Equals( coordinate2 ) );
			}

			[Test]
			public void CompareCoordinates_DifferentColumn_ResultNot0() {
				var coordinate1 = new Coordinate( 'A', 1 );
				var coordinate2 = new Coordinate( 'A', 2 );
				Assert.That( !coordinate1.Equals( coordinate2 ) );
			}

			[Test]
			public void CompareCoordinates_DifferentRow_ResultNot0() {
				var coordinate1 = new Coordinate( 'A', 1 );
				var coordinate2 = new Coordinate( 'B', 1 );
				Assert.That( !coordinate1.Equals( coordinate2 ) );
			}

			[Test]
			public void CompareCoordinates_DifferentRowAndColumn_ResultNot0() {
				var coordinate1 = new Coordinate( 'A', 1 );
				var coordinate2 = new Coordinate( 'B', 2 );
				Assert.That( !coordinate1.Equals( coordinate2 ) );
			}
		}
	}
}
