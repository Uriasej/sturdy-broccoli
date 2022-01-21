using System;
using UnityEngine;

namespace Unity.Services.Analytics.Platform
{
    [Obsolete]
    public class DeviceIdentifiers
    {
        [Obsolete]
        public static string Idfv { get; private set; }

        [Obsolete]
        public static string Idfa { get; private set; }

        [Obsolete]
        public static void SetupIdentifiers()
        {
            Idfv = SystemInfo.deviceUniqueIdentifier;
            Application.RequestAdvertisingIdentifierAsync((id, enabled, error) =>
            {
                Idfa = id;
            });
        }
    }

    class DeviceIdentifiersInternal
    {
        internal static string Idfv { get; private set; }

        internal static string Idfa { get; private set; }

        internal static void SetupIdentifiers()
        {
            Idfv = SystemInfo.deviceUniqueIdentifier;
            Application.RequestAdvertisingIdentifierAsync((id, enabled, error) =>
            {
                Idfa = id;
            });
        }
    }
}
