using System;
using ColossalFramework;
using ColossalFramework.Packaging;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using EnvironmentChanger.Redirection;

namespace EnvironmentChanger.Detours
{
    [TargetType(typeof(NewGamePanel))]
    public class NewGamePanelDetour : NewGamePanel
    {
        public static string m_forceEnvironment = null;

        [RedirectMethod]
        private void StartNewGameRoutine()
        {
            //begin mod
            var m_MapName = this.Find<UITextField>("MapName");
            var m_FileList = this.Find<UIListBox>("MapList");
            //end mod
            SimulationMetaData ngs = new SimulationMetaData() { m_CityName = m_MapName.text, m_gameInstanceIdentifier = Guid.NewGuid().ToString(), m_invertTraffic = !this.Find<UICheckBox>("InvertTraffic").isChecked ? SimulationMetaData.MetaBool.False : SimulationMetaData.MetaBool.True, m_disableAchievements = Singleton<PluginManager>.instance.enabledModCount <= 0 ? SimulationMetaData.MetaBool.False : SimulationMetaData.MetaBool.True, m_currentDateTime = DateTime.Now, m_newGameAppVersion = 159507472, m_updateMode = SimulationManager.UpdateMode.NewGameFromMap };
            //begin mod
            ngs.m_environment = m_forceEnvironment;
            //end mod

            MapMetaData listingMetaData = this.GetListingMetaData(m_FileList.selectedIndex);
            if (listingMetaData.mapThemeRef != null)
            {
                Package.Asset assetByName = PackageManager.FindAssetByName(listingMetaData.mapThemeRef);
                if (assetByName != (Package.Asset)null)
                {
                    ngs.m_MapThemeMetaData = assetByName.Instantiate<MapThemeMetaData>();
                    ngs.m_MapThemeMetaData.SetSelfRef(assetByName);
                }
            }
            Singleton<LoadingManager>.instance.LoadLevel(this.GetListingData(m_FileList.selectedIndex), "Game", "InGame", ngs);
            UIView.library.Hide(this.GetType().Name, 1);
        }
    }
}