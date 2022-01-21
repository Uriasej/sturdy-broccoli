using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.CCD.Management.Models;
using Unity.Services.CCD.Management.Http;
using TaskScheduler = Unity.Services.CCD.Management.Scheduler.TaskScheduler;
using Unity.Services.CCD.Management.Permissions;

namespace Unity.Services.CCD.Management.Apis.Permissions
{
    /// <summary>
    /// Interface for API endpoints
    /// </summary>
    public interface IPermissionsApiClient
    {
        /// <summary>
        /// Async Operation.
        /// Create a permission
        /// </summary>
        /// <param name="request">Request object for CreatePermissionByBucket</param>
        /// <param name="operationConfiguration">Configuration for CreatePermissionByBucket</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdPermission object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdPermission>> CreatePermissionByBucketAsync(CreatePermissionByBucketRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// delete a permission
        /// </summary>
        /// <param name="request">Request object for DeletePermissionByBucket</param>
        /// <param name="operationConfiguration">Configuration for DeletePermissionByBucket</param>
        /// <returns>Task for a Response object containing status code, headers</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response> DeletePermissionByBucketAsync(DeletePermissionByBucketRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get permissions for bucket
        /// </summary>
        /// <param name="request">Request object for GetAllByBucket</param>
        /// <param name="operationConfiguration">Configuration for GetAllByBucket</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdPermission&gt; object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<List<CcdPermission>>> GetAllByBucketAsync(GetAllByBucketRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Update a permission
        /// </summary>
        /// <param name="request">Request object for UpdatePermissionByBucket</param>
        /// <param name="operationConfiguration">Configuration for UpdatePermissionByBucket</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdPermission object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdPermission>> UpdatePermissionByBucketAsync(UpdatePermissionByBucketRequest request, Configuration operationConfiguration = null);

    }

    ///<inheritdoc cref="IPermissionsApiClient"/>
    public class PermissionsApiClient : BaseApiClient, IPermissionsApiClient
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
        public PermissionsApiClient(IHttpClient httpClient,
            Configuration configuration = null) : base(httpClient)
        {
            // We don't need to worry about the configuration being null at
            // this stage, we will check this in the accessor.
            _configuration = configuration;


        }

        /// <summary>
        /// Create permission
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdPermission>> CreatePermissionByBucketAsync(CreatePermissionByBucketRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdPermission) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("POST",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdPermission>(response, statusCodeToTypeMap);
            return new Response<CcdPermission>(response, handledResponse);
        }

        /// <summary>
        /// Delete permission
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response> DeletePermissionByBucketAsync(DeletePermissionByBucketRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", null }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("DELETE",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            ResponseHandler.HandleAsyncResponse(response, statusCodeToTypeMap);
            return new Response(response);
        }

        /// <summary>
        /// Get permissions for bucket
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<List<CcdPermission>>> GetAllByBucketAsync(GetAllByBucketRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdPermission) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<List<CcdPermission>>(response, statusCodeToTypeMap);
            return new Response<List<CcdPermission>>(response, handledResponse);
        }

        /// <summary>
        /// Update permission
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdPermission>> UpdatePermissionByBucketAsync(UpdatePermissionByBucketRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdPermission) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("PUT",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdPermission>(response, statusCodeToTypeMap);
            return new Response<CcdPermission>(response, handledResponse);
        }

    }
}
