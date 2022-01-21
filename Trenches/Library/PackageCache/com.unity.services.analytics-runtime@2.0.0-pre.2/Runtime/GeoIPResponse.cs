using System;
using UnityEngine.Serialization;

namespace Unity.Services.Analytics.Internal
{
    [Serializable]
    public class GeoIPResponse
    {
        public string identifier;
        public string country;
        public string region;
        public int ageGateLimit;
    }
}
