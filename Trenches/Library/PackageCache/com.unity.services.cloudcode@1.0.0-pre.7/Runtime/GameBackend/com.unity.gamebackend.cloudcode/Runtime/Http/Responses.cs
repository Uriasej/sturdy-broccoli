using System;
using System.Collections.Generic;
using Unity.GameBackend.CloudCode.Http;

namespace Unity.GameBackend.CloudCode
{
    internal class Response {
        public Dictionary<string, string> Headers { get; }
        public long Status { get; set; }

        public Response(HttpClientResponse httpResponse)
        {
            this.Headers = httpResponse.Headers;
            this.Status = httpResponse.StatusCode;
        }
    }

    internal class Response<T> : Response
    {
        public T Result { get; }

        public Response(HttpClientResponse httpResponse, T result): base(httpResponse)
        {
            this.Result = result;
        }
    }
}