using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.CCD.Management.Apis.Badges;
using Unity.Services.CCD.Management.Apis.Buckets;
using Unity.Services.CCD.Management.Apis.Content;
using Unity.Services.CCD.Management.Apis.Default;
using Unity.Services.CCD.Management.Apis.Entries;
using Unity.Services.CCD.Management.Apis.Orgs;
using Unity.Services.CCD.Management.Apis.Permissions;
using Unity.Services.CCD.Management.Apis.Releases;
using Unity.Services.CCD.Management.Apis.Users;

namespace Unity.Services.CCD.Management
{
    /// <summary>
    /// Static CCD Management Api Service
    /// </summary>
    public static class CCDManagementAPIService
    {
        /// <summary>
        /// Static accessor for BadgesApi methods.
        /// </summary>
        public static IBadgesApiClient BadgesApiClient { get; internal set; }
        /// <summary>
        /// Static accessor for BucketsApi methods.
        /// </summary>
        public static IBucketsApiClient BucketsApiClient { get; internal set; }
        /// <summary>
        /// Static accessor for ContentApi methods.
        /// </summary>
        public static IContentApiClient ContentApiClient { get; internal set; }
        /// <summary>
        /// Static accessor for DefaultApi methods.
        /// </summary>
        public static IDefaultApiClient DefaultApiClient { get; internal set; }
        /// <summary>
        /// Static accessor for EntriesApi methods.
        /// </summary>
        public static IEntriesApiClient EntriesApiClient { get; internal set; }
        /// <summary>
        /// Static accessor for OrgsApi methods.
        /// </summary>
        public static IOrgsApiClient OrgsApiClient { get; internal set; }
        /// <summary>
        /// Static accessor for PermissionsApi methods.
        /// </summary>
        public static IPermissionsApiClient PermissionsApiClient { get; internal set; }
        /// <summary>
        /// Static accessor for ReleasesApi methods.
        /// </summary>
        public static IReleasesApiClient ReleasesApiClient { get; internal set; }
        /// <summary>
        /// Static accessor for UsersApi methods.
        /// </summary>
        public static IUsersApiClient UsersApiClient { get; internal set; }

        /// <summary>
        /// Static configuration
        /// </summary>
        public static Configuration Configuration = new Configuration("https://services.unity.com", 10, 4, new Dictionary<string, string>());

        /// <summary>
        /// Utilized to Configure Services Gateway Authentication Header.
        /// </summary>
        /// <param name="cloudProjectSettingsAccessToken">CloudProjectSettings.accessToken</param>
        /// <returns></returns>
        public static async Task SetConfigurationAuthHeader(string cloudProjectSettingsAccessToken)
        {

            using (var client = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(new Token() { TokenValue = cloudProjectSettingsAccessToken });
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var url = $"{Configuration.BasePath}/api/auth/v1/genesis-token-exchange/unity/";
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new System.Exception(await response.Content.ReadAsStringAsync());
                }
                var token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync()).TokenValue;
                var tokenValue = $"Bearer {token}";

                if (Configuration.Headers.ContainsKey("Authorization"))
                {
                    Configuration.Headers["Authorization"] = tokenValue;
                }
                else
                {
                    Configuration.Headers.Add("Authorization", tokenValue);
                }
            }

        }

        private class Token
        {
            [JsonProperty("token")]
            public string TokenValue;
        }
    }
}
