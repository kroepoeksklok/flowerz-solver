using System;
using System.Threading;

using Flowerz.SolverModel;

namespace Flowerz.Solver {
	public interface ISolver {
		MoveList Solve( SolverModel.Board board, PiecesQueue queue );
        event EventHandler<SolutionCreatedEventArgs> SolutionCreated;
        MoveList GetCurrentBest { get; }
        MoveList LastGeneratedSolution { get; }
        CancellationTokenSource CancellationTokenSource { get; set; }
	}
}
