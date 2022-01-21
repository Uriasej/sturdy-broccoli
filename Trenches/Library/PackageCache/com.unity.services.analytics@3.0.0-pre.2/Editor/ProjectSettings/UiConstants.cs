using UnityEditor;

namespace Unity.Services.Analytics.Editor
{
    /// <summary>
    /// Helper to store all constants used in Ads service UI.
    /// </summary>
    /// <remarks>
    /// This helper has been created to avoid adding too much noise in UI classes.
    /// </remarks>
    static class UiConstants
    {
        public static class StyleSheetPaths
        {
            public const string Common = "Packages/com.unity.services.analytics/Editor/StyleSheets/Common.uss";
        }

        public static class UiTemplatePaths
        {
            public const string Troubleshooting = "Packages/com.unity.services.analytics/Editor/UXML/Troubleshooting.uxml";

            public const string GettingStarted = "Packages/com.unity.services.analytics/Editor/UXML/GettingStarted.uxml";
        }

        public static class UiElementClasses
        {
            public const string NoteTag = "note-tag";
        }

        public static class UiElementNames
        {
            public const string LearnMoreLink = "LearnMoreLink";

            public const string SupportLink = "SupportLink";
        }

        public static class LocalizedStrings
        {
            public static readonly string Title = L10n.Tr("Analytics");

            public static readonly string Description = L10n.Tr("Discover player insights");
        }

        public static class Formats
        {
            public const string TemplateNotFound = "No UI template found for Ads Service {0}.";

            public const string DependentService = "Note: Disabling Analytics also disables {0}.";
        }

        public static class Urls
        {
            public const string LearnMore = "http://unity3d.com/services/analytics";

            public const string Support = "https://analytics.cloud.unity3d.com/support";
        }

        public static readonly string[] Keywords =
        {
            L10n.Tr("analytics"),
            L10n.Tr("insights"),
            L10n.Tr("events"),
            L10n.Tr("monetization"),
            L10n.Tr("dashboard")
        };

        public static readonly string[] SupportedPlatforms =
        {
            "Android",
            "iOS",
            "Linux",
            "Mac",
            "PC",
            "WebGL",
            "Windows 8.1 Universal",
            "Windows 10 Universal"
        };
    }
}
