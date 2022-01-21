using Unity.Services.Core.Editor;
using UnityEditor;
using UnityEngine.UIElements;

namespace Unity.Services.Analytics.Editor
{
    class AnalyticsSettingsProvider : EditorGameServiceSettingsProvider
    {
        VisualElement m_Root;
        GettingStartedUi m_GettingStartedUi;
        TroubleShootingUi m_TroubleShootingUi;
        VisualElement m_SupportedPlatformsUi;

        public AnalyticsSettingsProvider(SettingsScope scopes)
            : base(GetSettingsPath(), scopes, UiConstants.Keywords) { }

        internal static string GetSettingsPath()
        {
            return GenerateProjectSettingsPath(new AnalyticsIdentifier().GetKey());
        }

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
#if ENABLE_EDITOR_GAME_SERVICES
            return new AnalyticsSettingsProvider(SettingsScope.Project);
#else
            return null;
#endif
        }

        AnalyticsService m_Service = (AnalyticsService)EditorGameServiceRegistry.Instance
            .GetEditorGameService<AnalyticsIdentifier>();
        protected override IEditorGameService EditorGameService => m_Service;

        protected override string Title => UiConstants.LocalizedStrings.Title;

        protected override string Description => UiConstants.LocalizedStrings.Description;

        protected override VisualElement GenerateServiceDetailUI()
        {
            m_Root = new VisualElement();
            m_GettingStartedUi = new GettingStartedUi();
            m_TroubleShootingUi = new TroubleShootingUi();
            m_SupportedPlatformsUi = PlatformSupportUiHelper.GeneratePlatformSupport(UiConstants.SupportedPlatforms);

            SetUpStyles();

            RefreshDetailUI();

            return m_Root;
        }

        void SetUpStyles()
        {
            m_Root.AddToClassList("analytics");

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(UiConstants.StyleSheetPaths.Common);
            if (!(styleSheet is null))
            {
                m_Root.styleSheets.Add(styleSheet);
            }
        }

        void RefreshDetailUI()
        {
            if (m_Root is null)
            {
                return;
            }

            m_Root.Clear();

            m_Root.Add(m_GettingStartedUi);
            m_Root.Add(m_TroubleShootingUi);
            m_Root.Add(m_SupportedPlatformsUi);

            TranslateAllLabelsIn(m_Root);
        }

        static void TranslateAllLabelsIn(VisualElement root)
        {
            root.Query<TextElement>()
                .ForEach(TranslateLabel);

            string TranslateLabel(TextElement label)
            {
                label.text = L10n.Tr(label.text);

                return label.text;
            }
        }
    }
}
