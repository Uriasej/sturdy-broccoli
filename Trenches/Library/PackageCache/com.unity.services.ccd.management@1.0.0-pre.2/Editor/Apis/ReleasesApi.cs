using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.CCD.Management.Models;
using Unity.Services.CCD.Management.Http;
using TaskScheduler = Unity.Services.CCD.Management.Scheduler.TaskScheduler;
using Unity.Services.CCD.Management.Releases;

namespace Unity.Services.CCD.Management.Apis.Releases
{
    /// <summary>
    /// Interface for API endpoints
    /// </summary>
    public interface IReleasesApiClient
    {
        /// <summary>
        /// Async Operation.
        /// Create release
        /// </summary>
        /// <param name="request">Request object for CreateRelease</param>
        /// <param name="operationConfiguration">Configuration for CreateRelease</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdRelease object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdRelease>> CreateReleaseAsync(CreateReleaseRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get release
        /// </summary>
        /// <param name="request">Request object for GetRelease</param>
        /// <param name="operationConfiguration">Configuration for GetRelease</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdRelease object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdRelease>> GetReleaseAsync(GetReleaseRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get release by badge
        /// </summary>
        /// <param name="request">Request object for GetReleaseByBadge</param>
        /// <param name="operationConfiguration">Configuration for GetReleaseByBadge</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdRelease object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdRelease>> GetReleaseByBadgeAsync(GetReleaseByBadgeRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get counts of changes between releases
        /// </summary>
        /// <param name="request">Request object for GetReleaseDiff</param>
        /// <param name="operationConfiguration">Configuration for GetReleaseDiff</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdChangecount object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdChangecount>> GetReleaseDiffAsync(GetReleaseDiffRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get changed entries between releases
        /// </summary>
        /// <param name="request">Request object for GetReleaseDiffEntries</param>
        /// <param name="operationConfiguration">Configuration for GetReleaseDiffEntries</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdReleaseentry&gt; object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<List<CcdReleaseentry>>> GetReleaseDiffEntriesAsync(GetReleaseDiffEntriesRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get release entries
        /// </summary>
        /// <param name="request">Request object for GetReleaseEntries</param>
        /// <param name="operationConfiguration">Configuration for GetReleaseEntries</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdReleaseentry&gt; object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<List<CcdReleaseentry>>> GetReleaseEntriesAsync(GetReleaseEntriesRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get badged release entries
        /// </summary>
        /// <param name="request">Request object for GetReleaseEntriesByBadge</param>
        /// <param name="operationConfiguration">Configuration for GetReleaseEntriesByBadge</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdReleaseentry&gt; object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<List<CcdReleaseentry>>> GetReleaseEntriesByBadgeAsync(GetReleaseEntriesByBadgeRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get releases for bucket
        /// </summary>
        /// <param name="request">Request object for GetReleases</param>
        /// <param name="operationConfiguration">Configuration for GetReleases</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdRelease&gt; object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<List<CcdRelease>>> GetReleasesAsync(GetReleasesRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Update release
        /// </summary>
        /// <param name="request">Request object for UpdateRelease</param>
        /// <param name="operationConfiguration">Configuration for UpdateRelease</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdRelease object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdRelease>> UpdateReleaseAsync(UpdateReleaseRequest request, Configuration operationConfiguration = null);

    }

    ///<inheritdoc cref="IReleasesApiClient"/>
    public class ReleasesApiClient : BaseApiClient, IReleasesApiClient
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
        public ReleasesApiClient(IHttpClient httpClient,
            Configuration configuration = null) : base(httpClient)
        {
            // We don't need to worry about the configuration being null at
            // this stage, we will check this in the accessor.
            _configuration = configuration;
        }

        /// <summary>
        /// Create release
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdRelease>> CreateReleaseAsync(CreateReleaseRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdRelease) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("POST",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdRelease>(response, statusCodeToTypeMap);
            return new Response<CcdRelease>(response, handledResponse);
        }

        /// <summary>
        /// Get release
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdRelease>> GetReleaseAsync(GetReleaseRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdRelease) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdRelease>(response, statusCodeToTypeMap);
            return new Response<CcdRelease>(response, handledResponse);
        }

        /// <summary>
        /// Get release by badge
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdRelease>> GetReleaseByBadgeAsync(GetReleaseByBadgeRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdRelease) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdRelease>(response, statusCodeToTypeMap);
            return new Response<CcdRelease>(response, handledResponse);
        }

        /// <summary>
        /// Get release diff
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdChangecount>> GetReleaseDiffAsync(GetReleaseDiffRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdChangecount) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdChangecount>(response, statusCodeToTypeMap);
            return new Response<CcdChangecount>(response, handledResponse);
        }

        /// <summary>
        /// Get release diff entries
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<List<CcdReleaseentry>>> GetReleaseDiffEntriesAsync(GetReleaseDiffEntriesRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdReleaseentry) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<List<CcdReleaseentry>>(response, statusCodeToTypeMap);
            return new Response<List<CcdReleaseentry>>(response, handledResponse);
        }

        /// <summary>
        /// Get release entries
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<List<CcdReleaseentry>>> GetReleaseEntriesAsync(GetReleaseEntriesRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdReleaseentry) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<List<CcdReleaseentry>>(response, statusCodeToTypeMap);
            return new Response<List<CcdReleaseentry>>(response, handledResponse);
        }

        /// <summary>
        /// Get release entries by badge
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<List<CcdReleaseentry>>> GetReleaseEntriesByBadgeAsync(GetReleaseEntriesByBadgeRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdReleaseentry) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<List<CcdReleaseentry>>(response, statusCodeToTypeMap);
            return new Response<List<CcdReleaseentry>>(response, handledResponse);
        }

        /// <summary>
        /// Get releases
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<List<CcdRelease>>> GetReleasesAsync(GetReleasesRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdRelease) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<List<CcdRelease>>(response, statusCodeToTypeMap);
            return new Response<List<CcdRelease>>(response, handledResponse);
        }

        /// <summary>
        /// Update release
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdRelease>> UpdateReleaseAsync(UpdateReleaseRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdRelease) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("PUT",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdRelease>(response, statusCodeToTypeMap);
            return new Response<CcdRelease>(response, handledResponse);
        }

    }
}
