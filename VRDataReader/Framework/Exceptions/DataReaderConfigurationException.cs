using System.Runtime.Serialization;

namespace VRDataReader.Framework.Exceptions
{
    [Serializable]
    public class DataReaderConfigurationException : Exception
    {
        public DataReaderConfigurationException()
        {
        }

        public DataReaderConfigurationException(string? message) : base(message)
        {
        }

        public DataReaderConfigurationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DataReaderConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}