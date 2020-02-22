using System;
using Flowerz.SolverModel;

namespace Flowerz.Solver.GA.Crossover {
	public interface ICrossoverStrategy {
		Tuple<MoveList, MoveList> DoCrossover( MoveList firstMovelist, MoveList secondMovelist );
	}
}