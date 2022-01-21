using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.CCD.Management.Models;
using Unity.Services.CCD.Management.Http;
using TaskScheduler = Unity.Services.CCD.Management.Scheduler.TaskScheduler;
using Unity.Services.CCD.Management.Orgs;

namespace Unity.Services.CCD.Management.Apis.Orgs
{
    /// <summary>
    /// Interface for API endpoints
    /// </summary>
    public interface IOrgsApiClient
    {
        /// <summary>
        /// Async Operation.
        /// Gets organization details.
        /// </summary>
        /// <param name="request">Request object for GetOrg</param>
        /// <param name="operationConfiguration">Configuration for GetOrg</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdOrg&gt; object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdOrg>> GetOrgAsync(GetOrgRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Gets organization Usage Details.
        /// </summary>
        /// <param name="request">Request object for GetOrgUsage</param>
        /// <param name="operationConfiguration">Configuration for GetOrgUsage</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdOrgusage&gt; object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdOrgusage>> GetOrgUsageAsync(GetOrgUsageRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Update tos accepted on a organization
        /// </summary>
        /// <param name="request">Request object for SaveTosAccepted</param>
        /// <param name="operationConfiguration">Configuration for SaveTosAccepted</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdOrg object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdOrg>> SaveTosAcceptedAsync(SaveTosAcceptedRequest request, Configuration operationConfiguration = null);

    }

    ///<inheritdoc cref="IOrgsApiClient"/>
    public class OrgsApiClient : BaseApiClient, IOrgsApiClient
    {
        private const int _baseTimeout = 10;
        private Configuration _configuration;

        /// <summary>
        /// The configuration used for requests
        /// </summary>
        public Configuration Configuration
        {
            get
            {
                // We return a merge between the current configuration and the
                // global configuration to ensure we have the correct
                // combination of headers and a base path (if it is set).
                return Configuration.MergeConfigurations(_configuration, CCDManagementAPIService.Configuration);
            }
        }

        /// <summary>
        /// Creates a new instance of the client
        /// </summary>
        /// <param name="httpClient">The http client to use</param>
        /// <param name="configuration">The configuration to use (by default, it will use the static version)</param>
        public OrgsApiClient(IHttpClient httpClient,
            Configuration configuration = null) : base(httpClient)
        {
            // We don't need to worry about the configuration being null at
            // this stage, we will check this in the accessor.
            _configuration = configuration;


        }

        /// <summary>
        /// Get org
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdOrg>> GetOrgAsync(GetOrgRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdOrg) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdOrg>(response, statusCodeToTypeMap);
            return new Response<CcdOrg>(response, handledResponse);
        }

        /// <summary>
        /// Get org usage
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdOrgusage>> GetOrgUsageAsync(GetOrgUsageRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdOrgusage) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdOrgusage>(response, statusCodeToTypeMap);
            return new Response<CcdOrgusage>(response, handledResponse);
        }

        /// <summary>
        /// Update Terms of Service
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdOrg>> SaveTosAcceptedAsync(SaveTosAcceptedRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdOrg) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("PUT",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdOrg>(response, statusCodeToTypeMap);
            return new Response<CcdOrg>(response, handledResponse);
        }

    }
}
