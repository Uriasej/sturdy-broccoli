using System;
using System.Reflection;
using Unity.Services.Core.Editor;
using UnityEditor.Purchasing;

namespace Unity.Services.Analytics.Editor
{
    /// <remarks>
    /// This script is required to properly set the settings for IAP when Analytics is disabled 
    /// </remarks>
    class PurchasingServiceEnabler : EditorGameServiceFlagEnabler
    {
        protected override string FlagName { get; } = "purchasing";

        bool m_IsEnabled = GetProjectSettingWithReflection();

        public override bool IsEnabled()
        {
            return m_IsEnabled;
        }

        protected override void EnableLocalSettings()
        {
            SetProjectSettingWithReflection(true);
            m_IsEnabled = true;
            PurchasingSettings.enabled = true;
        }

        protected override void DisableLocalSettings()
        {
            SetProjectSettingWithReflection(false);
            m_IsEnabled = false;
            PurchasingSettings.enabled = false;
        }

        const string k_ProjectSettingName = "Purchasing";

        static bool GetProjectSettingWithReflection()
        {
            var playerSettingsType = Type.GetType("UnityEditor.PlayerSettings,UnityEditor.dll");
            var isEnabled = false;
            if (playerSettingsType != null)
            {
                var getCloudServiceEnabledMethod = playerSettingsType.GetMethod("GetCloudServiceEnabled", BindingFlags.Static | BindingFlags.NonPublic);
                if (getCloudServiceEnabledMethod != null)
                {
                    var enabledStateResult = getCloudServiceEnabledMethod.Invoke(null, new object[] {k_ProjectSettingName});
                    isEnabled = Convert.ToBoolean(enabledStateResult);
                }
            }

            return isEnabled;
        }

        static void SetProjectSettingWithReflection(bool value)
        {
            var playerSettingsType = Type.GetType("UnityEditor.PlayerSettings,UnityEditor.dll");
            if (playerSettingsType != null)
            {
                var setCloudServiceEnabledMethod = playerSettingsType.GetMethod("SetCloudServiceEnabled", BindingFlags.Static | BindingFlags.NonPublic);
                if (setCloudServiceEnabledMethod != null)
                {
                    setCloudServiceEnabledMethod.Invoke(null, new object[] {k_ProjectSettingName, value});
                }
            }
        }
    }
} 