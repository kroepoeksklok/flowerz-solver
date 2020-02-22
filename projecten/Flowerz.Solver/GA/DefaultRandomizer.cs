using System;

namespace Flowerz.Solver.GA {
	public class DefaultRandomizer : IRandomizer {
		private readonly System.Random _random;
		public DefaultRandomizer( System.Random r ) {
			_random = r;
		}

		public int Next( int maxValue ) {
			return _random.Next( maxValue );
		}

		public int Next( int minValue, int maxValue ) {
			return _random.Next( minValue, maxValue );
		}

		public double NextDouble() {
			return _random.NextDouble();
		}

		public bool NextBoolean() {
			return Convert.ToBoolean( _random.Next( 2 ) );
		}

		public System.Random GetRandom {
			get { return _random; }
		}
	}
}