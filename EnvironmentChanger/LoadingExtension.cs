using ICities;

namespace EnvironmentChanger
{
    public class LoadingExtension : LoadingExtensionBase
    {

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            LoadPanelUI.Dispose();
            LoadMapPanelUI.Dispose();
            //TODO(earalov): restore
            //NewGamePanelUI.Dispose();
            LoadThemePanelUI.Dispose();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame)
            {
                LoadPanelUI.Initialize(true);
            }
            else if (mode == LoadMode.LoadMap || mode == LoadMode.NewMap)
            {
                LoadMapPanelUI.Initialize(true);
                LoadThemePanelUI.Initialize();
            }
        }

        public override void OnReleased()
        {
            base.OnReleased();
            LoadPanelUI.Dispose();
            LoadPanelUI.Initialize(false);
            //TODO(earalov): restore
            // NewGamePanelUI.Dispose();
            //NewGamePanelUI.Initialize();
            LoadMapPanelUI.Dispose();
            LoadMapPanelUI.Initialize(false);
            LoadThemePanelUI.Dispose();
            LoadThemePanelUI.Initialize();
        }
    }
}