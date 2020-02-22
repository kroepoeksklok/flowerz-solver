using System;
using Flowerz.Solver.GA;

namespace Flowerz.Solver.Tests {
	public class InsertNextItemRandomizer : IRandomizer {
		private readonly int _firstInt;
		private readonly int _secondInt;
		private bool _firstNumberReturned;

		public InsertNextItemRandomizer( int firstNumber, int secondNumber ) {
			_firstInt = firstNumber;
			_secondInt = secondNumber;
		}

		public int Next( int maxValue ) {
			if( !_firstNumberReturned ) {
				_firstNumberReturned = true;
				return _firstInt;
			}
			return _secondInt;
		}

		public int Next( int minValue, int maxValue ) {
			if( !_firstNumberReturned ) {
				_firstNumberReturned = true;
				return _firstInt;
			}
			return _secondInt;
		}

		public double NextDouble() {
			throw new NotImplementedException();
		}

		public bool NextBoolean() {
			throw new NotImplementedException();
		}

		public System.Random GetRandom { get { return null; } }
	}
}