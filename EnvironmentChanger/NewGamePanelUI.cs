using System.Reflection;
using ColossalFramework.UI;
using EnvironmentChanger.Detours;
using UnityEngine;

namespace EnvironmentChanger
{
    public class NewGamePanelUI : AbstractUI<MapMetaData>
    {
        private UILabel label;
        private NewGamePanel newGamePanel;
        private UIListBox mapList;
        private static GameObject go;
        private UIPanel overridePanel;

        public static void Initialize()
        {
            if (go != null)
            {
                return;
            }
            go = new GameObject("EnvironmentChangerNewGamePanel");
            go.AddComponent<NewGamePanelUI>();
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
                var panelGo = GameObject.Find("(Library) NewGamePanel");
                if (panelGo == null)
                {
                    return;
                }
                saveLoadPanel = panelGo.GetComponent<NewGamePanel>();
                mapList = saveLoadPanel.Find<UIListBox>("MapList");
                mapList.eventSelectedIndexChanged += OnListingSelectionChanged;

                var panel = panelGo.GetComponent<UIPanel>().Find<UIPanel>("Panel");
                label = panel.Find<UILabel>("MapTheme");
                label.AlignTo(panel, UIAlignAnchor.TopLeft);
                label.relativePosition = new Vector3(343, 20);

                overridePanel = panel.Find<UIPanel>("OverridePanel");
                var overrideMapTheme = panel.Find<UIDropDown>("OverrideMapTheme");
                Destroy(overrideMapTheme);

                envDropDown = UIUtils.CreateDropDown(label.parent);
                envDropDown.name = "EnvironmentDropDown";
                envDropDown.size = new Vector2(196, 32.0f);
                envDropDown.textScale = label.textScale;
                envDropDown.relativePosition = new Vector3(label.relativePosition.x, label.relativePosition.y + label.height);
                envDropDown.eventSelectedIndexChanged += OnEnvDropDownEventSelectedIndexChanged;

                var newMapThemeLabel = label.parent.AddUIComponent<UILabel>();
                newMapThemeLabel.text = "Custom Map Theme";
                newMapThemeLabel.textScale = label.textScale;
                newMapThemeLabel.textColor = label.textColor;
                newMapThemeLabel.relativePosition = new Vector3(envDropDown.relativePosition.x, envDropDown.relativePosition.y + envDropDown.height);

                themeDropDown = UIUtils.CreateDropDown(newMapThemeLabel.parent);
                themeDropDown.name = "MapThemeDropDown";
                themeDropDown.size = new Vector2(196, 32.0f);
                themeDropDown.textScale = label.textScale;
                themeDropDown.relativePosition = new Vector3(newMapThemeLabel.relativePosition.x, newMapThemeLabel.relativePosition.y + newMapThemeLabel.height);
                themeDropDown.eventSelectedIndexChanged += OnThemeDropDownEventSelectedIndexChanged;
                
                typeof(NewGamePanel).GetField("m_ThemeOverrideDropDown", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(saveLoadPanel, null);
            }
            if (label == null || !label.parent.isVisible)
            {
                return;
            }
            label.text = "Environment";
            overridePanel.Hide();
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