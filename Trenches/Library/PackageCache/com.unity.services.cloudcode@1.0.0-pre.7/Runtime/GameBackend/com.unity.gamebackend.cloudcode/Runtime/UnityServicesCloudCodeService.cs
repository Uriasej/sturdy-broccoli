using Unity.GameBackend.CloudCode.Apis.CloudCode;


namespace Unity.GameBackend.CloudCode
{
    internal static class UnityServicesCloudCodeService
    {
        public static IUnityServicesCloudCodeService Instance { get; internal set; }
    }

    internal interface IUnityServicesCloudCodeService
    {
        /// <summary>
        /// Accessor for CloudCodeApi methods.
        /// </summary>
        ICloudCodeApiClient CloudCodeApi { get; set; }
        
        Configuration Configuration { get; set; }
    }
}