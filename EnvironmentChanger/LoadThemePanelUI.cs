using ColossalFramework.UI;
using UnityEngine;

namespace EnvironmentChanger
{
    public class LoadThemePanelUI : AbstractUI<MapThemeMetaData>
    {
        private static GameObject go;
        private UILabel label;
        private UILabel mapThemeLabel;
        private UIListBox saveList;

        public static void Initialize()
        {
            if (go != null)
            {
                return;
            }
            go = new GameObject("EnvironmentChangerLoadThemePanel");
            go.AddComponent<LoadThemePanelUI>();
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
                var panelGo = GameObject.Find("(Library) LoadThemePanel");
                if (panelGo == null)
                {
                    return;
                }
                saveLoadPanel = panelGo.GetComponent<LoadThemePanel>();
                saveList = saveLoadPanel.component.Find<UIListBox>("SaveList");
                saveList.eventSelectedIndexChanged += OnListingSelectionChanged;
                var loadPanel = panelGo.GetComponent<UIPanel>();

                label = loadPanel.Find<UILabel>("MapTheme");
                mapThemeLabel = loadPanel.Find<UILabel>("MapThemeLabel");
                mapThemeLabel.text = "Environment";

                envDropDown = UIUtils.CreateDropDown(label.parent);
                envDropDown.name = "EnvironmentDropDown";
                envDropDown.size = new Vector2(196, 32.0f);
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
        }

        protected override void ForceEnvironment(string env)
        {
            if (saveLoadPanel == null)
            {
                return;
            }
            ((LoadThemePanel) saveLoadPanel).m_forceEnvironment = env;
        }
    }
}