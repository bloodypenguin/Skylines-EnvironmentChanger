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
    public abstract class AbstractUI<T> : LoadSavePanelBase<T> where T : MetaData
    {
        private const string WINTER = "Winter";
        private string AS_IS = " (Don't change)";
        private string AS_IS_LEGACY = "(Don't change)";

        protected MethodInfo GetListingMetaDataMethod = typeof(LoadSavePanelBase<>).MakeGenericType(typeof(T)).
            GetMethod("GetListingMetaData", BindingFlags.NonPublic | BindingFlags.Instance);

        protected UIDropDown envDropDown;
        private T selectedMetaData;
        protected LoadSavePanelBase<T> saveLoadPanel;


        protected override void Awake() //make base class method a stub
        {

        }

        public override void OnClosed() //make base class method a stub
        {

        }

        protected override void OnLocaleChanged() //make base class method a stub
        {

        }


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

        private void ResetSelectedMetadata()
        {
            if (selectedMetaData == null)
            {
                return;
            }
            ResetPreviousEnvironment();
            selectedMetaData = null;
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
            SetupEnvironmentDropDown(metadata);
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
                envList.Add(GetThemeString(
                    null,
                    GetMetadataEnvironment(metadata),
                    null
                ) + AS_IS);
            }
            var winterAvailable = SteamHelper.IsDLCOwned(SteamHelper.DLC.SnowFallDLC);
            envList.AddRange(from env in Constants.Envs
                             where env != WINTER || winterAvailable
                             select GetThemeString(null, env, null));
            envDropDown.items = envList.ToArray();
            envDropDown.selectedIndex = 0;
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
    }
}