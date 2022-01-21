using UnityEngine;
using UnityEngine.UIElements;

namespace Unity.Services.Analytics.Editor
{
    class GettingStartedUi : VisualElement
    {
        public GettingStartedUi()
        {
            var container = UiUtils.GetUiFromTemplate(UiConstants.UiTemplatePaths.GettingStarted);
            if (container is null)
            {
                var message = string.Format(
                    UiConstants.Formats.TemplateNotFound, nameof(UiConstants.UiTemplatePaths.GettingStarted));
                Debug.LogError(message);
                return;
            }

            Add(container);

            container.AddOnClickedForElement(OnLearnMoreClicked, UiConstants.UiElementNames.LearnMoreLink);
        }

        static void OnLearnMoreClicked()
        {
            Application.OpenURL(UiConstants.Urls.LearnMore);
        }
    }
}
