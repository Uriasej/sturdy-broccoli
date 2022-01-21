using System;

namespace Unity.Services.CCD.Management.Http
{
    /// <summary>
    /// Response Deserialization Exception
    /// </summary>
    [Serializable]
    public class ResponseDeserializationException : Exception
    {
        /// <summary>
        /// Response 
        /// </summary>
        public HttpClientResponse response;

        /// <summary>
        /// Response Deserialization Exception
        /// </summary>
        public ResponseDeserializationException() : base()
        {
        }

        /// <summary>
        /// Response Deserialization Exception
        /// </summary>
        /// <param name="message"></param>
        public ResponseDeserializationException(string message) : base(message)
        {
        }

        ResponseDeserializationException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Response Deserialization Exception        /// </summary>
        /// <param name="httpClientResponse"></param>
        public ResponseDeserializationException(HttpClientResponse httpClientResponse) : base(
            "Unable to Deserialize Http Client Response")
        {
            response = httpClientResponse;
        }

        /// <summary>
        /// Response Deserialization Exception
        /// </summary>
        /// <param name="httpClientResponse"></param>
        /// <param name="message"></param>
        public ResponseDeserializationException(HttpClientResponse httpClientResponse, string message) : base(
            message)
        {
            response = httpClientResponse;
        }
    }
}
