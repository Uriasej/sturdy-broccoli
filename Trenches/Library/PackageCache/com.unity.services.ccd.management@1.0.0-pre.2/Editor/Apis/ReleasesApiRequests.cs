using System.Collections.Generic;
using UnityEngine.Scripting;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Unity.Services.CCD.Management.Models;
using System;

namespace Unity.Services.CCD.Management.Releases
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
    public class ReleasesApiBaseRequest
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
    /// Create release request
    /// </summary>
    [Preserve]
    public class CreateReleaseRequest : ReleasesApiBaseRequest
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
        /// Create release object
        /// </summary>
        [Preserve]
        public CcdReleaseCreate CcdReleaseCreate { get; }
        string PathAndQueryParams;

        /// <summary>
        /// CreateRelease Request Object.
        /// Create release
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="ccdReleaseCreate">Release</param>
        [Preserve]
        public CreateReleaseRequest(string bucketid, string projectid, CcdReleaseCreate ccdReleaseCreate)
        {
            Bucketid = bucketid;
            Projectid = projectid;
            CcdReleaseCreate = ccdReleaseCreate;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/releases";

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
            return ConstructBody(CcdReleaseCreate);
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
    /// Get release request
    /// </summary>
    [Preserve]
    public class GetReleaseRequest : ReleasesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Release id
        /// </summary>
        [Preserve]
        public string Releaseid { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        string PathAndQueryParams;

        /// <summary>
        /// GetRelease Request Object.
        /// Get release
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="releaseid">Release ID</param>
        /// <param name="projectid">Project ID</param>
        [Preserve]
        public GetReleaseRequest(string bucketid, string releaseid, string projectid)
        {
            Bucketid = bucketid;
            Releaseid = releaseid;
            Projectid = projectid;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/releases/{releaseid}";

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
    /// Get realease by badge request
    /// </summary>
    [Preserve]
    public class GetReleaseByBadgeRequest : ReleasesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Badge name
        /// </summary>
        [Preserve]
        public string Badgename { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        string PathAndQueryParams;

        /// <summary>
        /// GetReleaseByBadge Request Object.
        /// Get release by badge
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="badgename">Badge Name</param>
        /// <param name="projectid">Project ID</param>
        [Preserve]
        public GetReleaseByBadgeRequest(string bucketid, string badgename, string projectid)
        {
            Bucketid = bucketid;
            Badgename = badgename;
            Projectid = projectid;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/release_by_badge/{badgename}";

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
    /// Get release diff request
    /// </summary>
    [Preserve]
    public class GetReleaseDiffRequest : ReleasesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// From release id
        /// </summary>
        [Preserve]
        public string Fromreleaseid { get; }
        /// <summary>
        /// From release num
        /// </summary>
        [Preserve]
        public int? Fromreleasenum { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        /// <summary>
        /// To release id
        /// </summary>
        [Preserve]
        public string Toreleaseid { get; }
        /// <summary>
        /// To release num
        /// </summary>
        [Preserve]
        public int? Toreleasenum { get; }
        string PathAndQueryParams;

        /// <summary>
        /// GetReleaseDiff Request Object.
        /// Get counts of changes between releases
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="fromreleaseid">From Release ID, specify 'latest' to use the most recent release. Either fromreleaseid or fromreleasenum can be specified, but not both. </param>
        /// <param name="fromreleasenum">From Release Number. To query against an empty bucket you may set fromreleasenum to zero. Either fromreleaseid or fromreleasenum can be specified, but not both. </param>
        /// <param name="projectid">Project ID</param>
        /// <param name="toreleaseid">To Release ID, when not specified the most recent state of the bucket will be used. Either toreleaseid or toreleasenum can be specified, but not both. </param>
        /// <param name="toreleasenum">To Release ID, when not specified the most recent state of the bucket will be used. Either toreleaseid or toreleasenum can be specified, but not both. </param>
        [Preserve]
        public GetReleaseDiffRequest(string bucketid, string projectid, string fromreleaseid, int? fromreleasenum = null, string toreleaseid = default(string), int? toreleasenum = null)
        {
            Bucketid = bucketid;
            Fromreleaseid = fromreleaseid;
            Fromreleasenum = fromreleasenum.HasValue ? fromreleasenum : null;
            Projectid = projectid;
            Toreleaseid = toreleaseid;
            Toreleasenum = toreleasenum.HasValue ? toreleasenum : null;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/diff/releases";

            List<string> queryParams = new List<string>();

            if (Fromreleaseid != null)
            {
                queryParams = AddParamsToQueryParams(queryParams, "fromreleaseid", Fromreleaseid);
            }

            if (Fromreleasenum.HasValue)
            {
                queryParams = AddParamsToQueryParams(queryParams, "fromreleasenum", Fromreleasenum);
            }

            if (Toreleaseid != null)
            {
                queryParams = AddParamsToQueryParams(queryParams, "toreleaseid", Toreleaseid);
            }

            if (Toreleasenum.HasValue)
            {
                queryParams = AddParamsToQueryParams(queryParams, "toreleasenum", Toreleasenum);
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
    /// Get release diff entries request
    /// </summary>
    [Preserve]
    public class GetReleaseDiffEntriesRequest : ReleasesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// From release id
        /// </summary>
        [Preserve]
        public string Fromreleaseid { get; }
        /// <summary>
        /// From release num
        /// </summary>
        [Preserve]
        public int? Fromreleasenum { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        /// <summary>
        /// To release id
        /// </summary>
        [Preserve]
        public string Toreleaseid { get; }
        /// <summary>
        /// To release num
        /// </summary>
        [Preserve]
        public int? Toreleasenum { get; }
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
        /// GetReleaseDiffEntries Request Object.
        /// Get changed entries between releases
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="fromreleaseid">From Release ID, specify 'latest' to use the most recent release. Either fromreleaseid or fromreleasenum can be specified, but not both. </param>
        /// <param name="fromreleasenum">From Release Number. To query against an empty bucket you may set fromreleasenum to zero. Either fromreleaseid or fromreleasenum can be specified, but not both. </param>
        /// <param name="projectid">Project ID</param>
        /// <param name="toreleaseid">To Release ID, when not specified the most recent state of the bucket will be used. Either toreleaseid or toreleasenum can be specified, but not both. </param>
        /// <param name="toreleasenum">To Release ID, when not specified the most recent state of the bucket will be used. Either toreleaseid or toreleasenum can be specified, but not both. </param>
        /// <param name="page">Current Page</param>
        /// <param name="perPage">Items Per Page</param>
        /// <param name="path">Path</param>
        /// <param name="includeStates">Include change states, one or more can be specified. The default is all states. [ Unchanged, Add, Delete, Update ]</param>
        [Preserve]
        public GetReleaseDiffEntriesRequest(string bucketid, string projectid, string fromreleaseid, int? fromreleasenum = null, string toreleaseid = default(string), int? toreleasenum = null, int page = default(int), int perPage = 10, string path = default(string), List<string> includeStates = default(List<string>))
        {
            Bucketid = bucketid;
            Fromreleaseid = fromreleaseid;
            Fromreleasenum = fromreleasenum.HasValue ? fromreleasenum : null;
            Projectid = projectid;
            Toreleaseid = toreleaseid;
            Toreleasenum = toreleasenum.HasValue ? toreleasenum : null;
            Page = page;
            PerPage = perPage;
            Path = path;
            IncludeStates = includeStates;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/diff/releases/entries";

            List<string> queryParams = new List<string>();

            if (Fromreleaseid != null)
            {
                queryParams = AddParamsToQueryParams(queryParams, "fromreleaseid", Fromreleaseid);
            }

            if (Fromreleasenum.HasValue)
            {
                queryParams = AddParamsToQueryParams(queryParams, "fromreleasenum", Fromreleasenum);
            }

            if (Toreleaseid != null)
            {
                queryParams = AddParamsToQueryParams(queryParams, "toreleaseid", Toreleaseid);
            }

            if (Toreleasenum.HasValue)
            {
                queryParams = AddParamsToQueryParams(queryParams, "toreleasenum", Toreleasenum);
            }

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
    /// Get release entries request
    /// </summary>
    [Preserve]
    public class GetReleaseEntriesRequest : ReleasesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Release id
        /// </summary>
        [Preserve]
        public string Releaseid { get; }
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
        /// GetReleaseEntries Request Object.
        /// Get release entries
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="releaseid">Release ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="label">Label</param>
        /// <param name="page">Current Page</param>
        /// <param name="perPage">Items Per Page</param>
        [Preserve]
        public GetReleaseEntriesRequest(string bucketid, string releaseid, string projectid, string label = default(string), int page = default(int), int perPage = 10)
        {
            Bucketid = bucketid;
            Releaseid = releaseid;
            Projectid = projectid;
            Label = label;
            Page = page;
            PerPage = perPage;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/releases/{releaseid}/entries";

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
    /// Get release entries by badge request
    /// </summary>
    [Preserve]
    public class GetReleaseEntriesByBadgeRequest : ReleasesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Badge name
        /// </summary>
        [Preserve]
        public string Badgename { get; }
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
        /// GetReleaseEntriesByBadge Request Object.
        /// Get badged release entries
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="badgename">Badge Name</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="label">Label</param>
        /// <param name="page">Current Page</param>
        /// <param name="perPage">Items Per Page</param>
        [Preserve]
        public GetReleaseEntriesByBadgeRequest(string bucketid, string badgename, string projectid, string label = default(string), int page = default(int), int perPage = 10)
        {
            Bucketid = bucketid;
            Badgename = badgename;
            Projectid = projectid;
            Label = label;
            Page = page;
            PerPage = perPage;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/release_by_badge/{badgename}/entries";

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
    /// Get release request
    /// </summary>
    [Preserve]
    public class GetReleasesRequest : ReleasesApiBaseRequest
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
        string PathAndQueryParams;

        /// <summary>
        /// GetReleases Request Object.
        /// Get releases for bucket
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="page">Current Page</param>
        /// <param name="perPage">Items Per Page</param>
        [Preserve]
        public GetReleasesRequest(string bucketid, string projectid, int page = default(int), int perPage = 10)
        {
            Bucketid = bucketid;
            Projectid = projectid;
            Page = page;
            PerPage = perPage;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/releases";

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
    /// Update release request
    /// </summary>
    [Preserve]
    public class UpdateReleaseRequest : ReleasesApiBaseRequest
    {
        /// <summary>
        /// Bucket id
        /// </summary>
        [Preserve]
        public string Bucketid { get; }
        /// <summary>
        /// Release id
        /// </summary>
        [Preserve]
        public string Releaseid { get; }
        /// <summary>
        /// Project id
        /// </summary>
        [Preserve]
        public string Projectid { get; }
        /// <summary>
        /// Ccd release update
        /// </summary>
        [Preserve]
        public CcdReleaseUpdate CcdReleaseUpdate { get; }
        string PathAndQueryParams;

        /// <summary>
        /// UpdateRelease Request Object.
        /// Update release
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="releaseid">Release ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="ccdReleaseUpdate">Release fields to update</param>
        [Preserve]
        public UpdateReleaseRequest(string bucketid, string releaseid, string projectid, CcdReleaseUpdate ccdReleaseUpdate)
        {
            Bucketid = bucketid;
            Releaseid = releaseid;
            Projectid = projectid;
            CcdReleaseUpdate = ccdReleaseUpdate;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/releases/{releaseid}";

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
            return ConstructBody(CcdReleaseUpdate);
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
