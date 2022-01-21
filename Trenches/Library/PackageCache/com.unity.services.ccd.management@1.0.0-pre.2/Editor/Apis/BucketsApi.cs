using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.CCD.Management.Models;
using Unity.Services.CCD.Management.Http;
using TaskScheduler = Unity.Services.CCD.Management.Scheduler.TaskScheduler;
using Unity.Services.CCD.Management.Buckets;
using System;

namespace Unity.Services.CCD.Management.Apis.Buckets
{
    /// <summary>
    /// Interface for API endpoints
    /// </summary>
    public interface IBucketsApiClient
    {
        /// <summary>
        /// Async Operation.
        /// Create bucket
        /// </summary>
        /// <param name="request">Request object for CreateBucketByProject</param>
        /// <param name="operationConfiguration">Configuration for CreateBucketByProject</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdBucket object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdBucket>> CreateBucketByProjectAsync(CreateBucketByProjectRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Delete a bucket
        /// </summary>
        /// <param name="request">Request object for DeleteBucket</param>
        /// <param name="operationConfiguration">Configuration for DeleteBucket</param>
        /// <returns>Task for a Response object containing status code, headers</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response> DeleteBucketAsync(DeleteBucketRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get a bucket
        /// </summary>
        /// <param name="request">Request object for GetBucket</param>
        /// <param name="operationConfiguration">Configuration for GetBucket</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdBucket object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdBucket>> GetBucketAsync(GetBucketRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get counts of changes since last release
        /// </summary>
        /// <param name="request">Request object for GetDiff</param>
        /// <param name="operationConfiguration">Configuration for GetDiff</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdChangecount object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdChangecount>> GetDiffAsync(GetDiffRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get changed entries since last releases
        /// </summary>
        /// <param name="request">Request object for GetDiffEntries</param>
        /// <param name="operationConfiguration">Configuration for GetDiffEntries</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdReleaseentry&gt; object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<List<CcdReleaseentry>>> GetDiffEntriesAsync(GetDiffEntriesRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get buckets for project
        /// </summary>
        /// <param name="request">Request object for ListBucketsByProject</param>
        /// <param name="operationConfiguration">Configuration for ListBucketsByProject</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdBucket&gt; object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<List<CcdBucket>>> ListBucketsByProjectAsync(ListBucketsByProjectRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Promote release between buckets
        /// </summary>
        /// <param name="request">Request object for PromoteBucket</param>
        /// <param name="operationConfiguration">Configuration for PromoteBucket</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdRelease object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdRelease>> PromoteBucketAsync(PromoteBucketRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Update a bucket
        /// </summary>
        /// <param name="request">Request object for UpdateBucket</param>
        /// <param name="operationConfiguration">Configuration for UpdateBucket</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdBucket object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdBucket>> UpdateBucketAsync(UpdateBucketRequest request, Configuration operationConfiguration = null);

    }

    ///<inheritdoc cref="IBucketsApiClient"/>
    public class BucketsApiClient : BaseApiClient, IBucketsApiClient
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
        public BucketsApiClient(IHttpClient httpClient,
            Configuration configuration = null) : base(httpClient)
        {
            // We don't need to worry about the configuration being null at
            // this stage, we will check this in the accessor.
            _configuration = configuration;


        }

        /// <summary>
        /// Creates a bucket
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdBucket>> CreateBucketByProjectAsync(CreateBucketByProjectRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdBucket) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("POST",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdBucket>(response, statusCodeToTypeMap);
            return new Response<CcdBucket>(response, handledResponse);
        }

        /// <summary>
        /// Delete a bucket
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response> DeleteBucketAsync(DeleteBucketRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "204", null }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

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
        /// Get a bucket
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdBucket>> GetBucketAsync(GetBucketRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdBucket) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdBucket>(response, statusCodeToTypeMap);
            return new Response<CcdBucket>(response, handledResponse);
        }

        /// <summary>
        /// Get the diff of a bucket
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdChangecount>> GetDiffAsync(GetDiffRequest request,
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
        /// Get the entries of a specific diff
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<List<CcdReleaseentry>>> GetDiffEntriesAsync(GetDiffEntriesRequest request,
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
        /// List buckets
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<List<CcdBucket>>> ListBucketsByProjectAsync(ListBucketsByProjectRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdBucket) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<List<CcdBucket>>(response, statusCodeToTypeMap);
            return new Response<List<CcdBucket>>(response, handledResponse);
        }

        /// <summary>
        /// Promotes one release to a bucket
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdRelease>> PromoteBucketAsync(PromoteBucketRequest request,
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
        /// Updates a bucket
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdBucket>> UpdateBucketAsync(UpdateBucketRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdBucket) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("PUT",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdBucket>(response, statusCodeToTypeMap);
            return new Response<CcdBucket>(response, handledResponse);
        }

    }
}
