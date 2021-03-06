﻿using System.Reflection;
using System.Runtime.CompilerServices;
using ColossalFramework.UI;
using UnityEngine;

namespace EnvironmentChanger
{
    public class LoadPanelUI : AbstractUI<SaveGameMetaData>
    {
        private UILabel label;
        private UILabel mapThemeLabel;
        private UIListBox saveList;
        private UIPanel overridePanel;

        public static void Initialize(GameObject go)
        {
            go.AddComponent<LoadPanelUI>();
        }

        public void Update()
        {
            if (label == null)
            {
                var panelGo = GameObject.Find("(Library) LoadPanel");
                if (panelGo == null)
                {
                    return;
                }
                saveLoadPanel = panelGo.GetComponent<LoadPanel>();
                saveList = saveLoadPanel.Find<UIListBox>("SaveList");
                saveList.eventSelectedIndexChanged += OnListingSelectionChanged;
                var loadPanel = panelGo.GetComponent<UIPanel>();

                label = loadPanel.Find<UILabel>("MapTheme");
                mapThemeLabel = loadPanel.Find<UILabel>("MapThemeLabel");
                overridePanel = loadPanel.Find<UIPanel>("OverridePanel");

                envDropDown = UIUtils.CreateDropDown(label.parent);
                envDropDown.name = "EnvironmentDropDown";
                envDropDown.size = new Vector2(label.width, 27.0f);
                envDropDown.textScale = label.textScale;
                envDropDown.relativePosition = label.relativePosition;
                envDropDown.eventSelectedIndexChanged += OnEnvDropDownEventSelectedIndexChanged;
            }
            if (label == null || !label.parent.isVisible)
            {
                return;
            }
            label.Hide();
            mapThemeLabel.Show();
            overridePanel.Show();
        }

        protected override void ForceEnvironment(string env)
        {
            if (saveLoadPanel == null)
            {
                return;
            }
            ((LoadPanel)saveLoadPanel).m_forceEnvironment = env;
        }
    }
}