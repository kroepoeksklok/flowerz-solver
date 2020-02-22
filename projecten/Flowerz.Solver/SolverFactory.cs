using Flowerz.Solver.DFS;
using Flowerz.Solver.GA;

namespace Flowerz.Solver {
    public class SolverFactory {
        public static IGASolver GetGeneticAlgorithmSolver() {
            return new GA.Solver();
        }

        public static IDepthFirstSolver GetDfsSolver() {
            return new DepthFirstSolver();
        }
    }
}