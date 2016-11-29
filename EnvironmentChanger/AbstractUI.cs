using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ColossalFramework.Packaging;
using ColossalFramework.UI;
using UnityEngine;
using static EnvironmentChanger.Util;

namespace EnvironmentChanger
{
    public abstract class AbstractUI<T> : MonoBehaviour where T : MetaData
    {
        private const string NO_THEME = "No Theme";
        private const string WINTER = "Winter";
        private string AS_IS = " (Don't change)";
        private string AS_IS_LEGACY = "(Don't change)";

        protected MethodInfo GetThemeStringMethod = typeof(LoadSavePanelBase<>).MakeGenericType(typeof(T)).
            GetMethod("GetThemeString", BindingFlags.NonPublic | BindingFlags.Static);
        protected MethodInfo GetListingMetaDataMethod = typeof(LoadSavePanelBase<>).MakeGenericType(typeof(T)).
            GetMethod("GetListingMetaData", BindingFlags.NonPublic | BindingFlags.Instance);

        protected UIDropDown envDropDown;
        protected UIDropDown themeDropDown;
        private T selectedMetaData;
        private string originalTheme;
        private readonly List<string> _themesIds = new List<string>();
        protected LoadSavePanelBase<T> saveLoadPanel;


        protected void OnDestroy()
        {
            ResetSelectedMetadata();
        }

        protected void ResetPreviousEnvironment()
        {
            if (selectedMetaData == null)
            {
                return;
            }
            ForceEnvironment(null);
        }

        protected abstract void ForceEnvironment(string env);

        protected void ResetPreviousTheme()
        {
            if (selectedMetaData == null)
            {
                return;
            }
            SetMetadataTheme(selectedMetaData, originalTheme);
        }

        private void ResetSelectedMetadata()
        {
            if (selectedMetaData == null)
            {
                return;
            }
            ResetPreviousEnvironment();
            ResetPreviousTheme();
            selectedMetaData = null;
            originalTheme = null;
        }

        protected void OnListingSelectionChanged(UIComponent comp, int sel)
        {
            ResetSelectedMetadata();
            if (saveLoadPanel == null || sel < 0)
            {
                return;
            }
            var metadata = (T)GetListingMetaDataMethod.Invoke(saveLoadPanel, new object[] { sel });
            selectedMetaData = metadata;
            originalTheme = GetMetadataTheme(metadata);
            SetupEnvironmentDropDown(metadata);
            SetupThemeDropDown(metadata);
        }

        private void SetupEnvironmentDropDown(T metadata)
        {
            if (envDropDown == null)
            {
                return;
            }
            var envList = new List<string>();
            if (GetMetadataEnvironment(metadata) == null)
            {
                envList.Add(AS_IS_LEGACY);
            }
            else
            {
                envList.Add((string)GetThemeStringMethod.Invoke(null, new object[]
                {
                    null,
                    GetMetadataEnvironment(metadata),
                    null
                }) + AS_IS);
            }
            var winterAvailable = SteamHelper.IsDLCOwned(SteamHelper.DLC.SnowFallDLC);
            envList.AddRange(from env in Constants.Envs
                             where env != WINTER || winterAvailable
                             select (string)GetThemeStringMethod.Invoke(null, new object[]
                             {null, env, null}));
            envDropDown.items = envList.ToArray();
            envDropDown.selectedIndex = 0;
        }

        private void SetupThemeDropDown(T metadata)
        {
            if (themeDropDown == null)
            {
                return;
            }
            _themesIds.Clear();
            var themeList = new List<string>();
            if (GetMetadataTheme(metadata) == null)
            {
                _themesIds.Add(null);
                themeList.Add(NO_THEME + AS_IS);
            }
            else
            {
                themeList.Add((string)GetThemeStringMethod.Invoke(null, new object[]
                {
                    GetMetadataTheme(metadata),
                    null,
                    null
                }) + AS_IS);
            }
            _themesIds.Add(null);
            themeList.Add(NO_THEME);
            foreach (var themeId in PackageManager.FilterAssets(UserAssetType.MapThemeMetaData)
                .Select(asset => $"{asset.package.packageName}.{asset.name}"))
            {
                _themesIds.Add(themeId);
                themeList.Add((string)GetThemeStringMethod.Invoke(null, new object[]
                {
                    themeId,
                    null,
                    null
                }));
            }
            themeDropDown.items = themeList.ToArray();
            themeDropDown.selectedIndex = 0;
        }

        protected void OnEnvDropDownEventSelectedIndexChanged(UIComponent component, int value)
        {
            ResetPreviousEnvironment();
            if (value < 0)
            {
                return;
            }
            if (value == 0)
            {
                ForceEnvironment(null);
                return;
            }
            if (value < Constants.Envs.Length + 1)
            {
                if (selectedMetaData != null)
                {
                    ForceEnvironment(Constants.Envs[value - 1]);
                }
            }
            else
            {
                throw new Exception("Wrong index!");
            }
        }

        protected void OnThemeDropDownEventSelectedIndexChanged(UIComponent component, int value)
        {
            ResetPreviousTheme();
            if (value <= 0)
            {
                return;
            }
            if (value < _themesIds.Count)
            {
                if (selectedMetaData != null)
                {
                    SetMetadataTheme(selectedMetaData, _themesIds[value]);
                }
            }
            else
            {
                throw new Exception("Wrong index!");
            }
        }
    }
}