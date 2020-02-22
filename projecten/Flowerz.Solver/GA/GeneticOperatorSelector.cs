namespace Flowerz.Solver.GA {
    internal class GeneticOperatorSelector {
        public static GeneticOperation DetermineGeneticOperation( int crossoverChance, int mutationChance, IRandomizer r ) {
            var num = r.Next( 1, 101 );
            const int crossoverLowerBound = 1;
            var crossoverUpperBound = crossoverChance;

            var mutationLowerBound = crossoverUpperBound + 1;
            var mutationUpperBound = mutationLowerBound + ( mutationChance - 1 );

            if( num >= crossoverLowerBound && num <= crossoverUpperBound ) {
                return GeneticOperation.Crossover;
            }

            if( num >= mutationLowerBound && num <= mutationUpperBound ) {
                return GeneticOperation.Mutation;
            }

            return GeneticOperation.Reproduction;
        }
    }
}