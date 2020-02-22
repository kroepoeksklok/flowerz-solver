using System;
using System.Collections.Generic;
using System.Linq;
using Flowerz.SolverModel;

namespace Flowerz.Solver.GA {
	public class ChromosomeSelector {
		private int _totalFitness;
		private readonly List<SelectionItem> _items = new List<SelectionItem>();
		private readonly List<MoveList> _moves;

		public ChromosomeSelector( List<MoveList> moveLists ) {
			_moves = new List<MoveList>();
			foreach( var moveList in moveLists ) {
				_moves.Add( moveList );
			}
			CalculateFitnesses();
		}

		public MoveList Get( IRandomizer r ) {
			var d = Math.Round( r.NextDouble() * 100, 2, MidpointRounding.AwayFromZero );
			var ml = _items.Single( t => t.LowerIndex <= d && t.HigherIndex >= d ).MoveList;
			_moves.Remove( ml );
			CalculateFitnesses();
			return ml;
		}

		private void CalculateFitnesses() {
			_items.Clear();
			_totalFitness = 0;
			foreach( var movelist in _moves ) {
				_totalFitness += movelist.Score;
			}
			var lowerIndex = 0.0;
			foreach( var movelist in _moves ) {
				var chance = Math.Round( ( ( double ) movelist.Score / ( double ) _totalFitness ) * 100, 2, MidpointRounding.AwayFromZero );
				double upperIndex = Math.Round( lowerIndex + chance, 2, MidpointRounding.AwayFromZero );

				var i = new SelectionItem( lowerIndex, upperIndex, movelist );
				lowerIndex = Math.Round( upperIndex + 0.01D, 2, MidpointRounding.AwayFromZero );

				_items.Add( i );
			}
		}

		private class SelectionItem {
			public Double LowerIndex { get; private set; }
			public Double HigherIndex { get; private set; }
			public MoveList MoveList { get; private set; }

			public SelectionItem( double lowerindex, double higherindex, MoveList movelist ) {
				LowerIndex = lowerindex;
				HigherIndex = higherindex;
				MoveList = movelist;
			}
		}
	}
}