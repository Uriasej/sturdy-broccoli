using System;

namespace Unity.GameBackend.CloudCode.Http
{
    [Serializable]
    [Obsolete("This was made public unintentionally and should not be used.")]
    public class DeserializationException : Exception
    {
        public DeserializationException() : base()
        {
        }

        public DeserializationException(string message) : base(message)
        {
        }

        DeserializationException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    [Serializable]
    internal class DeserializationExceptionInternal : Exception
    {
        public DeserializationExceptionInternal() : base()
        {
        }

        public DeserializationExceptionInternal(string message) : base(message)
        {
        }

        DeserializationExceptionInternal(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
