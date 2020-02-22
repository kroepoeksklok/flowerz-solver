using System.Collections.Generic;
using System.Linq;

namespace Flowerz.SolverModel {
	/// <summary>
	/// The queue.
	/// </summary>
	public class PiecesQueue : List<Piece> {
		/// <summary>
		/// Prepends the specified piece and returns it as a new queue.
		/// </summary>
		/// <returns></returns>
		public PiecesQueue Prepend( Piece pieceToPrepend ) {
			var pq = new PiecesQueue {
				pieceToPrepend
			};
			pq.AddRange( this.Select( piece => new Piece( piece.Type, piece.OuterColor, piece.InnerColor ) ) );
			return pq;
		}

		/// <summary>
		/// Checks if the queue contains at least one shovel.
		/// </summary>
		/// <returns>True if the queue contains at least one shovel, false if it doesn't.</returns>
		public bool ContainsShovel() {
			return this.Any( t => t.Type == PieceType.Shovel );
		}

		/// <summary>
		/// Splits the queue by shovel. The shovel itself is removed
		/// </summary>
		/// <returns></returns>
		public List<PiecesQueue> SplitByShovel() {
			if( !ContainsShovel() ) {
				return new List<PiecesQueue> { this };
			} else {
				var returnList = new List<PiecesQueue>();
				var q = new PiecesQueue();

				foreach( var piece in this ) {
					if( piece.Type == PieceType.Shovel ) {
						if( q.Count > 0 ) {
							returnList.Add( q );
							q = new PiecesQueue();
						}
					} else {
						var p = new Piece( piece.Type, piece.OuterColor, piece.InnerColor );
						q.Add( p );
					}
				}
				returnList.Add( q );
				return returnList;
			}
		}
	}
}