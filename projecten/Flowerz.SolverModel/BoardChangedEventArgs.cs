using System;

namespace Flowerz.SolverModel {
	public class BoardChangedEventArgs : EventArgs {
		public String PreviousState { get; set; }
		public String CurrentState { get; set; }
		public Move Move { get; set; }
		public int NumberOfFreeSpaces { get; set; }
	}
}