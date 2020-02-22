using System;
using Flowerz.Solver.GA;

namespace Flowerz.Solver.Tests {
	/// <summary>
	/// Always returns 1.
	/// </summary>
	public class OneRandomizer : IRandomizer {
		public int Next( int maxValue ) {
			return 1;
		}

		public int Next( int minValue, int maxValue ) {
			return 1;
		}

		public double NextDouble() {
			return 1.0;
		}

		public bool NextBoolean() {
			return true;
		}

		public System.Random GetRandom { get { return null; } }
	}
}