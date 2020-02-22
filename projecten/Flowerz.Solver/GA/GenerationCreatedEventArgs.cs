using System;

namespace Flowerz.Solver.GA {
	public class GenerationCreatedEventArgs : EventArgs {
		public int GenerationNumber { get; set; }
		public int HighestScoreThisGeneration { get; set; }
		public int HighestPreviousScore { get; set; }
		public int MaximumNumberOfFreeSpaces { get; set; }
		public String HighestScoringMoves { get; set; }
	}
}