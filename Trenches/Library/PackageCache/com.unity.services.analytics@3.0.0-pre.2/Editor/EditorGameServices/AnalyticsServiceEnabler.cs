using System;
using System.Reflection;
using Unity.Services.Core.Editor;
using UnityEditor.Analytics;

namespace Unity.Services.Analytics.Editor
{
    class AnalyticsServiceEnabler : EditorGameServiceFlagEnabler
    {
        protected override string FlagName { get; } = "analytics";

        bool m_IsEnabled = GetProjectSettingWithReflection();
        PurchasingServiceEnabler m_PurchasingEnabler = new PurchasingServiceEnabler();

        public override bool IsEnabled()
        {
            return m_IsEnabled;
        }

        protected override void EnableLocalSettings()
        {
            SetProjectSettingWithReflection(true);
            AnalyticsSettings.enabled = true;
            m_IsEnabled = true;
        }

        protected override void DisableLocalSettings()
        {
            SetProjectSettingWithReflection(false);
            AnalyticsSettings.enabled = false;
            m_IsEnabled = false;
            
            // when Analytics is disabled, IAP must also be disabled
            m_PurchasingEnabler.Disable();
        }

        const string k_ProjectSettingName = "Analytics";

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