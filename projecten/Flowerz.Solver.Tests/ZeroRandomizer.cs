using System;
using Flowerz.Solver.GA;

namespace Flowerz.Solver.Tests {
	/// <summary>
	/// Always returns 0 or false.
	/// </summary>
	public class ZeroRandomizer : IRandomizer {
		public int Next( int maxValue ) {
			return 0;
		}

		public int Next( int minValue, int maxValue ) {
			return 0;
		}

		public double NextDouble() {
			return 0.0;
		}

		public bool NextBoolean() {
			return false;
		}

		public System.Random GetRandom { get { return null; } }
	}
}