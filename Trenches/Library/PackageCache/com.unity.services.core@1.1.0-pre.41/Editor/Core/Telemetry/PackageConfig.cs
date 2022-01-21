using System;
using UnityEditor.PackageManager;

namespace Unity.Services.Core.Editor
{
    [Serializable]
    struct PackageConfig
    {
        public string Name;

        public string Version;

        public PackageConfig(PackageInfo packageInfo)
        {
            Name = packageInfo.name;
            Version = packageInfo.version;
        }
    }
}
