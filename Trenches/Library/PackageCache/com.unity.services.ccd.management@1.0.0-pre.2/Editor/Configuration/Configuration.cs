using System.Collections.Generic;

namespace Unity.Services.CCD.Management
{
    /// <summary>
    /// Represents a set of configuration settings
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Base path
        /// </summary>
        public string BasePath;
        /// <summary>
        /// Request Timeout
        /// </summary>
        public int? RequestTimeout;
        /// <summary>
        /// Number of retries
        /// </summary>
        public int? NumberOfRetries;
        /// <summary>
        /// Headers
        /// </summary>
        public IDictionary<string, string> Headers;

        #region authdata
        #endregion

        /// <summary>
        /// Creates a new configuration object for the CCD Management Service to use
        /// </summary>
        /// <param name="basePath">The base path of the service</param>
        /// <param name="requestTimeout">The request timeout</param>
        /// <param name="numRetries">The number of retries for a particular request</param>
        /// <param name="headers">The headers for each request</param>
        public Configuration(string basePath, int? requestTimeout, int? numRetries, IDictionary<string, string> headers)
        {
            BasePath = basePath;
            RequestTimeout = requestTimeout;
            NumberOfRetries = numRetries;
            Headers = headers;
        }

        // Helper function for merging two configurations. Configuration `a` is
        // considered the base configuration if it is a valid object. Certain
        // values will be overridden if they are set to null within this
        // configuration by configuration `b` and the headers will be merged.
        /// <summary>
        /// Merge Configurations
        /// </summary>
        /// <param name="a">config 1</param>
        /// <param name="b">config 2</param>
        /// <returns></returns>
        public static Configuration MergeConfigurations(Configuration a, Configuration b)
        {
            // Check if either inputs are `null`, if they are, we return
            // whichever is not `null`, if both are `null`, we return `b` which
            // will be `null`. 
            if (a == null || b == null)
            {
                return a ?? b;
            }

            Configuration mergedConfig = a;

            if (mergedConfig.BasePath == null)
            {
                mergedConfig.BasePath = b.BasePath;
            }

            var headers = new Dictionary<string, string>();

            if (b.Headers != null)
            {
                foreach (var pair in b.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            if (mergedConfig.Headers != null)
            {
                foreach (var pair in mergedConfig.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            mergedConfig.Headers = headers;
            mergedConfig.RequestTimeout = mergedConfig.RequestTimeout ?? b.RequestTimeout;
            mergedConfig.NumberOfRetries = mergedConfig.NumberOfRetries ?? b.NumberOfRetries;


            return mergedConfig;
        }
    }
}
