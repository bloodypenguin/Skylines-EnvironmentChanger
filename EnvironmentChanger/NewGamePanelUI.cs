using System.Reflection;
using ColossalFramework.UI;
using EnvironmentChanger.Detours;
using UnityEngine;

namespace EnvironmentChanger
{
    public class NewGamePanelUI : AbstractUI<MapMetaData>
    {
        private UILabel label;
        private UIPanel overridePanel;

        public static void Initialize(GameObject go)
        {
            go.AddComponent<NewGamePanelUI>();
        }

        protected override void Awake()
        {
            base.Awake();
            NewGamePanelDetour.m_forceEnvironment = null;
        }

        public void Update()
        {
            if (label == null)
            {
                var panelGo = GameObject.Find("NewGamePanel");
                if (panelGo == null)
                {
                    return;
                }
                saveLoadPanel = panelGo.GetComponent<NewGamePanel>();
                var mapList = saveLoadPanel.Find<UIListBox>("MapList");
                mapList.eventSelectedIndexChanged += OnListingSelectionChanged;

                var panel = panelGo.GetComponent<UIPanel>();
                overridePanel = panel.Find<UIPanel>("MapThemeOverridePanel");
                var overrideMapTheme = panel.Find<UIDropDown>("OverrideMapTheme");
                label = panel.Find<UILabel>("MapTheme");
                envDropDown = UIUtils.CreateDropDown(label.parent);
                envDropDown.name = "EnvironmentDropDown";
                envDropDown.size = new Vector2(196, 27.0f);
                envDropDown.textScale = label.textScale;
                envDropDown.relativePosition = new Vector3(label.relativePosition.x, overrideMapTheme.relativePosition.y);
                envDropDown.eventSelectedIndexChanged += OnEnvDropDownEventSelectedIndexChanged;

                overrideMapTheme.relativePosition = new Vector3(overrideMapTheme.relativePosition.x + (envDropDown.width - label.width), overrideMapTheme.relativePosition.y);

            }
            if (label == null || !label.parent.isVisible)
            {
                return;
            }
            label.text = "Base map theme";
            overridePanel.Show();
        }

        protected override void ForceEnvironment(string env)
        {
            if (saveLoadPanel == null)
            {
                return;
            }
            NewGamePanelDetour.m_forceEnvironment = env;
        }
    }
}