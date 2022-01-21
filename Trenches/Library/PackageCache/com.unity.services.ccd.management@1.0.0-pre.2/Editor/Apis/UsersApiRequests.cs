using System.Collections.Generic;
using UnityEngine.Scripting;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Unity.Services.CCD.Management.Models;


namespace Unity.Services.CCD.Management.Users
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
    public class UsersApiBaseRequest
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
    /// Get User Api Request
    /// </summary>
    [Preserve]
    public class GetUserApiKeyRequest : UsersApiBaseRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        [Preserve]
        public string Userid { get; }
        string PathAndQueryParams;

        /// <summary>
        /// GetUserApiKey Request Object.
        /// Get user API key
        /// </summary>
        /// <param name="userid">User ID</param>
        [Preserve]
        public GetUserApiKeyRequest(string userid)
        {
            Userid = userid;
            PathAndQueryParams = $"/api/ccd/management/v1/users/{userid}/apikey";

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
    /// Get user info request
    /// </summary>
    [Preserve]
    public class GetUserInfoRequest : UsersApiBaseRequest
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Preserve]
        public string Userid { get; }
        string PathAndQueryParams;

        /// <summary>
        /// GetUserInfo Request Object.
        /// Get user info
        /// </summary>
        /// <param name="userid">User ID</param>
        [Preserve]
        public GetUserInfoRequest(string userid)
        {
            Userid = userid;
            PathAndQueryParams = $"/api/ccd/management/v1/users/{userid}";

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
    /// Regenerate user api key request
    /// </summary>
    [Preserve]
    public class RegenerateUserApiKeyRequest : UsersApiBaseRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        [Preserve]
        public string Userid { get; }
        /// <summary>
        /// Ccd user api key
        /// </summary>
        [Preserve]
        public CcdUserapikey CcdUserapikey { get; }
        string PathAndQueryParams;

        /// <summary>
        /// RegenerateUserApiKey Request Object.
        /// Re-generate user API key
        /// </summary>
        /// <param name="userid">User ID</param>
        /// <param name="ccdUserapikey">ApiKey</param>
        [Preserve]
        public RegenerateUserApiKeyRequest(string userid, CcdUserapikey ccdUserapikey)
        {
            Userid = userid;
            CcdUserapikey = ccdUserapikey;
            PathAndQueryParams = $"/api/ccd/management/v1/users/{userid}/apikey";

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
            return ConstructBody(CcdUserapikey);
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
