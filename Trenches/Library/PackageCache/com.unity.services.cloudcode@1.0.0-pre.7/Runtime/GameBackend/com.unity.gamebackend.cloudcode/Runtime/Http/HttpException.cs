using System;
using UnityEngine.Scripting;
using Unity.GameBackend.CloudCode;

namespace Unity.GameBackend.CloudCode.Http
{
    [Serializable]
    [Preserve]
    internal class HttpException : Exception
    {
        [Preserve]
        public HttpClientResponse Response;

        [Preserve]
        public HttpException() : base()
        {
        }

        [Preserve]
        public HttpException(string message) : base(message)
        {
        }

        [Preserve]
        public HttpException(string message, Exception inner) : base(message, inner)
        {
        }

        [Preserve]
        public HttpException(HttpClientResponse response) : base(response.ErrorMessage)
        {
            Response = response;
        }
    }

    [Serializable]
    [Preserve]
    internal class HttpException<T> : HttpException
    {
        [Preserve]
        public T ActualError;

        [Preserve]
        public HttpException() : base()
        {
        }

        [Preserve]
        public HttpException(string message) : base(message)
        {
        }

        [Preserve]
        public HttpException(string message, Exception inner) : base(message, inner)
        {
        }

        [Preserve]
        public HttpException(HttpClientResponse response, T actualError) : base(response)
        {
            ActualError = actualError;
        }
    }
}
