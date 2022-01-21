using UnityEngine;
using UnityEngine.UIElements;

namespace Unity.Services.Analytics.Editor
{
    class TroubleShootingUi : VisualElement
    {
        public TroubleShootingUi()
        {
            var container = UiUtils.GetUiFromTemplate(UiConstants.UiTemplatePaths.Troubleshooting);
            if (container is null)
            {
                var message = string.Format(
                    UiConstants.Formats.TemplateNotFound, nameof(UiConstants.UiTemplatePaths.Troubleshooting));
                Debug.LogError(message);
                return;
            }

            Add(container);

            container.AddOnClickedForElement(OnContactSupportClicked, UiConstants.UiElementNames.SupportLink);
        }

        static void OnContactSupportClicked()
        {
            Application.OpenURL(UiConstants.Urls.Support);
        }
    }
}
