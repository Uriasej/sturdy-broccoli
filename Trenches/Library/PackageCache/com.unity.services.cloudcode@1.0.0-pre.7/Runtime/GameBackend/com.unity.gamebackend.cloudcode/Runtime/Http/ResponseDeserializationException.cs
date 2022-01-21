using System;

namespace Unity.GameBackend.CloudCode.Http
{
    [Serializable]
    internal class ResponseDeserializationException : Exception
    {
        public HttpClientResponse response;

        public override string Message => InnerException != null ? $"{response.ErrorMessage}  |  {InnerException}" : response.ErrorMessage;

        public ResponseDeserializationException() : base()
        {
        }

        public ResponseDeserializationException(string message) : base(message)
        {
        }

        ResponseDeserializationException(string message, Exception inner) : base(message, inner)
        {
        }

        public ResponseDeserializationException(HttpClientResponse httpClientResponse) : base(
            "Unable to Deserialize Http Client Response")
        {
            response = httpClientResponse;
        }

        public ResponseDeserializationException(HttpClientResponse httpClientResponse, string message) : base(
            message)
        {
            response = httpClientResponse;
        }

        public ResponseDeserializationException(HttpClientResponse response, Exception inner)
            : base(response.ErrorMessage, inner)
        {
            this.response = response;
        }
    }
}
