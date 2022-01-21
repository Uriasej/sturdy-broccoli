using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.CCD.Management.Models;
using Unity.Services.CCD.Management.Http;
using TaskScheduler = Unity.Services.CCD.Management.Scheduler.TaskScheduler;
using Unity.Services.CCD.Management.Content;
using TusDotNetClient;
using System.Net;
using System.Linq;

namespace Unity.Services.CCD.Management.Apis.Content
{
    /// <summary>
    /// Interface for API endpoints
    /// </summary>
    public interface IContentApiClient
    {
        /// <summary>
        /// Async Operation.
        /// Create content upload for TUS
        /// </summary>
        /// <param name="request">Request object for CreateContent</param>
        /// <param name="operationConfiguration">Configuration for CreateContent</param>
        /// <returns>Task for a Response object containing status code, headers</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response> CreateContentAsync(CreateContentRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get content by entryid
        /// </summary>
        /// <param name="request">Request object for GetContent</param>
        /// <param name="operationConfiguration">Configuration for GetContent</param>
        /// <returns>Task for a Response object containing status code, headers, and System.IO.Stream object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<System.IO.Stream>> GetContentAsync(GetContentRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get content status by entryid
        /// </summary>
        /// <param name="request">Request object for GetContentStatus</param>
        /// <param name="operationConfiguration">Configuration for GetContentStatus</param>
        /// <returns>Task for a Response object containing status code, headers</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response> GetContentStatusAsync(GetContentStatusRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get content status for version of entry
        /// </summary>
        /// <param name="request">Request object for GetContentStatusVersion</param>
        /// <param name="operationConfiguration">Configuration for GetContentStatusVersion</param>
        /// <returns>Task for a Response object containing status code, headers</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response> GetContentStatusVersionAsync(GetContentStatusVersionRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get content for version of entry
        /// </summary>
        /// <param name="request">Request object for GetContentVersion</param>
        /// <param name="operationConfiguration">Configuration for GetContentVersion</param>
        /// <returns>Task for a Response object containing status code, headers, and System.IO.Stream object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<System.IO.Stream>> GetContentVersionAsync(GetContentVersionRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Upload content for entry
        /// </summary>
        /// <param name="request">Request object for UploadContent</param>
        /// <param name="onProgressed">Progress Delegate to execute upon upload progress</param>
        /// <param name="operationConfiguration">Configuration for UploadContent</param>
        /// <returns>Task for a Response object containing status code, headers</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response> UploadContentAsync(UploadContentRequest request, ProgressDelegate onProgressed = null, Configuration operationConfiguration = null);

    }

    ///<inheritdoc cref="IContentApiClient"/>
    public class ContentApiClient : BaseApiClient, IContentApiClient
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
        public ContentApiClient(IHttpClient httpClient,
            Configuration configuration = null) : base(httpClient)
        {
            // We don't need to worry about the configuration being null at
            // this stage, we will check this in the accessor.
            _configuration = configuration;


        }

        /// <summary>
        /// Creates a content upload for TUS
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response> CreateContentAsync(CreateContentRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "201", null }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("POST",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            ResponseHandler.HandleAsyncResponse(response, statusCodeToTypeMap);
            return new Response(response);
        }

        /// <summary>
        /// Get content by entryid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<System.IO.Stream>> GetContentAsync(GetContentRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(System.IO.Stream) }, { "206", typeof(System.IO.Stream) }, { "307", null }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<System.IO.Stream>(response, statusCodeToTypeMap);
            return new Response<System.IO.Stream>(response, handledResponse);
        }

        /// <summary>
        /// Get content status by entryid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response> GetContentStatusAsync(GetContentStatusRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", null }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("HEAD",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            ResponseHandler.HandleAsyncResponse(response, statusCodeToTypeMap);
            return new Response(response);
        }

        /// <summary>
        /// Get content status for version of entry
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response> GetContentStatusVersionAsync(GetContentStatusVersionRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", null }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("HEAD",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            ResponseHandler.HandleAsyncResponse(response, statusCodeToTypeMap);
            return new Response(response);
        }

        /// <summary>
        /// Get content for version of entry
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<System.IO.Stream>> GetContentVersionAsync(GetContentVersionRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(System.IO.Stream) }, { "206", typeof(System.IO.Stream) }, { "307", null }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<System.IO.Stream>(response, statusCodeToTypeMap);
            return new Response<System.IO.Stream>(response, handledResponse);
        }

        /// <summary>
        /// Upload content for entry
        /// </summary>
        /// <param name="request"></param>
        /// <param name="onProgressed"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response> UploadContentAsync(UploadContentRequest request, ProgressDelegate onProgressed = null,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "204", null }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var client = new TusClient();
            finalConfiguration.Headers.TryGetValue("Authorization", out string auth);
            client.AdditionalHeaders.Add("Authorization", auth);
            var url = request.ConstructUrl(finalConfiguration.BasePath);
            var uploadOperation = client.UploadAsync(url, request.File, request.ChunkSize);
            uploadOperation.Progressed += onProgressed;
            var uploadResponse = await uploadOperation;
            var clientReponse = TusClient.MapTusHttpResponsesToHttpResponse(uploadResponse);
            ResponseHandler.HandleAsyncResponse(clientReponse, statusCodeToTypeMap);

            return new Response(clientReponse);
        }

    }
}
