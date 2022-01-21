using UnityEngine;
using System.Threading.Tasks;

using Unity.GameBackend.CloudCode.Apis.CloudCode;

using Unity.GameBackend.CloudCode.Http;
using Unity.GameBackend.CloudCode.Scheduler;
using TaskScheduler = Unity.GameBackend.CloudCode.Scheduler.TaskSchedulerInternal;
using Unity.Services.Core.Internal;
using Unity.Services.Authentication.Internal;

namespace Unity.GameBackend.CloudCode
{
    internal class UnityServicesCloudCodeServiceProvider : IInitializablePackage
    {
        private static GameObject _gameObjectFactory;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        {
            // Pass an instance of this class to Core
            var generatedPackageRegistry =
            CoreRegistry.Instance.RegisterPackage(new UnityServicesCloudCodeServiceProvider());
                // And specify what components it requires, or provides.
            generatedPackageRegistry.DependsOn<IAccessToken>();
;
        }

        public Task Initialize(CoreRegistry registry)
        {
            var httpClient = new HttpClient();

            var accessTokenUnityServicesCloudCode = registry.GetServiceComponent<IAccessToken>();

            if (accessTokenUnityServicesCloudCode != null)
            {
                UnityServicesCloudCodeService.Instance =
                    new InternalUnityServicesCloudCodeService(httpClient, registry.GetServiceComponent<IAccessToken>());
            }

            return Task.CompletedTask;
        }
    }

    internal class InternalUnityServicesCloudCodeService : IUnityServicesCloudCodeService
    {
        public InternalUnityServicesCloudCodeService(HttpClient httpClient, IAccessToken accessToken = null)
        {
            
            CloudCodeApi = new CloudCodeApiClient(httpClient, accessToken);
            
            Configuration = new Configuration("https://cloud-code.services.api.unity.com", 10, 4, null);
        }
        
        public ICloudCodeApiClient CloudCodeApi { get; set; }
        
        public Configuration Configuration { get; set; }
    }
}
