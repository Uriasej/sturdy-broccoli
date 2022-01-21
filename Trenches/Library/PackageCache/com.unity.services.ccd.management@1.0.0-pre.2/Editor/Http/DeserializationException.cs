using System;

namespace Unity.Services.CCD.Management.Http
{
    /// <summary>
    /// Deserialization exception
    /// </summary>
    [Serializable]
    public class DeserializationException : Exception
    {
        /// <summary>
        /// Deserialization exception
        /// </summary>
        public DeserializationException() : base()
        {
        }

        /// <summary>
        /// Deserialization Exception
        /// </summary>
        /// <param name="message"></param>
        public DeserializationException(string message) : base(message)
        {
        }

        DeserializationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
