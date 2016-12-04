using EnvironmentChanger.Detours;
using EnvironmentChanger.Redirection;
using ICities;

namespace EnvironmentChanger
{
    public class Mod : IUserMod
    {
        private static bool _detoured;

        private static bool _bootstrapped;

        public Mod()
        {
            if (_detoured)
            {
                return;
            }
            RedirectionUtil.RedirectType(typeof(LoadSavePanelBaseDetour));
            RedirectionUtil.RedirectType(typeof(NewGamePanelDetour));
            RedirectionUtil.RedirectType(typeof(NewScenarioGamePanelDetour));
            _detoured = true;
        }

        public string Name
        {
            get
            {
                if (!_bootstrapped)
                {
                    LoadPanelUI.Initialize();
                    NewGamePanelUI.Initialize();
                    NewScenarioGamePanelUI.Initialize();
                    //TODO(earalov): restore
                    //
                    //                    LoadMapPanelUI.Initialize(false);
                    //                    LoadThemePanelUI.Initialize();
                    _bootstrapped = true;
                }
                return "Environment Changer";
            }
        }

        public string Description => "Allows to switch environment or theme when starting a new game or loading a save game/map/theme!";
    }
}
