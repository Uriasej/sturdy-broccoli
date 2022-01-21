using System.Collections.Generic;
using UnityEngine.Scripting;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Unity.Services.CCD.Management.Models;


namespace Unity.Services.CCD.Management.Entries
{
    internal static class JsonSerialization
    {
        public static byte[] Serialize<T>(T obj)
        {
            return Encoding.UTF8.GetBytes(SerializeToString(obj));
        }

        public static string SerializeToString<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }

    /// <summary>
    /// Base api request 
    /// </summary>
    [Preserve]
    public class EntriesApiBaseRequest
    {
        /// <summary>
        /// Add query parameter to list of query params
        /// </summary>
        /// <param name="queryParams">List of current query params</param>
        /// <param name="key">Key of query param</param>
        /// <param name="value">Value of query param</param>
        /// <returns>modified list of query params</returns>
        [Preserve]
        public List<string> AddParamsToQueryParams(List<string> queryParams, string key, string value)
        {
            key = UnityWebRequest.EscapeURL(key);
            value = UnityWebRequest.EscapeURL(value);
            queryParams.Add($"{key}={value}");

            return queryParams;
        }

        /// <summary>
        /// Add a list param to query params
        /// </summary>
        /// <param name="queryParams">List of current query params</param>
        /// <param name="key">Key of query param</param>
        /// <param name="values">List of values to store</param>
        /// <param name="style"></param>
        /// <param name="explode">break into multiple query params</param>
        /// <returns>Modified list of query params</returns>
        [Preserve]
        public List<string> AddParamsToQueryParams(List<string> queryParams, string key, List<string> values, string style, bool explode)
        {
            if (explode)
            {
                foreach (var value in values)
                {
                    string escapedValue = UnityWebRequest.EscapeURL(value);
                    queryParams.Add($"{UnityWebRequest.EscapeURL(key)}={escapedValue}");
                }
            }
            else
            {
                string paramString = $"{UnityWebRequest.EscapeURL(key)}=";
                foreach (var value in values)
                {
                    paramString += UnityWebRequest.EscapeURL(value) + ",";
                }
                paramString = paramString.Remove(paramString.Length - 1);
                queryParams.Add(paramString);
            }

            return queryParams;
        }

        /// <summary>
        /// Add a list param to query params
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryParams"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Preserve]
        public List<string> AddParamsToQueryParams<T>(List<string> queryParams, string key, T value)
        {
            if (queryParams == null)
            {
                queryParams = new List<string>();
            }

            key = UnityWebRequest.EscapeURL(key);
            string valueString = UnityWebRequest.EscapeURL(value.ToString());
            queryParams.Add($"{key}={valueString}");
            return queryParams;
        }

        /// <summary>
        /// Constructs the body of the request
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public byte[] ConstructBody(System.IO.Stream stream)
        {
            if (stream != null)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            return null;
        }

        /// <summary>
        /// Constructs the body of the request
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public byte[] ConstructBody(string s)
        {
            return System.Text.Encoding.UTF8.GetBytes(s);
        }

        /// <summary>
        /// Constructs the body of the request
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public byte[] ConstructBody(object o)
        {
            return JsonSerialization.Serialize(o);
        }

        /// <summary>
        /// Generates accept header for request
        /// </summary>
        /// <param name="accepts"></param>
        /// <returns></returns>
        public string GenerateAcceptHeader(string[] accepts)
        {
            if (accepts.Length == 0)
            {
                return null;
            }
            for (int i = 0; i < accepts.Length; ++i)
            {
                if (string.Equals(accepts[i], "application/json", System.StringComparison.OrdinalIgnoreCase))
                {
                    return "application/json";
                }
            }
            return string.Join(", ", accepts);
        }

        private static readonly Regex JsonRegex = new Regex(@"application\/json(;\s)?((charset=utf8|q=[0-1]\.\d)(\s)?)*");

        /// <summary>
        /// Generates Content Type header for request
        /// </summary>
        /// <param name="contentTypes"></param>
        /// <returns></returns>
        public string GenerateContentTypeHeader(string[] contentTypes)
        {
            if (contentTypes.Length == 0)
            {
                return null;
            }

            for (int i = 0; i < contentTypes.Length; ++i)
            {
                if (!string.IsNullOrWhiteSpace(contentTypes[i]) && JsonRegex.IsMatch(contentTypes[i]))
                {
                    return contentTypes[i];
                }
            }
            return contentTypes[0];
        }

        /// <summary>
        /// Generate multipart form file section
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public IMultipartFormSection GenerateMultipartFormFileSection(string paramName, System.IO.Stream stream, string contentType)
        {
            if (stream is System.IO.FileStream)
            {
                System.IO.FileStream fileStream = (System.IO.FileStream)stream;
                return new MultipartFormFileSection(paramName, ConstructBody(fileStream), GetFileName(fileStream.Name), contentType);
            }
            return new MultipartFormDataSection(paramName, ConstructBody(stream));
        }

        /// <summary>
        /// Get file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string GetFileName(string filePath)
        {
            return System.IO.Path.GetFileName(filePath);
        }
    }

    /// <summary>
    /// Create entry request
    /// </summary>
    [Preserve]
    public class CreateEntryRequest : EntriesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        /// <summary>
        /// Create entry object
        /// </summary>
        [Preserve]
        public CcdEntryCreate CcdEntryCreate { get; }
        string PathAndQueryParams;

        /// <summary>
        /// CreateEntry Request Object.
        /// Create entry
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="ccdEntryCreate">Entry</param>
        [Preserve]
        public CreateEntryRequest(string bucketid, string projectid, CcdEntryCreate ccdEntryCreate)
        {
            Bucketid = bucketid;
            Projectid = projectid;
            CcdEntryCreate = ccdEntryCreate;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries";

            List<string> queryParams = new List<string>();

            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Construct body
        /// </summary>
        /// <returns></returns>
        public byte[] ConstructBody()
        {
            return ConstructBody(CcdEntryCreate);
        }

        /// <summary>
        /// Construct headers
        /// </summary>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public Dictionary<string, string> ConstructHeaders(Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();

            string[] contentTypes = {
                "application/json"
            };

            string[] accepts = {
                "application/json",
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }


            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }

    /// <summary>
    /// Create or update entry by path request
    /// </summary>
    [Preserve]
    public class CreateOrUpdateEntryByPathRequest : EntriesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Path
        /// </summary>
        [Preserve]
        public string Path { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        /// <summary>
        /// Create entry object 
        /// </summary>
        [Preserve]
        public CcdEntryCreateByPath CcdEntryCreateByPath { get; }
        /// <summary>
        /// Update if exists
        /// </summary>
        [Preserve]
        public bool UpdateIfExists { get; }
        string PathAndQueryParams;

        /// <summary>
        /// CreateOrUpdateEntryByPath Request Object.
        /// Create or update entry by path
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="path">Path</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="ccdEntryCreateByPath">Entry</param>
        /// <param name="updateIfExists">Set to 'true' if you want to update the existing entries</param>
        [Preserve]
        public CreateOrUpdateEntryByPathRequest(string bucketid, string path, string projectid, CcdEntryCreateByPath ccdEntryCreateByPath, bool updateIfExists = false)
        {
            Bucketid = bucketid;
            Path = path;
            Projectid = projectid;
            CcdEntryCreateByPath = ccdEntryCreateByPath;
            UpdateIfExists = updateIfExists;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entry_by_path";

            List<string> queryParams = new List<string>();

            var pathStringValue = Path.ToString();
            queryParams = AddParamsToQueryParams(queryParams, "path", pathStringValue);

            var updateIfExistsStringValue = UpdateIfExists.ToString();
            queryParams = AddParamsToQueryParams(queryParams, "updateIfExists", updateIfExistsStringValue);

            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Construct body
        /// </summary>
        /// <returns></returns>
        public byte[] ConstructBody()
        {
            return ConstructBody(CcdEntryCreateByPath);
        }

        /// <summary>
        /// Construct headers
        /// </summary>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public Dictionary<string, string> ConstructHeaders(Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();

            string[] contentTypes = {
                "application/json"
            };

            string[] accepts = {
                "application/json",
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }


            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }

    /// <summary>
    /// Delete entry request
    /// </summary>
    [Preserve]
    public class DeleteEntryRequest : EntriesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Entry id
        /// </summary>
        [Preserve]
        public string Entryid { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        string PathAndQueryParams;

        /// <summary>
        /// DeleteEntry Request Object.
        /// Delete entry
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="entryid">Entry ID</param>
        /// <param name="projectid">Project ID</param>
        [Preserve]
        public DeleteEntryRequest(string bucketid, string entryid, string projectid)
        {
            Bucketid = bucketid;
            Entryid = entryid;
            Projectid = projectid;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}";

            List<string> queryParams = new List<string>();

            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Construct body
        /// </summary>
        /// <returns></returns>
        public byte[] ConstructBody()
        {
            return null;
        }

        /// <summary>
        /// Construct headers
        /// </summary>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public Dictionary<string, string> ConstructHeaders(Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();

            string[] contentTypes = {
            };

            string[] accepts = {
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }


            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }

    /// <summary>
    /// Get entries request
    /// </summary>
    [Preserve]
    public class GetEntriesRequest : EntriesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        /// <summary>
        /// Path
        /// </summary>
        [Preserve]
        public string Path { get; }
        /// <summary>
        /// Label
        /// </summary>
        [Preserve]
        public string Label { get; }
        /// <summary>
        /// Page
        /// </summary>
        [Preserve]
        public int Page { get; }
        /// <summary>
        /// Per page
        /// </summary>
        [Preserve]
        public int PerPage { get; }
        string PathAndQueryParams;

        /// <summary>
        /// GetEntries Request Object.
        /// Get entries for bucket
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="path">Path</param>
        /// <param name="label">Label</param>
        /// <param name="page">Current Page</param>
        /// <param name="perPage">Items Per Page</param>
        [Preserve]
        public GetEntriesRequest(string bucketid, string projectid, string path = default(string), string label = default(string), int page = default(int), int perPage = 10)
        {
            Bucketid = bucketid;
            Projectid = projectid;
            Path = path;
            Label = label;
            Page = page;
            PerPage = perPage;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries";

            List<string> queryParams = new List<string>();

            if (Path != null)
            {
                var pathStringValue = Path.ToString();
                queryParams = AddParamsToQueryParams(queryParams, "path", pathStringValue);
            }

            if (Label != null)
            {
                var labelStringValue = Label.ToString();
                queryParams = AddParamsToQueryParams(queryParams, "label", labelStringValue);
            }

            var pageStringValue = Page.ToString();
            queryParams = AddParamsToQueryParams(queryParams, "page", pageStringValue);

            var perPageStringValue = PerPage.ToString();
            queryParams = AddParamsToQueryParams(queryParams, "per_page", perPageStringValue);

            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Construct body
        /// </summary>
        /// <returns></returns>
        public byte[] ConstructBody()
        {
            return null;
        }

        /// <summary>
        /// Construct headers
        /// </summary>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public Dictionary<string, string> ConstructHeaders(Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();

            string[] contentTypes = {
            };

            string[] accepts = {
                "application/json",
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }


            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }

    /// <summary>
    /// Get entry request
    /// </summary>
    [Preserve]
    public class GetEntryRequest : EntriesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Entry id
        /// </summary>
        [Preserve]
        public string Entryid { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        string PathAndQueryParams;

        /// <summary>
        /// GetEntry Request Object.
        /// Get entry
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="entryid">Entry ID</param>
        /// <param name="projectid">Project ID</param>
        [Preserve]
        public GetEntryRequest(string bucketid, string entryid, string projectid)
        {
            Bucketid = bucketid;
            Entryid = entryid;
            Projectid = projectid;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}";

            List<string> queryParams = new List<string>();

            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Construct body
        /// </summary>
        /// <returns></returns>
        public byte[] ConstructBody()
        {
            return null;
        }

        /// <summary>
        /// Construct headers
        /// </summary>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public Dictionary<string, string> ConstructHeaders(Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();

            string[] contentTypes = {
            };

            string[] accepts = {
                "application/json",
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }


            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }

    /// <summary>
    /// Get entry by path request
    /// </summary>
    [Preserve]
    public class GetEntryByPathRequest : EntriesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Path
        /// </summary>
        [Preserve]
        public string Path { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        /// <summary>
        /// Version id
        /// </summary>
        [Preserve]
        public string Versionid { get; }
        string PathAndQueryParams;

        /// <summary>
        /// GetEntryByPath Request Object.
        /// Get entry by path
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="path">Path</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="versionid">Version ID</param>
        [Preserve]
        public GetEntryByPathRequest(string bucketid, string path, string projectid, string versionid = default(string))
        {
            Bucketid = bucketid;
            Path = path;
            Projectid = projectid;
            Versionid = versionid;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entry_by_path";

            List<string> queryParams = new List<string>();

            var pathStringValue = Path.ToString();
            queryParams = AddParamsToQueryParams(queryParams, "path", pathStringValue);

            if (Versionid != null)
            {
                var versionidStringValue = Versionid.ToString();
                queryParams = AddParamsToQueryParams(queryParams, "versionid", versionidStringValue);
            }

            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Construct body
        /// </summary>
        /// <returns></returns>
        public byte[] ConstructBody()
        {
            return null;
        }

        /// <summary>
        /// Construct headers
        /// </summary>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public Dictionary<string, string> ConstructHeaders(Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();

            string[] contentTypes = {
            };

            string[] accepts = {
                "application/json",
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }


            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }

    /// <summary>
    /// Get entry version request
    /// </summary>
    [Preserve]
    public class GetEntryVersionRequest : EntriesApiBaseRequest
    {
        /// <summary>
        /// Bucket id 
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Entry id
        /// </summary>
        [Preserve]
        public string Entryid { get; }
        /// <summary>
        /// Version id
        /// </summary>
        [Preserve]
        public string Versionid { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        string PathAndQueryParams;

        /// <summary>
        /// GetEntryVersion Request Object.
        /// Get entry version
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="entryid">Entry ID</param>
        /// <param name="versionid">Version ID</param>
        /// <param name="projectid">Project ID</param>
        [Preserve]
        public GetEntryVersionRequest(string bucketid, string entryid, string versionid, string projectid)
        {
            Bucketid = bucketid;
            Entryid = entryid;
            Versionid = versionid;
            Projectid = projectid;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}/versions/{versionid}";

            List<string> queryParams = new List<string>();

            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Construct body
        /// </summary>
        /// <returns></returns>
        public byte[] ConstructBody()
        {
            return null;
        }

        /// <summary>
        /// Construct headers
        /// </summary>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public Dictionary<string, string> ConstructHeaders(Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();

            string[] contentTypes = {
            };

            string[] accepts = {
                "application/json",
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }


            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }

    /// <summary>
    /// Get entry versions request
    /// </summary>
    [Preserve]
    public class GetEntryVersionsRequest : EntriesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Entry id 
        /// </summary>
        [Preserve]
        public string Entryid { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        /// <summary>
        /// Label
        /// </summary>
        [Preserve]
        public string Label { get; }
        /// <summary>
        /// Page
        /// </summary>
        [Preserve]
        public int Page { get; }
        /// <summary>
        /// Per page
        /// </summary>
        [Preserve]
        public int PerPage { get; }
        string PathAndQueryParams;

        /// <summary>
        /// GetEntryVersions Request Object.
        /// Get entry versions
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="entryid">Entry ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="label">Label</param>
        /// <param name="page">Current Page</param>
        /// <param name="perPage">Items Per Page</param>
        [Preserve]
        public GetEntryVersionsRequest(string bucketid, string entryid, string projectid, string label = default(string), int page = default(int), int perPage = 10)
        {
            Bucketid = bucketid;
            Entryid = entryid;
            Projectid = projectid;
            Label = label;
            Page = page;
            PerPage = perPage;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}/versions";

            List<string> queryParams = new List<string>();

            if (Label != null)
            {
                var labelStringValue = Label.ToString();
                queryParams = AddParamsToQueryParams(queryParams, "label", labelStringValue);
            }

            var pageStringValue = Page.ToString();
            queryParams = AddParamsToQueryParams(queryParams, "page", pageStringValue);

            var perPageStringValue = PerPage.ToString();
            queryParams = AddParamsToQueryParams(queryParams, "per_page", perPageStringValue);

            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Construct body
        /// </summary>
        /// <returns></returns>
        public byte[] ConstructBody()
        {
            return null;
        }

        /// <summary>
        /// Construct headers
        /// </summary>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public Dictionary<string, string> ConstructHeaders(Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();

            string[] contentTypes = {
            };

            string[] accepts = {
                "application/json",
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }


            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }

    /// <summary>
    /// Update entry request
    /// </summary>
    [Preserve]
    public class UpdateEntryRequest : EntriesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Entry id 
        /// </summary>
        [Preserve]
        public string Entryid { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        /// <summary>
        /// Update entry object
        /// </summary>
        [Preserve]
        public CcdEntryUpdate CcdEntryUpdate { get; }
        string PathAndQueryParams;

        /// <summary>
        /// UpdateEntry Request Object.
        /// Update entry
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="entryid">Entry ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="ccdEntryUpdate">Entry</param>
        [Preserve]
        public UpdateEntryRequest(string bucketid, string entryid, string projectid, CcdEntryUpdate ccdEntryUpdate)
        {
            Bucketid = bucketid;
            Entryid = entryid;
            Projectid = projectid;
            CcdEntryUpdate = ccdEntryUpdate;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entries/{entryid}";

            List<string> queryParams = new List<string>();

            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Construct body
        /// </summary>
        /// <returns></returns>
        public byte[] ConstructBody()
        {
            return ConstructBody(CcdEntryUpdate);
        }

        /// <summary>
        /// Construct headers
        /// </summary>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public Dictionary<string, string> ConstructHeaders(Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();

            string[] contentTypes = {
                "application/json"
            };

            string[] accepts = {
                "application/json",
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }


            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }

    /// <summary>
    /// Update entry by path request 
    /// </summary>
    [Preserve]
    public class UpdateEntryByPathRequest : EntriesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Path
        /// </summary>
        [Preserve]
        public string Path { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        /// <summary>
        /// Update entry object
        /// </summary>
        [Preserve]
        public CcdEntryUpdate CcdEntryUpdate { get; }
        string PathAndQueryParams;

        /// <summary>
        /// UpdateEntryByPath Request Object.
        /// Update entry by path
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="path">Path</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="ccdEntryUpdate">Entry</param>
        [Preserve]
        public UpdateEntryByPathRequest(string bucketid, string path, string projectid, CcdEntryUpdate ccdEntryUpdate)
        {
            Bucketid = bucketid;
            Path = path;
            Projectid = projectid;
            CcdEntryUpdate = ccdEntryUpdate;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/entry_by_path";

            List<string> queryParams = new List<string>();

            var pathStringValue = Path.ToString();
            queryParams = AddParamsToQueryParams(queryParams, "path", pathStringValue);

            if (queryParams.Count > 0)
            {
                PathAndQueryParams = $"{PathAndQueryParams}?{string.Join("&", queryParams)}";
            }
        }

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="requestBasePath"></param>
        /// <returns></returns>
        public string ConstructUrl(string requestBasePath)
        {
            return requestBasePath + PathAndQueryParams;
        }

        /// <summary>
        /// Construct body
        /// </summary>
        /// <returns></returns>
        public byte[] ConstructBody()
        {
            return ConstructBody(CcdEntryUpdate);
        }

        /// <summary>
        /// Construct headers
        /// </summary>
        /// <param name="operationConfiguration"></param>
        /// <returns></returns>
        public Dictionary<string, string> ConstructHeaders(Configuration operationConfiguration = null)
        {
            var headers = new Dictionary<string, string>();

            string[] contentTypes = {
                "application/json"
            };

            string[] accepts = {
                "application/json",
                "application/problem+json"
            };

            var acceptHeader = GenerateAcceptHeader(accepts);
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                headers.Add("Accept", acceptHeader);
            }
            var contentTypeHeader = GenerateContentTypeHeader(contentTypes);
            if (!string.IsNullOrEmpty(contentTypeHeader))
            {
                headers.Add("Content-Type", contentTypeHeader);
            }


            // We also check if there are headers that are defined as part of
            // the request configuration.
            if (operationConfiguration != null && operationConfiguration.Headers != null)
            {
                foreach (var pair in operationConfiguration.Headers)
                {
                    headers[pair.Key] = pair.Value;
                }
            }

            return headers;
        }
    }
}
