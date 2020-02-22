using Flowerz.SolverModel;

namespace Flowerz.Solver.GA.Mutation {
	public interface IMutationStrategy {
		//IRandomizer Randomizer { set; }
		MoveList Mutate( MoveList moveListToMutate );
	}
}
