using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.Services.CCD.Management.Http
{
    /// <summary>
    /// Custom http client response
    /// </summary>
    public class HttpClientResponse
    {
        /// <summary>
        /// Create http client response
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="statusCode"></param>
        /// <param name="isHttpError"></param>
        /// <param name="isNetworkError"></param>
        /// <param name="data"></param>
        /// <param name="errorMessage"></param>
        public HttpClientResponse(Dictionary<string, string> headers, long statusCode, bool isHttpError, bool isNetworkError, byte[] data, string errorMessage)
        {
            Headers = headers;
            StatusCode = statusCode;
            IsHttpError = isHttpError;
            IsNetworkError = isNetworkError;
            Data = data;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Headers
        /// </summary>
        public Dictionary<string, string> Headers { get; }

        /// <summary>
        /// Status Code
        /// </summary>
        public long StatusCode { get; }

        /// <summary>
        /// Is http error
        /// </summary>
        public bool IsHttpError { get; }

        /// <summary>
        /// Is network error
        /// </summary>
        public bool IsNetworkError { get; }

        /// <summary>
        /// Data
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; }
    }
}