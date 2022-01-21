using System;
using Unity.Services.CCD.Management;
using Unity.Services.CCD.Management.Apis.Buckets;
using Unity.Services.CCD.Management.Buckets;
using Unity.Services.CCD.Management.Http;
using UnityEditor;
using UnityEngine;

namespace Unity.Services.CCD.Management.Examples
{
    class BucketsExample
    {
        async static void GetBucketsExample()
        {
            await CCDManagementAPIService.SetConfigurationAuthHeader(CloudProjectSettings.accessToken);
            var request = new ListBucketsByProjectRequest(CloudProjectSettings.projectId, 1, 10);
            var client = new BucketsApiClient(new HttpClient());
            var response = await client.ListBucketsByProjectAsync(request);
            Debug.unityLogger.LogFormat(LogType.Log, $"{response.Status} : {response.Result}");
        }
    }
}

