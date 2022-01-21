using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.GameBackend.CloudCode.Models;
using Unity.GameBackend.CloudCode.Http;
using TaskScheduler = Unity.GameBackend.CloudCode.Scheduler.TaskSchedulerInternal;
using Unity.Services.Authentication.Internal;
using Unity.GameBackend.CloudCode.CloudCode;

namespace Unity.GameBackend.CloudCode.Apis.CloudCode
{
    internal interface ICloudCodeApiClient
    {
            /// <summary>
            /// Async Operation.
            /// Run Script
            /// </summary>
            /// <param name="request">Request object for RunScript</param>
            /// <param name="operationConfiguration">Configuration for RunScript</param>
            /// <returns>Task for a Response object containing status code, headers, and RunScriptResponse object</returns>
            /// <exception cref="Unity.GameBackend.CloudCode.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<RunScriptResponseInternal>> RunScriptAsync(RunScriptRequest request, Configuration operationConfiguration = null);

    }

    ///<inheritdoc cref="ICloudCodeApiClient"/>
    internal class CloudCodeApiClient : BaseApiClient, ICloudCodeApiClient
    {
        private IAccessToken _accessToken;
        private const int _baseTimeout = 10;
        private Configuration _configuration;
        public Configuration Configuration
        {
            get {
                // We return a merge between the current configuration and the
                // global configuration to ensure we have the correct
                // combination of headers and a base path (if it is set).
                Configuration globalConfiguration = new Configuration("https://cloud-code.services.api.unity.com", 10, 4, null);
                if (UnityServicesCloudCodeService.Instance != null)
                {
                    globalConfiguration = UnityServicesCloudCodeService.Instance.Configuration;
                }
                return Configuration.MergeConfigurations(_configuration, globalConfiguration);
            }
        }

        public CloudCodeApiClient(IHttpClient httpClient,
            IAccessToken accessToken,
            Configuration configuration = null) : base(httpClient)
        {
            // We don't need to worry about the configuration being null at
            // this stage, we will check this in the accessor.
            _configuration = configuration;

            _accessToken = accessToken;
        }


        public async Task<Response<RunScriptResponseInternal>> RunScriptAsync(RunScriptRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(RunScriptResponseInternal)   },{"400", typeof(RunScript400OneOfInternal)   },{"401", typeof(BasicErrorResponseInternal)   },{"404", typeof(BasicErrorResponseInternal)   },{"422", typeof(BasicErrorResponseInternal)   },{"429", typeof(BasicErrorResponseInternal)   },{"500", typeof(BasicErrorResponseInternal)   },{"503", typeof(BasicErrorResponseInternal)   } };
            
            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("POST",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(_accessToken, finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<RunScriptResponseInternal>(response, statusCodeToTypeMap);
            return new Response<RunScriptResponseInternal>(response, handledResponse);
        }

    }
}
