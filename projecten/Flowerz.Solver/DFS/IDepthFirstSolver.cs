namespace Flowerz.Solver.DFS {
    public interface IDepthFirstSolver : ISolver {
        DepthFirstSearchOptions SearchOptions { set; }
    }
}