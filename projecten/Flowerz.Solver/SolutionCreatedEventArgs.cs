using System;

using Flowerz.SolverModel;

namespace Flowerz.Solver {
    public class SolutionCreatedEventArgs : EventArgs {
        public int MaxScore { get; set; }
        public MoveList CurrentBestMoveList { get; set; }
    }
}