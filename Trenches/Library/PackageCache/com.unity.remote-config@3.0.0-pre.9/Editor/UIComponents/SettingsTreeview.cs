﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Unity.RemoteConfig.Editor.UIComponents
{
    /// <summary>
    /// Class that can be used to make a settings tree view
    /// </summary>
    public class SettingsTreeview
    {
        /// <summary>
        /// Event fired by this component whenever a setting has been changed. The first JObject represents the setting before the change was made.
        /// The second JObject represents the setting after the change has been made. If the old setting is null, the setting is new. If the new setting is null,
        /// but the old setting is not, this is removing a setting.
        /// </summary>
        public event Action<JObject, JObject> OnSettingChanged;
        GUIContent m_CreateSettingButtonContent;
        const float k_LineHeight = 22f;
        string keyColumnHeader = "Key";
        string valueColumnHeader = "Value";

        SettingsTreeView treeView;
        /// <summary>
        /// JArray of settings.
        /// </summary>
        public JArray settingsList
        {
            set
            {
                treeView.settingsList = value;
                treeView.Reload();
            }
        }

        /// <summary>
        /// JArray of "active settings" - those being used in rules.
        /// </summary>
        public JArray activeSettingsList
        {
            set
            {
                treeView.activeSettingsList = value;
                treeView.Reload();
            }
        }

        /// <summary>
        /// JArray of rules - used to determine if the setting can be modified or not.
        /// </summary>
        public JArray rulesList
        {
            set
            {
                treeView.rulesList = value;
                treeView.Reload();
            }
        }

        /// <summary>
        /// Sets the loading state of the tree view.
        /// </summary>
        public bool isLoading
        {
            set
            {
                treeView.isLoading = value;
            }
        }

        /// <summary>
        /// Sets whether or not the keys can be updated
        /// </summary>
        public bool enableEditingSettingsKeys
        {
            set
            {
                treeView.enableEditingSettingsKeys = value;
                treeView.Reload();
            }
        }

        /// <summary>
        /// Constructor for the component.
        /// </summary>
        /// <param name="addSettingButtonText">Text to populate in the footer button that adds a setting. Defaults to "Add Setting".</param>
        /// <param name="keyColumnHeader">Text to populate in the header for the key column. Defaults to "Key".</param>
        /// <param name="valueColumnHeader">Text to populate in the header for the value column. Defaults to "Value".</param>
        public SettingsTreeview(string addSettingButtonText = "Add Setting", string keyColumnHeader = "Key", string valueColumnHeader = "Value")
        {
            this.keyColumnHeader = keyColumnHeader;
            this.valueColumnHeader = valueColumnHeader;
            treeView = new SettingsTreeView(new TreeViewState(), new SettingsMultiColumnHeader(CreateSettingsMultiColumnHeaderState()), new JArray(), new JArray());
            treeView.UpdateSetting += OnUpdateSetting;
            m_CreateSettingButtonContent = new GUIContent(addSettingButtonText);
        }

        void Cleanup()
        {
            treeView.UpdateSetting -= OnUpdateSetting;
        }

        void OnUpdateSetting(JObject oldItem, JObject newItem)
        {
            OnSettingChanged?.Invoke(oldItem, newItem);
        }

        /// <summary>
        /// Renders the treeview and footer
        /// </summary>
        /// <param name="viewRect">The Rect inside which to draw the tree view and footer</param>
        public void OnGUI(Rect viewRect)
        {
            var treeviewRect = new Rect(viewRect.x, viewRect.y, viewRect.width, viewRect.height - k_LineHeight);
            var toolbarRect = new Rect(viewRect.x, viewRect.y + treeviewRect.height, viewRect.width, k_LineHeight);

            if (GUI.Button(new Rect(toolbarRect.x + 2.5f, toolbarRect.y, toolbarRect.width - 5f, toolbarRect.height),
                   m_CreateSettingButtonContent))
            {
                OnSettingChanged?.Invoke(null, new JObject());
            }
            treeView.OnGUI(treeviewRect);
        }

        MultiColumnHeaderState CreateSettingsMultiColumnHeaderState()
        {
            var columns = new[]
            {
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent(keyColumnHeader),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = 150,
                    minWidth = 60,
                    autoResize = true,
                    allowToggleVisibility = false
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Type"),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = 150,
                    minWidth = 60,
                    autoResize = true,
                    allowToggleVisibility = false
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent(valueColumnHeader),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = 150,
                    minWidth = 60,
                    autoResize = true,
                    allowToggleVisibility = false
                },
                new MultiColumnHeaderState.Column
                {
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = 30,
                    minWidth = 30,
                    maxWidth = 30,
                    autoResize = true,
                    allowToggleVisibility = false
                }
            };
            var state = new MultiColumnHeaderState(columns);
            return state;
        }
    }

    internal class SettingsTreeView : TreeView
    {
        public JArray settingsList;
        public JArray activeSettingsList;
        public JArray rulesList;

        public event Action<JObject, JObject> UpdateSetting;

        public bool isLoading = false;
        JsonSerializerSettings rawDateSettings = new JsonSerializerSettings { DateParseHandling = DateParseHandling.None };

        public bool enableEditingSettingsKeys;
        private SettingsJsonModal m_SettingsJsonModal;

        private struct RSTypeChangedStruct
        {
            public JObject rs;
            public string newType;
        }

        public SettingsTreeView(TreeViewState state, MultiColumnHeader multiColumnHeader, JArray settingsList,
            JArray rulesList, bool enableEditingSettingsKeys = true) : base(state, multiColumnHeader)
        {
            this.rowHeight = 18f;
            this.settingsList = settingsList;
            this.rulesList = rulesList;
            this.enableEditingSettingsKeys = enableEditingSettingsKeys;
            useScrollView = true;
            Reload();
        }

        private bool isActiveSettingInSettingsList(List<TreeViewItem<JObject>> settingsList, string entityId)
        {
            foreach (var setting in settingsList)
            {
                if (setting.data["metadata"]["entityId"].Value<string>() == entityId)
                {
                    return true;
                }
            }
            return false;
        }

        private List<TreeViewItem<JObject>> AddDeletedSettings(
            List<TreeViewItem<JObject>> tempItems, JArray activeSettings, int id)
        {
            foreach (var setting in activeSettings)
            {
                if (!isActiveSettingInSettingsList(tempItems, setting["metadata"]["entityId"].Value<string>()))
                {
                    tempItems.Add(new TreeViewItem<JObject>(id++, 0, setting["rs"]["key"].Value<string>(), (JObject)setting, false));
                }
            }
            return tempItems;
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem<JObject>(0, -1, "Root", new JObject());
            var id = 0;
            var allItems = new List<TreeViewItem>();
            if (settingsList != null && settingsList.Count > 0 && activeSettingsList != null)
            {
                var tempItems = settingsList
                    .Select(x => new TreeViewItem<JObject>(id++, 0, x["rs"]["key"].Value<string>(), (JObject)x, false))
                    .ToList<TreeViewItem<JObject>>();

                tempItems = AddDeletedSettings(tempItems, activeSettingsList, id);

                foreach (var activeRS in activeSettingsList)
                {
                    for (int i = 0; i < tempItems.Count; i++)
                    {
                        if (activeRS["metadata"]["entityId"].Value<string>() == tempItems[i].data["metadata"]["entityId"].Value<string>())
                        {
                            tempItems[i].data = (JObject)activeRS;
                            tempItems[i].enabled = true;
                        }
                    }
                }

                allItems = tempItems.ToList<TreeViewItem>();
            }

            SetupParentsAndChildrenFromDepths(root, allItems);

            return root;
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            var item = (TreeViewItem<JObject>)args.item;
            for (int i = 0; i < args.GetNumVisibleColumns(); ++i)
            {
                CellGUI(args.GetCellRect(i), item, args.GetColumn(i), ref args);
            }
        }

        void CellGUI(Rect cellRect, TreeViewItem<JObject> item, int column, ref RowGUIArgs args)
        {
            var isDisabled = enableEditingSettingsKeys && IsKeyInRules(item.data["metadata"]["entityId"].Value<string>(), rulesList);
            switch (column)
            {
                case 0:
                    EditorGUI.BeginDisabledGroup(!enableEditingSettingsKeys || isLoading || isDisabled);
                    var newKey = GUI.TextField(cellRect, item.data["rs"]["key"].Value<string>());
                    if (enableEditingSettingsKeys && !isLoading)
                    {
                        EditorGUIUtility.AddCursorRect(cellRect, MouseCursor.Text);
                    }
                    EditorGUI.EndDisabledGroup();
                    if (newKey != item.data["rs"]["key"].Value<string>())
                    {
                        var newObj = new JObject(item.data);
                        newObj["rs"]["key"] = newKey;
                        UpdateSetting?.Invoke(item.data, newObj);
                    }
                    break;
                case 1:
                    CenterRectUsingSingleLineHeight(ref cellRect);
                    EditorGUI.BeginDisabledGroup(!enableEditingSettingsKeys || isLoading || isDisabled);
                    GUIContent ddBtnContent = new GUIContent(string.IsNullOrEmpty(item.data["rs"]["type"].Value<string>()) ? "Select a type" : item.data["rs"]["type"].Value<string>());
                    if (GUI.Button(cellRect, ddBtnContent, EditorStyles.popup))
                    {
                        BuildPopupListForSettingTypes(item).DropDown(cellRect);
                    }
                    EditorGUI.EndDisabledGroup();

                    break;
                case 2:
                    EditorGUI.BeginDisabledGroup(isLoading || item.enabled == false);
                    var newSetting = new JObject(item.data);
                    switch (item.data["rs"]["type"].Value<string>())
                    {
                        case "string":
                            var formattedInputString = string.IsNullOrEmpty(item.data["rs"]["value"].Value<string>()) ? "" : item.data["rs"]["value"].Value<string>();
                            DateTime dateValue;
                            if (DateTime.TryParse(formattedInputString, out dateValue))
                            {
                                formattedInputString = JsonConvert.SerializeObject(item.data["rs"]["value"], rawDateSettings).Replace("\"", "");
                            }

                            var newStringValue = GUI.TextField(cellRect, formattedInputString);
                            if (!isLoading && item.enabled)
                            {
                                EditorGUIUtility.AddCursorRect(cellRect, MouseCursor.Text);
                            }
                            if (newStringValue != formattedInputString)
                            {
                                newSetting["rs"]["value"] = newStringValue;
                                UpdateSetting?.Invoke(item.data, newSetting);
                            }
                            break;
                        case "bool":
                            bool boolVal = false;
                            try
                            {
                                boolVal = item.data["rs"]["value"].Value<bool>();
                            }
                            catch (FormatException)
                            {
                                newSetting["rs"]["value"] = boolVal;
                                UpdateSetting?.Invoke(item.data, newSetting);
                            }
                            var menu = new GenericMenu();

                            menu.AddItem(new GUIContent("True"), boolVal == true, () =>
                            {
                                newSetting["rs"]["value"] = true;
                                UpdateSetting?.Invoke(item.data, newSetting);
                            });
                            menu.AddItem(new GUIContent("False"), boolVal == false, () =>
                            {
                                newSetting["rs"]["value"] = false;
                                UpdateSetting?.Invoke(item.data, newSetting);
                            });
                            GUIContent boolDdBtnContent = new GUIContent(boolVal.ToString());
                            if (GUI.Button(cellRect, boolDdBtnContent, EditorStyles.popup))
                            {
                                menu.DropDown(cellRect);
                            }
                            break;
                        case "float":
                            float floatVal = 0.0f;
                            try
                            {
                                floatVal = item.data["rs"]["value"].Value<float>();
                            }
                            catch (FormatException)
                            {
                                newSetting["rs"]["value"] = floatVal;
                                UpdateSetting?.Invoke(item.data, newSetting);
                            }
                            var floatFieldVal = EditorGUI.FloatField(cellRect, floatVal);
                            // TODO: abs check
                            if (floatFieldVal != floatVal)
                            {
                                newSetting["rs"]["value"] = floatFieldVal;
                                UpdateSetting?.Invoke(item.data, newSetting);
                            }
                            break;
                        case "int":
                            int intVal = 0;
                            try
                            {
                                intVal = item.data["rs"]["value"].Value<int>();
                            }
                            catch (FormatException)
                            {
                                newSetting["rs"]["value"] = intVal;
                                UpdateSetting?.Invoke(item.data, newSetting);
                            }
                            var intFieldValue = EditorGUI.IntField(cellRect, intVal);
                            if (intFieldValue != intVal)
                            {
                                newSetting["rs"]["value"] = intFieldValue;
                                UpdateSetting?.Invoke(item.data, newSetting);
                            }
                            break;
                        case "long":
                            long longVal = 0L;
                            try
                            {
                                longVal = item.data["rs"]["value"].Value<long>();
                            }
                            catch (FormatException)
                            {
                                newSetting["rs"]["value"] = longVal;
                                UpdateSetting?.Invoke(item.data, newSetting);
                            }
                            var longFieldValue = EditorGUI.LongField(cellRect, longVal);
                            if (longFieldValue != longVal)
                            {
                                newSetting["rs"]["value"] = longFieldValue;
                                UpdateSetting?.Invoke(item.data, newSetting);
                            }
                            break;
                        case "json":
                            string inputJsonValue = item.data["rs"]["value"].ToString();
                            if (GUI.Button(new Rect(cellRect.x, cellRect.y, cellRect.width, cellRect.height),
                                new GUIContent("Edit",EditorGUIUtility.FindTexture("vcs_document")), GUI.skin.button))
                            {
                                m_SettingsJsonModal = SettingsJsonModal.CreateInstance(inputJsonValue, item.data["rs"]["key"].ToString(), item.data["metadata"]["entityId"].ToString());
                            }

                            Event e = Event.current;
                            if (e.commandName == "JsonSubmitted")
                            {
                                e.commandName = null;

                                // find item with corresponding entityId before updating
                                if (m_SettingsJsonModal != null && m_SettingsJsonModal.currentText != inputJsonValue)
                                {
                                    for (int i = 0; i < activeSettingsList.Count; i++)
                                    {
                                        if (activeSettingsList[i]["metadata"]["entityId"].ToString() == m_SettingsJsonModal.keyEntityId)
                                        {
                                            newSetting["rs"]["key"] = activeSettingsList[i]["rs"]["key"].ToString();
                                            newSetting["rs"]["value"] = JToken.Parse(m_SettingsJsonModal.currentText);
                                            newSetting["metadata"]["entityId"] = m_SettingsJsonModal.keyEntityId;
                                            UpdateSetting?.Invoke((JObject)activeSettingsList[i], newSetting);
                                            break;
                                        }
                                    }
                                }
                            }

                            break;
                    }

                    EditorGUI.EndDisabledGroup();

                    break;

                case 3:
                    CenterRectUsingSingleLineHeight(ref cellRect);
                    if (enableEditingSettingsKeys)
                    {
                        //var isDisabled = enableEditingSettingsKeys &&
                        //                 IsKeyInRules(item.data["metadata"]["entityId"].Value<string>(), rulesList);
                        EditorGUI.BeginDisabledGroup(isDisabled || isLoading);
                        if (GUI.Button(cellRect,
                            new GUIContent(EditorGUIUtility.FindTexture("d_TreeEditor.Trash"), isDisabled ? "Can't remove a setting used in a rule" : "")))
                        {
                            UpdateSetting?.Invoke(item.data, null);
                        }

                        EditorGUI.EndDisabledGroup();
                        break;
                    }
                    else
                    {
                        var toggle = GUI.Toggle(cellRect, item.enabled, "");
                        if (toggle != item.enabled)
                        {
                            if (toggle)
                            {
                                UpdateSetting?.Invoke(null, item.data);
                            }
                            else
                            {
                                UpdateSetting?.Invoke(item.data, null);
                            }
                        }
                        break;
                    }
            }
        }

        private GenericMenu BuildPopupListForSettingTypes(TreeViewItem<JObject> treeViewItem)
        {
            var menu = new GenericMenu();

            for (int i = 0; i < RemoteConfigDataStore.rsTypes.Count; i++)
            {
                string name = RemoteConfigDataStore.rsTypes[i];
                menu.AddItem(new GUIContent(name), string.Equals(name, treeViewItem.data["rs"]["type"].Value<string>()), RSTypeChangedCallback, new RSTypeChangedStruct() { newType = name, rs = treeViewItem.data });
            }

            return menu;
        }

        private void RSTypeChangedCallback(object obj)
        {
            var rSTypeChangedStruct = (RSTypeChangedStruct)obj;
            if (rSTypeChangedStruct.newType != rSTypeChangedStruct.rs["rs"]["type"].Value<string>())
            {
                var newObj = new JObject(rSTypeChangedStruct.rs);
                newObj["rs"]["type"] = rSTypeChangedStruct.newType;
                var currentValueStringified = rSTypeChangedStruct.rs["rs"]["value"].ToString();
                switch (rSTypeChangedStruct.newType)
                {
                    case "string":
                        newObj["rs"]["value"] = currentValueStringified;
                        break;
                    case "int":
                        int intVal = 0;
                        try
                        {
                            Int32.TryParse(currentValueStringified, out intVal);
                        }
                        catch (FormatException)
                        {
                        }
                        newObj["rs"]["value"] = intVal;
                        break;
                    case "float":
                        float floatVal = 0.0f;
                        try
                        {
                            float.TryParse(currentValueStringified, NumberStyles.Float, CultureInfo.InvariantCulture, out floatVal);
                        }
                        catch (FormatException)
                        {
                        }
                        newObj["rs"]["value"] = floatVal;
                        break;
                    case "bool":
                        bool boolVal = false;
                        try
                        {
                            Boolean.TryParse(currentValueStringified, out boolVal);
                        }
                        catch (FormatException)
                        {
                        }
                        newObj["rs"]["value"] = boolVal;
                        break;
                    case "long":
                        long longVal = 0L;
                        try
                        {
                            Int64.TryParse(currentValueStringified, out longVal);
                        }
                        catch (FormatException)
                        {
                        }
                        newObj["rs"]["value"] = longVal;
                        break;
                    case "json":
                        JToken jObjectVal = new JObject();
                        try
                        {
                            jObjectVal = JToken.Parse(currentValueStringified);
                        }
                        catch (JsonReaderException)
                        {
                        }
                        newObj["rs"]["value"] = jObjectVal;
                        break;
                }

                UpdateSetting?.Invoke(rSTypeChangedStruct.rs, newObj);
            }
        }

        bool IsKeyInRules(string entityId, JArray rulesList)
        {
            foreach (var rule in rulesList)
            {

                if (rule["type"].Value<string>() == "segmentation")
                {
                    foreach (var setting in rule["value"])
                    {
                        if (entityId == setting["metadata"]["entityId"].Value<string>())
                        {
                            return true;
                        }
                    }
                }
                else if (rule["type"].Value<string>() == "variant")
                {
                    foreach (var variant in rule["value"])
                    {
                        foreach (var setting in (JArray)variant["values"])
                        {
                            if (setting["metadata"]["entityId"].Value<string>() == entityId)
                            {
                                return true;
                            }
                        }
                    }
                }

            }
            return false;
        }
    }

    internal class SettingsMultiColumnHeader : MultiColumnHeader
    {
        public SettingsMultiColumnHeader(MultiColumnHeaderState state) : base(state)
        {
            canSort = false;
            this.ResizeToFit();
        }
    }

    internal class TreeViewItem<T> : TreeViewItem
    {
        public T data;
        public bool enabled;

        public TreeViewItem(int id, int depth, string displayName, T data, bool enabled = true) : base(id, depth,
            displayName)
        {
            this.data = data;
            this.enabled = enabled;
        }

    }
}
