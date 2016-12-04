using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ColossalFramework.Packaging;
using EnvironmentChanger.Redirection;

namespace EnvironmentChanger.Detours
{
    [LoadSavePanelBaseType]
    public class LoadSavePanelBaseDetour : LoadSavePanelBase<MetaData>
    {
        [RedirectMethod]
        protected void RebuildThemeDictionary()
        {
            if (this.m_Themes == null)
                return;
            using (Dictionary<string, List<MapThemeMetaData>>.KeyCollection.Enumerator enumerator = this.m_Themes.Keys.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    this.m_Themes[enumerator.Current].Clear();
            }
            List<MapThemeMetaData> themes = new List<MapThemeMetaData>();

            Package.AssetType[] assetTypeArray = new Package.AssetType[1] { UserAssetType.MapThemeMetaData };
            foreach (Package.Asset filterAsset in PackageManager.FilterAssets(assetTypeArray))
            {
                if (filterAsset != (Package.Asset)null && filterAsset.isEnabled)
                {
                    MapThemeMetaData mapThemeMetaData = filterAsset.Instantiate<MapThemeMetaData>();
                    mapThemeMetaData.SetSelfRef(filterAsset);
                    if (!this.m_Themes.ContainsKey(mapThemeMetaData.environment))
                    {
                        List<MapThemeMetaData> mapThemeMetaDataList = new List<MapThemeMetaData>();
                        this.m_Themes[mapThemeMetaData.environment] = mapThemeMetaDataList;
                    }
                    //begin mod
                    themes.Add(mapThemeMetaData);
                    //end mod
                }
            }

            //begin mod
            this.m_Themes["Tropical"] = themes;
            this.m_Themes["Sunny"] = themes;
            this.m_Themes["Winter"] = themes;
            this.m_Themes["North"] = themes;
            this.m_Themes["Europe"] = themes;
            //end mod
            this.RefreshOverrideThemes();
        }



        //        [RedirectMethod]
        //        protected void RefreshOverrideThemes()
        //        {
        //            UnityEngine.Debug.Log("AAA");
        //            if (this.m_Themes == null || (UnityEngine.Object)this.m_ThemeOverrideDropDown == (UnityEngine.Object)null)
        //                return;
        //            if (!string.IsNullOrEmpty(this.m_SelectedEnvironment))
        //            {
        //                if (!this.m_Themes.ContainsKey(this.m_SelectedEnvironment))
        //                    this.m_Themes[this.m_SelectedEnvironment] = new List<MapThemeMetaData>();
        //                //begin mod
        //            }
        //            List<MapThemeMetaData> mapThemeMetaDataList = this.m_Themes.SelectMany(e => e.Value).Distinct().ToList();
        //            //end mod
        //            string[] strArray = new string[mapThemeMetaDataList.Count + 1];
        //            strArray[0] = ColossalFramework.Globalization.Locale.Get("MAPTHEME_NONE");
        //            for (int index = 0; index < mapThemeMetaDataList.Count; ++index)
        //            {
        //                string str = mapThemeMetaDataList[index].name;
        //                if (mapThemeMetaDataList[index] != null)
        //                    strArray[index + 1] = str;
        //            }
        //            this.m_ThemeOverrideDropDown.items = strArray;
        //            //begin mod
        //            //end mod
        //        }
        //
        //        [RedirectMethod]
        //        protected void SelectThemeOverride(string name)
        //        {
        //            UnityEngine.Debug.Log("CCC");
        //            this.m_ShowMissingThemeWarning = false;
        //            if (this.m_Themes == null || (UnityEngine.Object)this.m_ThemeOverrideDropDown == (UnityEngine.Object)null)
        //                return;
        //            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(this.m_SelectedEnvironment))
        //                this.m_ThemeOverrideDropDown.selectedIndex = 0;
        //            //begin mod
        //            else if (!this.m_Themes.ContainsKey(this.m_SelectedEnvironment))
        //            {
        //                //end mod
        //                this.m_ThemeOverrideDropDown.selectedIndex = 0;
        //                this.m_ShowMissingThemeWarning = true;
        //            }
        //            else
        //            {
        //                //begin mod
        //                List<MapThemeMetaData> mapThemeMetaDataList = this.m_Themes.SelectMany(e => e.Value).Distinct().ToList();
        //                //end mod
        //                for (int index = 0; index < mapThemeMetaDataList.Count; ++index)
        //                {
        //                    if (name == mapThemeMetaDataList[index].mapThemeRef)
        //                    {
        //                        this.m_ThemeOverrideDropDown.selectedIndex = index + 1;
        //                        return;
        //                    }
        //                }
        //                this.m_ThemeOverrideDropDown.selectedIndex = 0;
        //                this.m_ShowMissingThemeWarning = true;
        //            }
        //        }
        //
        //        private bool m_ShowMissingThemeWarning
        //        {
        //            set
        //            {
        //                UnityEngine.Debug.Log("AAA");
        //                var field = this.GetType().BaseType.GetField("m_ShowMissingThemeWarning", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
        //                if (field == null)
        //                {
        //                    UnityEngine.Debug.Log("Field is null!");
        //                }
        //                field.SetValue(this, value);
        //            }
        //        }
    }
}