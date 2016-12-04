using System.Reflection;
using ColossalFramework.UI;
using UnityEngine;

namespace EnvironmentChanger
{
    public class LoadMapPanelUI : AbstractUI<MapMetaData>
    {
        private static GameObject go;
        private UILabel label;
        private UILabel mapThemeLabel;
        private UIListBox saveList;
        private UIPanel overridePanel;

        public static void Initialize(GameObject go)
        {
            go.AddComponent<LoadMapPanelUI>();
        }

        public void Update()
        {
            if (label == null)
            {
                var panelGo = GameObject.Find("(Library) LoadMapPanel");
                if (panelGo == null)
                {
                    return;
                }
                saveLoadPanel = panelGo.GetComponent<LoadMapPanel>();
                saveList = saveLoadPanel.Find<UIListBox>("SaveList");
                saveList.eventSelectedIndexChanged += OnListingSelectionChanged;
                var loadPanel = panelGo.GetComponent<UIPanel>();
                overridePanel = loadPanel.Find<UIPanel>("OverridePanel");

                mapThemeLabel = loadPanel.Find<UILabel>("MapThemeLabel");

                label = loadPanel.Find<UILabel>("MapTheme");
                envDropDown = UIUtils.CreateDropDown(label.parent);
                envDropDown.name = "EnvironmentDropDown";
                envDropDown.size = new Vector2(244.0f, 27.0f);
                envDropDown.textScale = label.textScale;
                envDropDown.relativePosition = new Vector3(mapThemeLabel.relativePosition.x + mapThemeLabel.width + 50, mapThemeLabel.relativePosition.y);
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
            ((LoadMapPanel) saveLoadPanel).m_forceEnvironment = env;
        }
    }
}