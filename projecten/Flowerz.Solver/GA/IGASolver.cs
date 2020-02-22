namespace Flowerz.Solver.GA {
	public interface IGASolver : ISolver {
        GeneticAlgorithmOptions GeneticAlgorithmOptions { get; set; }

        event MovelistCreatedHandler MovelistCreated;
        event GenerationCreatedHandler GenerationCreated;
        event CrossoverSelectedHandler CrossoverSelected;
        event SolverCreatedHandler SolverCreated;

	    void ImportMovelist( string ml );
	}
}