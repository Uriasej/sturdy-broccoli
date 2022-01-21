#if ENABLE_EDITOR_GAME_SERVICES
using UnityEditor;
#endif
using Unity.Services.Core.Editor;

namespace Unity.Services.Analytics.Editor
{
    class AnalyticsService : IEditorGameService
    {
        public string Name { get; } = "Analytics";

        public IEditorGameServiceIdentifier Identifier { get; } = new AnalyticsIdentifier();

        public bool RequiresCoppaCompliance => false;

        public bool HasDashboard => true;

        public string GetFormattedDashboardUrl()
        {
#if ENABLE_EDITOR_GAME_SERVICES
            return $"https://dashboard.unity3d.com/organizations/{CloudProjectSettings.organizationKey}/projects/{CloudProjectSettings.projectId}/analytics/events";
#else
            return string.Empty;
#endif
        }

        public IEditorGameServiceEnabler Enabler { get; } = new AnalyticsServiceEnabler();
    }
}
