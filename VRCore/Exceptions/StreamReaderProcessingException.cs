using System.Runtime.Serialization;

namespace VRCore.Exceptions
{
    [Serializable]
    public class StreamReaderProcessingException : Exception
    {
        public StreamReaderProcessingException()
        {
        }

        public StreamReaderProcessingException(string? message) : base(message)
        {
        }

        public StreamReaderProcessingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected StreamReaderProcessingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}