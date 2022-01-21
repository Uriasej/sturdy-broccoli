using System;
using Unity.Services.CCD.Management.Scheduler;

namespace Unity.Services.CCD.Management.Http
{
    /// <summary>
    /// Abstract custom client class
    /// </summary>
    public abstract class BaseApiClient
    {
        /// <summary>
        /// Http Client
        /// </summary>
        protected readonly IHttpClient HttpClient;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="httpClient"></param>
        public BaseApiClient(IHttpClient httpClient)
        {
            HttpClient = httpClient ?? new HttpClient();
        }
    }
}