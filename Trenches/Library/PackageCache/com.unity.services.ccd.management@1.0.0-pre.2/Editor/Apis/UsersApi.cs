using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.CCD.Management.Models;
using Unity.Services.CCD.Management.Http;
using TaskScheduler = Unity.Services.CCD.Management.Scheduler.TaskScheduler;
using Unity.Services.CCD.Management.Users;

namespace Unity.Services.CCD.Management.Apis.Users
{
    /// <summary>
    /// Interface for API endpoints
    /// </summary>
    public interface IUsersApiClient
    {
        /// <summary>
        /// Async Operation.
        /// Get user API key
        /// </summary>
        /// <param name="request">Request object for GetUserApiKey</param>
        /// <param name="operationConfiguration">Configuration for GetUserApiKey</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdUserapikey object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdUserapikey>> GetUserApiKeyAsync(GetUserApiKeyRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get user info
        /// </summary>
        /// <param name="request">Request object for GetUserInfo</param>
        /// <param name="operationConfiguration">Configuration for GetUserInfo</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdUser object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdUser>> GetUserInfoAsync(GetUserInfoRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Re-generate user API key
        /// </summary>
        /// <param name="request">Request object for RegenerateUserApiKey</param>
        /// <param name="operationConfiguration">Configuration for RegenerateUserApiKey</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdUserapikey object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdUserapikey>> RegenerateUserApiKeyAsync(RegenerateUserApiKeyRequest request, Configuration operationConfiguration = null);

    }

    ///<inheritdoc cref="IUsersApiClient"/>
    public class UsersApiClient : BaseApiClient, IUsersApiClient
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
        public UsersApiClient(IHttpClient httpClient,
            Configuration configuration = null) : base(httpClient)
        {
            // We don't need to worry about the configuration being null at
            // this stage, we will check this in the accessor.
            _configuration = configuration;
        }

        /// <summary>
        /// Get user api key
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdUserapikey>> GetUserApiKeyAsync(GetUserApiKeyRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdUserapikey) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdUserapikey>(response, statusCodeToTypeMap);
            return new Response<CcdUserapikey>(response, handledResponse);
        }

        /// <summary>
        /// Get user info
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdUser>> GetUserInfoAsync(GetUserInfoRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdUser) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdUser>(response, statusCodeToTypeMap);
            return new Response<CcdUser>(response, handledResponse);
        }

        /// <summary>
        /// Regenerate user api key
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdUserapikey>> RegenerateUserApiKeyAsync(RegenerateUserApiKeyRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdUserapikey) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("POST",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdUserapikey>(response, statusCodeToTypeMap);
            return new Response<CcdUserapikey>(response, handledResponse);
        }

    }
}
