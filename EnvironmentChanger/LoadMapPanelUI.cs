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
        public bool inGame;

        public static void Initialize(bool inGame)
        {
            if (go != null)
            {
                return;
            }
            go = new GameObject("EnvironmentChangerLoadMapPanel");
            go.AddComponent<LoadMapPanelUI>().inGame = inGame;
        }

        public static void Dispose()
        {
            if (go == null)
            {
                return;
            }
            DestroyImmediate(go);
            go = null;
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
//                var overrideMapTheme = loadPanel.Find<UIDropDown>("OverrideMapTheme");
//                Destroy(overrideMapTheme);

                mapThemeLabel = loadPanel.Find<UILabel>("MapThemeLabel");
                mapThemeLabel.text = "Environment";
                label = loadPanel.Find<UILabel>("MapTheme");
                envDropDown = UIUtils.CreateDropDown(label.parent);
                envDropDown.name = "EnvironmentDropDown";
                envDropDown.size = new Vector2(244.0f, 32.0f);
                envDropDown.textScale = label.textScale;
                envDropDown.relativePosition = label.relativePosition;
                envDropDown.eventSelectedIndexChanged += OnEnvDropDownEventSelectedIndexChanged;

//                var newMapThemeLabel = mapThemeLabel.parent.AddUIComponent<UILabel>();
//                newMapThemeLabel.text = "Custom Map Theme";
//                newMapThemeLabel.textScale = mapThemeLabel.textScale;
//                newMapThemeLabel.textColor = mapThemeLabel.textColor;
//                newMapThemeLabel.relativePosition = new Vector3(envDropDown.relativePosition.x, envDropDown.relativePosition.y + envDropDown.height);


//                themeDropDown = UIUtils.CreateDropDown(newMapThemeLabel.parent);
//                themeDropDown.name = "MapThemeDropDown";
//                themeDropDown.size = new Vector2(244.0f, 32.0f);
//                themeDropDown.textScale = label.textScale;
//                themeDropDown.relativePosition = new Vector3(newMapThemeLabel.relativePosition.x, newMapThemeLabel.relativePosition.y + newMapThemeLabel.height);
//                themeDropDown.eventSelectedIndexChanged += OnThemeDropDownEventSelectedIndexChanged;
//                if (inGame)
//                {
//                    SimulationManager.instance.AddAction(() =>
//                    {
//                        typeof(LoadMapPanel).GetField("m_ThemeOverrideDropDown", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(saveLoadPanel, null);
//                    });
//                }
//                else {
//                    typeof(LoadMapPanel).GetField("m_ThemeOverrideDropDown", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(saveLoadPanel, null);
//                }
            }
            if (label == null || !label.parent.isVisible)
            {
                return;
            }
            label.Hide();
//            mapThemeLabel.Show();
//            overridePanel.Hide();
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