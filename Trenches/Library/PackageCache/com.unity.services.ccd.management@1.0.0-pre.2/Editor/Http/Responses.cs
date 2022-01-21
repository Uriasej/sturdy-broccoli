using System;
using System.Collections.Generic;
using Unity.Services.CCD.Management.Http;

namespace Unity.Services.CCD.Management
{
    /// <summary>
    /// Response
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Headers
        /// </summary>
        public Dictionary<string, string> Headers { get; }
        /// <summary>
        /// Status
        /// </summary>
        public long Status { get; set; }

        /// <summary>
        /// Response
        /// </summary>
        /// <param name="httpResponse"></param>
        public Response(HttpClientResponse httpResponse)
        {
            this.Headers = httpResponse.Headers;
            this.Status = httpResponse.StatusCode;
        }
    }

    /// <summary>
    /// Response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : Response
    {
        /// <summary>
        /// Result
        /// </summary>
        public T Result { get; }

        /// <summary>
        /// Response
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="result"></param>
        public Response(HttpClientResponse httpResponse, T result) : base(httpResponse)
        {
            this.Result = result;
        }
    }
}