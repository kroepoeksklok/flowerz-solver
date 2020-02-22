using System;

namespace Flowerz.Solver.GA {
    public class GeneticAlgorithmOptions {
        public int PopulationSize { get; set; }
        public int NumberOfGenerationsToCreate { get; set; }
        public int CrossOverChance { get; set; }
        public int MutationChance { get; set; }

        public override string ToString() {
            return String.Format( @"Options:
Population size: {0}
Number of generations to create: {1}
Crossover chance: {2}%
Mutation chance: {3}%", PopulationSize, NumberOfGenerationsToCreate, CrossOverChance, MutationChance );
        }
    }
}