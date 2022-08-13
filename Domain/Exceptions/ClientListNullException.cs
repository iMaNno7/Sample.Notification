using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    internal class ClientListNullException : Exception
    {
        public ClientListNullException()
        {
        }

        public ClientListNullException(string? message) : base(message)
        {
        }

        public ClientListNullException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ClientListNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}