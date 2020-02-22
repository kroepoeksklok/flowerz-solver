using System;

namespace Flowerz.Solver.GA.Mutation {
	public class MutationFactory {
		public static IMutationStrategy GetMutationStrategy( IRandomizer randomizer ) {
			return new OrderedMutation( randomizer );
		}
	}
}