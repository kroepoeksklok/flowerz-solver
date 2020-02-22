using System;

namespace Flowerz.Solver.GA {
	public class MovelistCreatedEventArgs : EventArgs {
		public Boolean CreatedThroughMutation { get; set; }
		public Boolean CreatedThroughCrossover { get; set; }
		public Boolean CreatedThroughReproduction { get; set; }
		public int Score { get; set; }
		public String Moves { get; set; }
		//public String NameSolution { get; set; }
	}
}