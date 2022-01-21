using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Unity.Services.CCD.Management.Http
{
    /// <summary>
    /// Http client interface
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// Make request with byte[] body
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="headers"></param>
        /// <param name="requestTimeout"></param>
        /// <returns></returns>
        Task<HttpClientResponse> MakeRequestAsync(string method, string url, byte[] body, Dictionary<string, string> headers, int requestTimeout);
        /// <summary>
        /// Make request with multipart form body
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="headers"></param>
        /// <param name="requestTimeout"></param>
        /// <param name="boundary"></param>
        /// <returns></returns>
        Task<HttpClientResponse> MakeRequestAsync(string method, string url, List<IMultipartFormSection> body, Dictionary<string, string> headers, int requestTimeout, string boundary = null);
    }
}
