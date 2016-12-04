using UnityEngine;

namespace EnvironmentChanger
{
    public class Initializer : MonoBehaviour
    {
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }
            new GameObject("EnvironmentChanger").AddComponent<Initializer>();
        }

        public void Awake()
        {
            if (!_initialized)
            {
                if (ToolsModifierControl.toolController == null || ToolsModifierControl.toolController.m_mode == ItemClass.Availability.None || ToolsModifierControl.toolController.m_mode == ItemClass.Availability.Game)
                {
                    LoadPanelUI.Initialize(this.gameObject);
                }
                if (ToolsModifierControl.toolController == null ||
                    ToolsModifierControl.toolController.m_mode == ItemClass.Availability.None)
                {
                    NewGamePanelUI.Initialize(this.gameObject);
                    NewScenarioGamePanelUI.Initialize(this.gameObject);
                }
                //TODO(earalov): restore
                //
                //                    LoadMapPanelUI.Initialize(false);
                //                    LoadThemePanelUI.Initialize();
                _initialized = true;
            }
        }

        public void OnDestroy()
        {
            _initialized = false;
        }

    }
}