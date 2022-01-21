using System;
using Unity.GameBackend.CloudCode.Scheduler;

namespace Unity.GameBackend.CloudCode.Http
{
    internal abstract class BaseApiClient
    {
        protected readonly IHttpClient HttpClient;

        public BaseApiClient(IHttpClient httpClient)
        {
            HttpClient = httpClient ?? new HttpClient();
        }
    }
}
