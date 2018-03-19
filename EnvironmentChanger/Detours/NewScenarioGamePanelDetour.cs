using System;
using ColossalFramework;
using ColossalFramework.Packaging;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using EnvironmentChanger.Redirection;

namespace EnvironmentChanger.Detours
{
    [TargetType(typeof(NewScenarioGamePanel))]
    public class NewScenarioGamePanelDetour : NewScenarioGamePanel
    {
        public static string m_forceEnvironment = null;

        [RedirectMethod]
        private void StartNewGameRoutine()
        {
            //begin mod
            var m_CityName = this.Find<UITextField>("MapName");
            var m_FileList = this.Find<UIListBox>("MapList");
            var m_leftHandTraffic = this.Find<UICheckBox>("InvertTraffic");
            //end mod

            Package.Asset listingData = this.GetListingData(m_FileList.selectedIndex);
            ScenarioMetaData listingMetaData = this.GetListingMetaData(m_FileList.selectedIndex);
            SimulationMetaData ngs = new SimulationMetaData()
            {
                m_CityName = m_CityName.text,
                m_gameInstanceIdentifier = Guid.NewGuid().ToString(),
                m_invertTraffic = !m_leftHandTraffic.isChecked ? SimulationMetaData.MetaBool.False : SimulationMetaData.MetaBool.True,
                m_disableAchievements = Singleton<PluginManager>.instance.enabledModCount <= 0 ? SimulationMetaData.MetaBool.False : SimulationMetaData.MetaBool.True,
                m_currentDateTime = DateTime.Now,
                m_newGameAppVersion = 159507472,
                m_updateMode = SimulationManager.UpdateMode.NewGameFromScenario,
                m_ScenarioAsset = StringUtils.SafeFormat("{0}.{1}", (object)listingData.package.packageName, (object)listingData.name),
                m_ScenarioName = listingMetaData.scenarioName
            };
            //begin mod
            ngs.m_environment = m_forceEnvironment;
            //end mod
            if (listingMetaData.mapThemeRef != null)
            {
                Package.Asset assetByName = PackageManager.FindAssetByName(listingMetaData.mapThemeRef);
                if (assetByName != (Package.Asset)null)
                {
                    ngs.m_MapThemeMetaData = assetByName.Instantiate<MapThemeMetaData>();
                    ngs.m_MapThemeMetaData.SetSelfRef(assetByName);
                }
            }
            Singleton<LoadingManager>.instance.LoadLevel(listingData, "Game", "InGame", ngs, false);
            UIView.library.Hide(this.GetType().Name, 1);
        }
    }
}