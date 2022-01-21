using System.Collections.Generic;
using UnityEngine.Scripting;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Unity.Services.CCD.Management.Models;
using System;

namespace Unity.Services.CCD.Management.Buckets
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
    public class BucketsApiBaseRequest
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
    /// Create buckets by project request
    /// </summary>
    [Preserve]
    public class CreateBucketByProjectRequest : BucketsApiBaseRequest
    {
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        /// <summary>
        /// Create bucket object
        /// </summary>
        [Preserve]
        public CcdBucketCreate CcdBucketCreate { get; }
        string PathAndQueryParams;

        /// <summary>
        /// CreateBucketByProject Request Object.
        /// Create bucket
        /// </summary>
        /// <param name="projectid">Project ID</param>
        /// <param name="ccdBucketCreate">Bucket</param>
        [Preserve]
        public CreateBucketByProjectRequest(string projectid, CcdBucketCreate ccdBucketCreate)
        {
            Projectid = projectid;
            CcdBucketCreate = ccdBucketCreate;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets";

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
            return ConstructBody(CcdBucketCreate);
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
    /// Delete bucket request
    /// </summary>
    [Preserve]
    public class DeleteBucketRequest : BucketsApiBaseRequest
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
        string PathAndQueryParams;

        /// <summary>
        /// DeleteBucket Request Object.
        /// Delete a bucket
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        [Preserve]
        public DeleteBucketRequest(string bucketid, string projectid)
        {
            Bucketid = bucketid;
            Projectid = projectid;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}";

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
    /// Get bucket request
    /// </summary>
    [Preserve]
    public class GetBucketRequest : BucketsApiBaseRequest
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
        string PathAndQueryParams;

        /// <summary>
        /// GetBucket Request Object.
        /// Get a bucket
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        [Preserve]
        public GetBucketRequest(string bucketid, string projectid)
        {
            Bucketid = bucketid;
            Projectid = projectid;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}";

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
    /// Get diff request
    /// </summary>
    [Preserve]
    public class GetDiffRequest : BucketsApiBaseRequest
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
        string PathAndQueryParams;

        /// <summary>
        /// GetDiff Request Object.
        /// Get counts of changes since last release
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        [Preserve]
        public GetDiffRequest(string bucketid, string projectid)
        {
            Bucketid = bucketid;
            Projectid = projectid;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/diff/unreleased";

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
    /// Get diff entries request
    /// </summary>
    [Preserve]
    public class GetDiffEntriesRequest : BucketsApiBaseRequest
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
        /// Page
        /// </summary>
        [Preserve]
        public int Page { get; }
        /// <summary>
        /// Per page
        /// </summary>
        [Preserve]
        public int PerPage { get; }
        /// <summary>
        /// Path
        /// </summary>
        [Preserve]
        public string Path { get; }
        /// <summary>
        /// Include states
        /// </summary>
        [Preserve]
        public List<string> IncludeStates { get; }
        string PathAndQueryParams;

        /// <summary>
        /// GetDiffEntries Request Object.
        /// Get changed entries since last releases
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="page">Current Page</param>
        /// <param name="perPage">Items Per Page</param>
        /// <param name="path">Path</param>
        /// <param name="includeStates">Include change states, one or more can be specified. The default is all states.</param>
        [Preserve]
        public GetDiffEntriesRequest(string bucketid, string projectid, int page = default(int), int perPage = 10, string path = default(string), List<string> includeStates = default(List<string>))
        {
            Bucketid = bucketid;
            Projectid = projectid;
            Page = page;
            PerPage = perPage;
            Path = path;
            IncludeStates = includeStates;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/diff/unreleased/entries";

            List<string> queryParams = new List<string>();

            var pageStringValue = Page.ToString();
            queryParams = AddParamsToQueryParams(queryParams, "page", pageStringValue);

            var perPageStringValue = PerPage.ToString();
            queryParams = AddParamsToQueryParams(queryParams, "per_page", perPageStringValue);

            if (Path != null)
            {
                var pathStringValue = Path.ToString();
                queryParams = AddParamsToQueryParams(queryParams, "path", pathStringValue);
            }

            if (IncludeStates != null)
            {
                var includeStatesStringValue = String.Join(",", IncludeStates);
                queryParams = AddParamsToQueryParams(queryParams, "include_states", includeStatesStringValue);
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
    /// List buckets by project request
    /// </summary>
    [Preserve]
    public class ListBucketsByProjectRequest : BucketsApiBaseRequest
    {
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
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
        /// ListBucketsByProject Request Object.
        /// Get buckets for project
        /// </summary>
        /// <param name="projectid">Project ID</param>
        /// <param name="page">Current Page</param>
        /// <param name="perPage">Items Per Page</param>
        [Preserve]
        public ListBucketsByProjectRequest(string projectid, int page = default(int), int perPage = 10)
        {
            Projectid = projectid;
            Page = page;
            PerPage = perPage;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets";

            List<string> queryParams = new List<string>();

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
    /// Promote bucket request
    /// </summary>
    [Preserve]
    public class PromoteBucketRequest : BucketsApiBaseRequest
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
        /// Promote bucket object
        /// </summary>
        [Preserve]
        public CcdPromotebucket CcdPromotebucket { get; }
        string PathAndQueryParams;

        /// <summary>
        /// PromoteBucket Request Object.
        /// Promote release between buckets
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="ccdPromotebucket">PromoteBucket</param>
        [Preserve]
        public PromoteBucketRequest(string bucketid, string projectid, CcdPromotebucket ccdPromotebucket)
        {
            Bucketid = bucketid;
            Projectid = projectid;
            CcdPromotebucket = ccdPromotebucket;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/promote";

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
            return ConstructBody(CcdPromotebucket);
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
    /// Update bucket request
    /// </summary>
    [Preserve]
    public class UpdateBucketRequest : BucketsApiBaseRequest
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
        /// Update bucket object
        /// </summary>
        [Preserve]
        public CcdBucketUpdate CcdBucketUpdate { get; }
        string PathAndQueryParams;

        /// <summary>
        /// UpdateBucket Request Object.
        /// Update a bucket
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="ccdBucketUpdate">Bucket</param>
        [Preserve]
        public UpdateBucketRequest(string bucketid, string projectid, CcdBucketUpdate ccdBucketUpdate)
        {
            Bucketid = bucketid;
            Projectid = projectid;
            CcdBucketUpdate = ccdBucketUpdate;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}";

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
            return ConstructBody(CcdBucketUpdate);
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
