using System;

namespace Flowerz.Solver.ExperimentalDFS {
    public class SolutionCreatedEventArgs : EventArgs {
        public int MaxScore { get; set; }
        public MoveList GeneratedSolution { get; set; }
    }
}