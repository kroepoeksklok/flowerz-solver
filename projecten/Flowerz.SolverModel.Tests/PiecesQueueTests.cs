using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Flowerz.SolverModel.Tests {
	public class PiecesQueueTests {
		private Piece _piece;

		[SetUp]
		public void SetUp() {
			_piece = new Piece( PieceType.Flower, Color.Blue, Color.Cyan );
		}

		[TestFixture]
		public class AddTests : PiecesQueueTests {
			[Test]
			public void TestAdd_SomeQueue_SizeIncreasedOne() {
				var p = new PiecesQueue();
				Assert.That( !p.Any() );
				p.Add( _piece );
				Assert.That( p.Count() == 1, "Size was " + p.Count() );
			}
		}

		[TestFixture]
		public class IndexerTests : PiecesQueueTests {
			[Test]
			public void TestIndexerValidElement_Match() {
				var p = new PiecesQueue { _piece };
				Assert.That( p[ 0 ] == _piece );
			}

			[Test]
			[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
			public void TestInvalidIndexer_GetIndexerTooHigh_Exception() {
				var p = new PiecesQueue();
				var x = p[ 10 ];
			}

			[Test]
			[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
			public void TestInvalidIndexer_GetIndexerTooLow_Exception() {
				var p = new PiecesQueue();
				var x = p[ -1 ];
			}

			[Test]
			public void TestValidSet_ValidIndexer_ItemReplaced() {
				var q = new PiecesQueue { _piece };
				q[ 0 ] = new Piece( PieceType.Flower, Color.Red, Color.White );
				Assert.That( q[ 0 ].OuterColor == Color.Red );
			}

			[Test]
			[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
			public void TestInvalidIndexer_Set_IndexerTooHigh() {
				var p = new PiecesQueue();
				p[ 2 ] = new Piece( PieceType.Flower, Color.Red, Color.White );
			}

			[Test]
			[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
			public void TestInvalidIndexer_Set_IndexerTooLow() {
				var p = new PiecesQueue();
				p[ -1 ] = new Piece( PieceType.Flower, Color.Red, Color.White );
			}
		}

		//[TestFixture]
		//public class RangeOfPiecesTests : PiecesQueueTests {
		//	[Test]
		//	public void TestRange_GetRemainingPieces() {
		//		var p = new PiecesQueue {
		//				new Piece(PieceType.Flower, Color.Blue, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Blue, Color.Red),
		//				new Piece(PieceType.Flower, Color.Blue, Color.Yellow),
		//				new Piece(PieceType.Flower, Color.Yellow, Color.Pink),
		//				new Piece(PieceType.Flower, Color.White, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Red, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Blue, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Blue, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Yellow, Color.Red)
		//			};
		//		var remainingQueue = p.GetRemainingPiecesBasedOnIndex( 4 );
		//		Assert.That( remainingQueue.Count == 4 );
		//		Assert.That( remainingQueue.All( t => t.Type == PieceType.Flower ) );

		//		Assert.That( remainingQueue[ 0 ].InnerColor == Color.Pink );
		//		Assert.That( remainingQueue[ 0 ].OuterColor == Color.Red );

		//		Assert.That( remainingQueue[ 1 ].InnerColor == Color.Pink );
		//		Assert.That( remainingQueue[ 1 ].OuterColor == Color.Blue );

		//		Assert.That( remainingQueue[ 2 ].InnerColor == Color.Pink );
		//		Assert.That( remainingQueue[ 2 ].OuterColor == Color.Blue );

		//		Assert.That( remainingQueue[ 3 ].InnerColor == Color.Red );
		//		Assert.That( remainingQueue[ 3 ].OuterColor == Color.Yellow );
		//	}

		//	[Test]
		//	public void TestRange_GetRemainingPieces_IndexAtEnd() {
		//		var p = new PiecesQueue {
		//				new Piece(PieceType.Flower, Color.Blue, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Blue, Color.Red),
		//				new Piece(PieceType.Flower, Color.Blue, Color.Yellow),
		//				new Piece(PieceType.Flower, Color.Yellow, Color.Pink),
		//				new Piece(PieceType.Flower, Color.White, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Red, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Blue, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Blue, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Yellow, Color.Red)
		//			};
		//		var remainingQueue = p.GetRemainingPiecesBasedOnIndex( 8 );
		//		Assert.That( remainingQueue.Count == 0 );
		//	}

		//	[Test]
		//	public void TestRange_GetRemainingPieces_StartIndex() {
		//		var p = new PiecesQueue {
		//				new Piece(PieceType.Flower, Color.Blue, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Blue, Color.Red),
		//				new Piece(PieceType.Flower, Color.Blue, Color.Yellow),
		//				new Piece(PieceType.Flower, Color.Yellow, Color.Pink),
		//				new Piece(PieceType.Flower, Color.White, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Red, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Blue, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Blue, Color.Pink),
		//				new Piece(PieceType.Flower, Color.Yellow, Color.Red)
		//			};
		//		var remainingQueue = p.GetRemainingPiecesBasedOnIndex( 0 );
		//		Assert.That( remainingQueue.Count == 8 );
		//		Assert.That( remainingQueue.All( t => t.Type == PieceType.Flower ) );

		//		Assert.That( remainingQueue[ 0 ].InnerColor == Color.Red );
		//		Assert.That( remainingQueue[ 0 ].OuterColor == Color.Blue );

		//		Assert.That( remainingQueue[ 1 ].InnerColor == Color.Yellow );
		//		Assert.That( remainingQueue[ 1 ].OuterColor == Color.Blue );

		//		Assert.That( remainingQueue[ 2 ].InnerColor == Color.Pink );
		//		Assert.That( remainingQueue[ 2 ].OuterColor == Color.Yellow );

		//		Assert.That( remainingQueue[ 3 ].InnerColor == Color.Pink );
		//		Assert.That( remainingQueue[ 3 ].OuterColor == Color.White );

		//		Assert.That( remainingQueue[ 4 ].InnerColor == Color.Pink );
		//		Assert.That( remainingQueue[ 4 ].OuterColor == Color.Red );

		//		Assert.That( remainingQueue[ 5 ].InnerColor == Color.Pink );
		//		Assert.That( remainingQueue[ 5 ].OuterColor == Color.Blue );

		//		Assert.That( remainingQueue[ 6 ].InnerColor == Color.Pink );
		//		Assert.That( remainingQueue[ 6 ].OuterColor == Color.Blue );

		//		Assert.That( remainingQueue[ 7 ].InnerColor == Color.Red );
		//		Assert.That( remainingQueue[ 7 ].OuterColor == Color.Yellow );
		//	}
		//}

		[TestFixture]
		public class EnumTests : PiecesQueueTests {
			[Test]
			public void TestEnum() {
				var q = new PiecesQueue();
				var enumerator = q.GetEnumerator();
				Assert.That( enumerator is List<Piece>.Enumerator, "Type = " + enumerator.GetType() );
			}
		}

		[TestFixture]
		public class ContainsShovelTests : PiecesQueueTests {
			[Test]
			public void ContainsShovel_NoShovel_ReturnFalse() {
				var p = new PiecesQueue { _piece };

				Assert.That( !p.ContainsShovel() );
			}

			[Test]
			public void ContainsShovel_HasShovel_ReturnTrue() {
				var p = new PiecesQueue {
					_piece, 
					new Piece( PieceType.Shovel, Color.None, Color.None )
				};

				Assert.That( p.ContainsShovel() );
			}
		}

		[TestFixture]
		public class SplitByShovelTests : PiecesQueueTests {
			[Test]
			public void TestSplitByShovel_NoShovel_SameList() {
				var p = new PiecesQueue {
							new Piece(PieceType.Flower, Color.Blue, Color.Pink),   //0
							new Piece(PieceType.Flower, Color.Blue, Color.Red),    //1
							new Piece(PieceType.Flower, Color.Yellow, Color.Red)   //2
						};
				var splitQueue = p.SplitByShovel();
				Assert.That( splitQueue.Count == 1 );
				Assert.That( splitQueue[ 0 ] == p );
				TestHelper( splitQueue[ 0 ][ 0 ], PieceType.Flower, Color.Blue, Color.Pink );
				TestHelper( splitQueue[ 0 ][ 1 ], PieceType.Flower, Color.Blue, Color.Red );
				TestHelper( splitQueue[ 0 ][ 2 ], PieceType.Flower, Color.Yellow, Color.Red );
			}

			[Test]
			public void TestSplitByShovel_OneShovel_TwoLists() {
				var p = new PiecesQueue {
							new Piece(PieceType.Flower, Color.Blue, Color.Pink),   //0
							new Piece(PieceType.Flower, Color.Blue, Color.Red),    //1
							new Piece(PieceType.Flower, Color.Yellow, Color.Red),  //2
							new Piece(PieceType.Shovel, Color.None, Color.None),   //3
							new Piece(PieceType.Flower, Color.Pink, Color.Cyan),   //4
							new Piece(PieceType.Flower, Color.Cyan, Color.White),  //5
						};
				var splitQueue = p.SplitByShovel();

				Assert.That( splitQueue.Count == 2 );
				Assert.That( splitQueue[ 0 ].Count == 3 );
				Assert.That( splitQueue[ 1 ].Count == 2 );

				TestHelper( splitQueue[ 0 ][ 0 ], PieceType.Flower, Color.Blue, Color.Pink );
				TestHelper( splitQueue[ 0 ][ 1 ], PieceType.Flower, Color.Blue, Color.Red );
				TestHelper( splitQueue[ 0 ][ 2 ], PieceType.Flower, Color.Yellow, Color.Red );

				TestHelper( splitQueue[ 1 ][ 0 ], PieceType.Flower, Color.Pink, Color.Cyan );
				TestHelper( splitQueue[ 1 ][ 1 ], PieceType.Flower, Color.Cyan, Color.White );
			}

			[Test]
			public void TestSplitByShovel_OneShovelAtFront_OneList() {
				var p = new PiecesQueue {
					new Piece( PieceType.Shovel, Color.None, Color.None ),
					new Piece( PieceType.Flower, Color.Blue, Color.Pink ),   //0
					new Piece( PieceType.Flower, Color.Blue, Color.Red ),    //1
					new Piece( PieceType.Flower, Color.Yellow, Color.Red )   //2
				};
				var splitQueue = p.SplitByShovel();
				Assert.That( splitQueue.Count == 1 );
				TestHelper( splitQueue[ 0 ][ 0 ], PieceType.Flower, Color.Blue, Color.Pink );
				TestHelper( splitQueue[ 0 ][ 1 ], PieceType.Flower, Color.Blue, Color.Red );
				TestHelper( splitQueue[ 0 ][ 2 ], PieceType.Flower, Color.Yellow, Color.Red );
			}

			// ReSharper disable UnusedParameter.Local
			private static void TestHelper( Piece piece, PieceType requiredType, Color requiredOuterColor, Color requiredInnerColor ) {
				Assert.That( piece.OuterColor == requiredOuterColor );
				Assert.That( piece.InnerColor == requiredInnerColor );
				Assert.That( piece.Type == requiredType );
			}
			// ReSharper restore UnusedParameter.Local

		}
	}
}