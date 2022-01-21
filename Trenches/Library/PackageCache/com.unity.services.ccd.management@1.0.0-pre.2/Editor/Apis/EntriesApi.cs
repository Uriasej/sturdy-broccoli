using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.CCD.Management.Models;
using Unity.Services.CCD.Management.Http;
using TaskScheduler = Unity.Services.CCD.Management.Scheduler.TaskScheduler;
using Unity.Services.CCD.Management.Entries;

namespace Unity.Services.CCD.Management.Apis.Entries
{
    /// <summary>
    /// Interface for API endpoints
    /// </summary>
    public interface IEntriesApiClient
    {
        /// <summary>
        /// Async Operation.
        /// Create entry
        /// </summary>
        /// <param name="request">Request object for CreateEntry</param>
        /// <param name="operationConfiguration">Configuration for CreateEntry</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdEntry>> CreateEntryAsync(CreateEntryRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Create or update entry by path
        /// </summary>
        /// <param name="request">Request object for CreateOrUpdateEntryByPath</param>
        /// <param name="operationConfiguration">Configuration for CreateOrUpdateEntryByPath</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdEntry>> CreateOrUpdateEntryByPathAsync(CreateOrUpdateEntryByPathRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Delete entry
        /// </summary>
        /// <param name="request">Request object for DeleteEntry</param>
        /// <param name="operationConfiguration">Configuration for DeleteEntry</param>
        /// <returns>Task for a Response object containing status code, headers</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response> DeleteEntryAsync(DeleteEntryRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get entries for bucket
        /// </summary>
        /// <param name="request">Request object for GetEntries</param>
        /// <param name="operationConfiguration">Configuration for GetEntries</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdEntry&gt; object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<List<CcdEntry>>> GetEntriesAsync(GetEntriesRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get entry
        /// </summary>
        /// <param name="request">Request object for GetEntry</param>
        /// <param name="operationConfiguration">Configuration for GetEntry</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdEntry>> GetEntryAsync(GetEntryRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get entry by path
        /// </summary>
        /// <param name="request">Request object for GetEntryByPath</param>
        /// <param name="operationConfiguration">Configuration for GetEntryByPath</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdEntry>> GetEntryByPathAsync(GetEntryByPathRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get entry version
        /// </summary>
        /// <param name="request">Request object for GetEntryVersion</param>
        /// <param name="operationConfiguration">Configuration for GetEntryVersion</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdEntry>> GetEntryVersionAsync(GetEntryVersionRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Get entry versions
        /// </summary>
        /// <param name="request">Request object for GetEntryVersions</param>
        /// <param name="operationConfiguration">Configuration for GetEntryVersions</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdVersion&gt; object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<List<CcdVersion>>> GetEntryVersionsAsync(GetEntryVersionsRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Update entry
        /// </summary>
        /// <param name="request">Request object for UpdateEntry</param>
        /// <param name="operationConfiguration">Configuration for UpdateEntry</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdEntry>> UpdateEntryAsync(UpdateEntryRequest request, Configuration operationConfiguration = null);

        /// <summary>
        /// Async Operation.
        /// Update entry by path
        /// </summary>
        /// <param name="request">Request object for UpdateEntryByPath</param>
        /// <param name="operationConfiguration">Configuration for UpdateEntryByPath</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object</returns>
        /// <exception cref="Unity.Services.CCD.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        Task<Response<CcdEntry>> UpdateEntryByPathAsync(UpdateEntryByPathRequest request, Configuration operationConfiguration = null);

    }

    ///<inheritdoc cref="IEntriesApiClient"/>
    public class EntriesApiClient : BaseApiClient, IEntriesApiClient
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
        public EntriesApiClient(IHttpClient httpClient,
            Configuration configuration = null) : base(httpClient)
        {
            // We don't need to worry about the configuration being null at
            // this stage, we will check this in the accessor.
            _configuration = configuration;


        }

        /// <summary>
        /// Create entry
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdEntry>> CreateEntryAsync(CreateEntryRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdEntry) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("POST",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }

        /// <summary>
        /// Create or update entry by path
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdEntry>> CreateOrUpdateEntryByPathAsync(CreateOrUpdateEntryByPathRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdEntry) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("POST",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }

        /// <summary>
        /// Delete entry
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response> DeleteEntryAsync(DeleteEntryRequest request,
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
        /// Get entries for bucket
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<List<CcdEntry>>> GetEntriesAsync(GetEntriesRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdEntry) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<List<CcdEntry>>(response, statusCodeToTypeMap);
            return new Response<List<CcdEntry>>(response, handledResponse);
        }

        /// <summary>
        /// Get entry
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdEntry>> GetEntryAsync(GetEntryRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdEntry) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }

        /// <summary>
        /// Get entry by path
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdEntry>> GetEntryByPathAsync(GetEntryByPathRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdEntry) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }

        /// <summary>
        /// Get entry version
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdEntry>> GetEntryVersionAsync(GetEntryVersionRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdEntry) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }

        /// <summary>
        /// Get entry versions
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<List<CcdVersion>>> GetEntryVersionsAsync(GetEntryVersionsRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdVersion) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<List<CcdVersion>>(response, statusCodeToTypeMap);
            return new Response<List<CcdVersion>>(response, handledResponse);
        }

        /// <summary>
        /// Update entry
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdEntry>> UpdateEntryAsync(UpdateEntryRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdEntry) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("PUT",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }

        /// <summary>
        /// Update entry by path
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public async Task<Response<CcdEntry>> UpdateEntryByPathAsync(UpdateEntryByPathRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { { "200", typeof(CcdEntry) }, { "400", typeof(ValidationError) }, { "401", typeof(AuthenticationError) }, { "403", typeof(AuthorizationError) }, { "404", typeof(NotFoundError) }, { "429", typeof(TooManyRequestsError) }, { "500", typeof(InternalServerError) }, { "503", typeof(ServiceUnavailableError) } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("PUT",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }

    }
}
