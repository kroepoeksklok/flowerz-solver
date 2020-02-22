using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Flowerz.SolverModel.Tests {
	public class MoveListTests {
		private const string ValidMoveListWithoutShovel = "18|RW => (E,2);BW => (B,4);WY => (F,2);YW => (E,5);B_ => (D,6);RY => (D,5);R_ => (C,2);BY => (C,3);WY => (D,1);YB => (G,4);RB => (D,3);YW => (F,5);BW => (B,3);YW => (D,2);YW => (E,4);RB => (D,4);YB => (C,5);YW => (C,6);WB => (F,3);YW => (F,4);BY => (C,4);WB => (E,6);RW => (C,4)";

		[TestFixture]
		public class MoveListImportTest : MoveListTests {
			[Test]
			public void TestParseMoveList_ValidMoveList_ReturnMoveList() {
				var ml = MoveList.ParseMoveList( ValidMoveListWithoutShovel );
				Assert.That( ml.Score == 18 );
				Assert.That( ml[ 0 ].Coordinate.Column == 'E' );
				Assert.That( ml[ 0 ].Coordinate.Row == 2 );
				Assert.That( ml[ 0 ].Piece.OuterColor == Color.Red );
				Assert.That( ml[ 0 ].Piece.InnerColor == Color.White );

				Assert.That( ml[ 1 ].Coordinate.Column == 'B' );
				Assert.That( ml[ 1 ].Coordinate.Row == 4 );
				Assert.That( ml[ 1 ].Piece.OuterColor == Color.Blue );
				Assert.That( ml[ 1 ].Piece.InnerColor == Color.White );

				Assert.That( ml[ 2 ].Coordinate.Column == 'F' );
				Assert.That( ml[ 2 ].Coordinate.Row == 2 );
				Assert.That( ml[ 2 ].Piece.OuterColor == Color.White );
				Assert.That( ml[ 2 ].Piece.InnerColor == Color.Yellow );

				Assert.That( ml.Count == 23 );
			}
		}

		[TestFixture]
		public class CompareTests : MoveListTests {
			[Test]
			public void TestCompare_EqualLists_Result0() {
				var ml1 = new MoveList {
					new Move( new Piece( Model.Piece.YellowPink ), CoordinateProvider.GetCoordinate( 0, 0 ) )
				};

				var ml2 = new MoveList {
					new Move( new Piece( Model.Piece.YellowPink ), CoordinateProvider.GetCoordinate( 0, 0 ) )
				};

				Assert.That( ml1.CompareTo( ml2 ) == 0 );
			}

			[Test]
			public void TestCompare_NotEqualLists_ResultNot0() {
				var ml1 = new MoveList{
					new Move( new Piece( Model.Piece.YellowPink ), CoordinateProvider.GetCoordinate( 0, 0 ) )
				};

				var ml2 = new MoveList{
					new Move( new Piece( Model.Piece.YellowPink ), CoordinateProvider.GetCoordinate( 1, 0 ) )
				};

				Assert.That( ml1.CompareTo( ml2 ) != 0 );
			}
		}
	}
}
