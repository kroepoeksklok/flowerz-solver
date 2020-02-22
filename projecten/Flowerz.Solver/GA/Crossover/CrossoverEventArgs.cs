using System;

namespace Flowerz.Solver.GA.Crossover {
	public class CrossoverEventArgs : EventArgs {
		public String MovesParent1 { get; set; }
		public String MovesParent2 { get; set; }
	}
}