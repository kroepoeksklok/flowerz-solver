using System;
using System.Runtime.Serialization;

namespace Flowerz.Model {
    [Serializable]
    public class InvalidQueueException : Exception {
        public InvalidQueueException() { }
        public InvalidQueueException( string message ) : base( message ) { }
        public InvalidQueueException( string message, Exception innerException ) : base( message, innerException ) { }
        protected InvalidQueueException( SerializationInfo info, StreamingContext context ) : base( info, context ) { }
    }
}