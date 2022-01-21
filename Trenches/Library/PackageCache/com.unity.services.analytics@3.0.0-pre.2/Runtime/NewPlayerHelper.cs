using System;
using Unity.Services.Core.Device.Internal;
using UnityEngine;

namespace Unity.Services.Analytics
{
    [Obsolete]
    public class NewPlayerHelper
    {
        internal IInstallationId InstallId { get; }
        const string k_UnityAnalyticsInstallationIdKey = "UnityAnalyticsInstallationId";

        [Obsolete]
        public NewPlayerHelper(IInstallationId installId)
        {
            if (installId == null)
            {
                throw new ArgumentNullException("Did not get IInstallationId provider from Unity Services Core.");
            }
            InstallId = installId;
        }

        [Obsolete]
        public bool IsNewPlayer()
        {
            var coreIdentifier = InstallId.GetOrCreateIdentifier();
            var analyticsIdentifier = ReadAnalyticsIdentifier();
            
            if (String.IsNullOrEmpty(analyticsIdentifier) || analyticsIdentifier != coreIdentifier)
            {
                WriteAnalyticsIdentifierToFile(coreIdentifier);
                return true;
            }

            return false;
        }

        internal string ReadAnalyticsIdentifier()
        {
            return PlayerPrefs.GetString(k_UnityAnalyticsInstallationIdKey);
        }

        internal void WriteAnalyticsIdentifierToFile(string identifier)
        {
            PlayerPrefs.SetString(k_UnityAnalyticsInstallationIdKey, identifier);
            PlayerPrefs.Save();
        }
    }

    class InternalNewPlayerHelper
    {
        internal IInstallationId InstallId { get; }
        const string k_UnityAnalyticsInstallationIdKey = "UnityAnalyticsInstallationId";

        internal InternalNewPlayerHelper(IInstallationId installId)
        {
            if (installId == null)
            {
                throw new ArgumentNullException("Did not get IInstallationId provider from Unity Services Core.");
            }
            InstallId = installId;
        }

        internal bool IsNewPlayer()
        {
            var coreIdentifier = InstallId.GetOrCreateIdentifier();
            var analyticsIdentifier = ReadAnalyticsIdentifier();
            
            if (String.IsNullOrEmpty(analyticsIdentifier) || analyticsIdentifier != coreIdentifier)
            {
                WriteAnalyticsIdentifierToFile(coreIdentifier);
                return true;
            }

            return false;
        }

        internal string ReadAnalyticsIdentifier()
        {
            return PlayerPrefs.GetString(k_UnityAnalyticsInstallationIdKey);
        }

        internal void WriteAnalyticsIdentifierToFile(string identifier)
        {
            PlayerPrefs.SetString(k_UnityAnalyticsInstallationIdKey, identifier);
            PlayerPrefs.Save();
        }
    }
}
