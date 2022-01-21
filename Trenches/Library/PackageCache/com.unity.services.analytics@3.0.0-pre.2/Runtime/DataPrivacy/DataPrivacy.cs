using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Unity.Services.Analytics.DataPrivacy
{
    [Obsolete("Developers should implement their own consent button by presenting Events.PrivacyUrl to users," +
              "and calling Events.OptOut() should they refuse consent")]
    public class DataPrivacy
    {
        [Serializable]
        internal struct UserPostData
        {
            public string appid;
            public string userid;
            // Removed it since it is not being used and might cause issues since the type changed from long to string
            // public string sessionid;
            public string platform;
            public UInt32 platformid;
            public string sdk_ver;
            public bool debug_device;
            public string deviceid;
            public string plugin_ver;
        }

        [Serializable]
        internal struct TokenData
        {
            public string url;
            public string token;
        }

        const string kVersion = "3.1.0";
        const string kVersionString = "DataPrivacyPackage/" + kVersion;

        internal const string kBaseUrl = "https://data-optout-service.uca.cloud.unity3d.com";
        const string kTokenUrl = kBaseUrl + "/token";

        internal static UserPostData GetUserData()
        {
            var postData = new UserPostData
            {
                appid = Application.cloudProjectId,
                userid = Events.InstallId.GetOrCreateIdentifier(), // Changed from AnalyticsSessionInfo.userId
                // sessionid = Events.GetSessionId(), // Changed from AnalyticsSessionInfo.sessionId
                platform = Application.platform.ToString(),
                platformid = (UInt32)Application.platform,
                sdk_ver = Application.unityVersion,
                debug_device = Debug.isDebugBuild,
                deviceid = SystemInfo.deviceUniqueIdentifier,
                plugin_ver = kVersionString
            };
            return postData;
        }

        static string GetUserAgent()
        {
            var message = "UnityPlayer/{0} ({1}/{2}{3} {4})";
            return String.Format(message,
                Application.unityVersion,
                Application.platform.ToString(),
                (UInt32)Application.platform,
                Debug.isDebugBuild ? "-dev" : "",
                kVersionString);
        }

        static String getErrorString(UnityWebRequest www)
        {
            var json = www.downloadHandler.text;
            var error = www.error;
            if (String.IsNullOrEmpty(error))
            {
                // 5.5 sometimes fails to parse an error response, and the only clue will be
                // in www.responseHeadersString, which isn't accessible.
                error = "Empty response";
            }

            if (!String.IsNullOrEmpty(json))
            {
                error += ": " + json;
            }

            return error;
        }

        [Obsolete("The privacy policy page URL can be found using Events.PrivacyUrl")]
        public static async Task<string> FetchPrivacyUrlAsync()
        {
            string postJson = JsonUtility.ToJson(GetUserData());
            byte[] bytes = Encoding.UTF8.GetBytes(postJson);
            var uploadHandler = new UploadHandlerRaw(bytes);
            uploadHandler.contentType = "application/json";

            var www = UnityWebRequest.Post(kTokenUrl, "");
            www.uploadHandler = uploadHandler;
#if !UNITY_WEBGL
            www.SetRequestHeader("User-Agent", GetUserAgent());
#endif
            var async = www.SendWebRequest();

            while (!async.isDone)
            {
                await Task.Delay(1);
            }

            if (async.webRequest.isHttpError || async.webRequest.isNetworkError)
            {
                throw new Unity.Services.Core.RequestFailedException((int)async.webRequest.responseCode, async.webRequest.error);
            }

            var tokenData = JsonUtility.FromJson<TokenData>(async.webRequest.downloadHandler.text);
            return tokenData.url;
        }
    }
}
