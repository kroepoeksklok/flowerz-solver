using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Flowerz.SolverModel {
	[Serializable]
	public class InvalidCoordinateException : Exception {
		[ExcludeFromCodeCoverage]
		public InvalidCoordinateException() { }

		[ExcludeFromCodeCoverage]
		public InvalidCoordinateException( String message ) : base( message ) { }

		[ExcludeFromCodeCoverage]
		protected InvalidCoordinateException( SerializationInfo info, StreamingContext context ) : base( info, context ) { }
	}
}