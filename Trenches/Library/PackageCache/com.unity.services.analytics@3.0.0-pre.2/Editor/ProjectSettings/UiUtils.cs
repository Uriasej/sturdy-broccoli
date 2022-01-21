using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace Unity.Services.Analytics.Editor
{
    static class UiUtils
    {
        public static VisualElement GetUiFromTemplate(string templatePath)
        {
            var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(templatePath);
            if (template == null)
            {
                return null;
            }

            return template.CloneTree().contentContainer;
        }

        public static void AddOnClickedForElement(this VisualElement self, Action onClicked, string elementName)
        {
            var link = self.Q(elementName);
            switch (link)
            {
                case null:
                    return;

                case Button linkButton:
                {
                    linkButton.clicked += onClicked;
                    break;
                }

                default:
                {
                    var clickable = new Clickable(onClicked);
                    link.AddManipulator(clickable);
                    break;
                }
            }
        }
    }
}
