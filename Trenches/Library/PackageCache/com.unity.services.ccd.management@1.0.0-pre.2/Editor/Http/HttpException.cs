using System;
using UnityEngine.Scripting;
using Unity.Services.CCD.Management;

namespace Unity.Services.CCD.Management.Http
{
    /// <summary>
    /// Http Exception
    /// </summary>
    [Serializable]
    [Preserve]
    public class HttpException : Exception
    {
        /// <summary>
        /// Http Exception
        /// </summary>
        [Preserve]
        public HttpClientResponse Response;

        /// <summary>
        /// Http Exception
        /// </summary>
        [Preserve]
        public HttpException() : base()
        {
        }

        /// <summary>
        /// Http Exception
        /// </summary>
        /// <param name="message"></param>
        [Preserve]
        public HttpException(string message) : base(message)
        {
        }

        /// <summary>
        /// Http Exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        [Preserve]
        public HttpException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Http Exception
        /// </summary>
        /// <param name="response"></param>
        [Preserve]
        public HttpException(HttpClientResponse response) : base(response.ErrorMessage)
        {
            Response = response;
        }
    }

    /// <summary>
    /// Http Exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [Preserve]
    public class HttpException<T> : HttpException
    {
        /// <summary>
        /// Actual Error
        /// </summary>
        [Preserve]
        public T ActualError;

        /// <summary>
        /// Http Exception
        /// </summary>
        [Preserve]
        public HttpException() : base()
        {
        }

        /// <summary>
        /// Http Exception
        /// </summary>
        /// <param name="message"></param>
        [Preserve]
        public HttpException(string message) : base(message)
        {
        }

        /// <summary>
        /// Http Exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        [Preserve]
        public HttpException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Http Exception
        /// </summary>
        /// <param name="response"></param>
        /// <param name="actualError"></param>
        [Preserve]
        public HttpException(HttpClientResponse response, T actualError) : base(response)
        {
            ActualError = actualError;
        }
    }
}
