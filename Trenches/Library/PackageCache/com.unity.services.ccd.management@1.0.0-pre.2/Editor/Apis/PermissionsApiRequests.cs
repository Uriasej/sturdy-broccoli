using System.Collections.Generic;
using UnityEngine.Scripting;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Unity.Services.CCD.Management.Models;


namespace Unity.Services.CCD.Management.Permissions
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
    public class PermissionsApiBaseRequest
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
    /// Create permission by bucket request
    /// </summary>
    [Preserve]
    public class CreatePermissionByBucketRequest : PermissionsApiBaseRequest
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
        /// Create permission object
        /// </summary>
        [Preserve]
        public CcdPermissionCreate CcdPermissionCreate { get; }
        string PathAndQueryParams;

        /// <summary>
        /// CreatePermissionByBucket Request Object.
        /// Create a permission
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="ccdPermissionCreate">Permission</param>
        [Preserve]
        public CreatePermissionByBucketRequest(string bucketid, string projectid, CcdPermissionCreate ccdPermissionCreate)
        {
            Bucketid = bucketid;
            Projectid = projectid;
            CcdPermissionCreate = ccdPermissionCreate;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/permissions";

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
            return ConstructBody(CcdPermissionCreate);
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
    /// Delete permission by bucket request
    /// </summary>
    [Preserve]
    public class DeletePermissionByBucketRequest : PermissionsApiBaseRequest
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
        /// Permission
        /// </summary>
        [Preserve]
        public string Permission { get; }
        /// <summary>
        /// Action
        /// </summary>
        [Preserve]
        public string Action { get; }
        string PathAndQueryParams;

        /// <summary>
        /// DeletePermissionByBucket Request Object.
        /// delete a permission
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="permission">Permission of resource: allow, deny. </param>
        /// <param name="action">Permission action: write, read. </param>
        [Preserve]
        public DeletePermissionByBucketRequest(string bucketid, string projectid, string permission = default(string), string action = default(string))
        {
            Bucketid = bucketid;
            Projectid = projectid;
            Permission = permission;
            Action = action;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/permissions";

            List<string> queryParams = new List<string>();

            if (Permission != null)
            {
                var permissionStringValue = Permission.ToString();
                queryParams = AddParamsToQueryParams(queryParams, "permission", permissionStringValue);
            }

            if (Action != null)
            {
                var actionStringValue = Action.ToString();
                queryParams = AddParamsToQueryParams(queryParams, "action", actionStringValue);
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
    /// Get all by bucket request
    /// </summary>
    [Preserve]
    public class GetAllByBucketRequest : PermissionsApiBaseRequest
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
        /// GetAllByBucket Request Object.
        /// Get permissions for bucket
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        [Preserve]
        public GetAllByBucketRequest(string bucketid, string projectid)
        {
            Bucketid = bucketid;
            Projectid = projectid;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/permissions";

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
    /// Update permission by bucket request
    /// </summary>
    [Preserve]
    public class UpdatePermissionByBucketRequest : PermissionsApiBaseRequest
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
        /// Update permission object
        /// </summary>
        [Preserve]
        public CcdPermissionUpdate CcdPermissionUpdate { get; }
        string PathAndQueryParams;

        /// <summary>
        /// UpdatePermissionByBucket Request Object.
        /// Update a permission
        /// </summary>
        /// <param name="bucketid">Bucket ID</param>
        /// <param name="projectid">Project ID</param>
        /// <param name="ccdPermissionUpdate">Permission</param>
        [Preserve]
        public UpdatePermissionByBucketRequest(string bucketid, string projectid, CcdPermissionUpdate ccdPermissionUpdate)
        {
            Bucketid = bucketid;
            Projectid = projectid;
            CcdPermissionUpdate = ccdPermissionUpdate;
            PathAndQueryParams = $"/api/ccd/management/v1/projects/{projectid}/buckets/{bucketid}/permissions";

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
            return ConstructBody(CcdPermissionUpdate);
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
