using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace Unity.Services.Analytics.DataPrivacy
{
    [Obsolete("Developers should implement their own consent button by presenting Events.PrivacyUrl to users," +
              "and calling Events.OptOut() should they refuse consent")]
    public class DataPrivacyButton : Button
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void OpenNewWindow(string url);
#endif
        bool urlOpened = false;

        DataPrivacyButton()
        {
            onClick.AddListener(OpenDataPrivacyUrl);
        }

        void OnFailure(string reason)
        {
            interactable = true;
            Debug.LogWarning($"Failed to get data privacy url: {reason}");
        }

        void OpenUrl(string url)
        {
            interactable = true;
            urlOpened = true;

#if UNITY_WEBGL && !UNITY_EDITOR
            OpenNewWindow(url);
#else
            Application.OpenURL(url);
#endif
        }

        async void OpenDataPrivacyUrl()
        {
            interactable = false;

            try
            {
                var url = await DataPrivacy.FetchPrivacyUrlAsync();
                OpenUrl(url);
            }
            catch (Exception e)
            {
                OnFailure(e.Message);
            }
        }

        void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus && urlOpened)
            {
                urlOpened = false;
                // Immediately refresh the remote config so new privacy settings can be enabled
                // as soon as possible if they have changed.
                RemoteSettings.ForceUpdate();
            }
        }
    }
}
