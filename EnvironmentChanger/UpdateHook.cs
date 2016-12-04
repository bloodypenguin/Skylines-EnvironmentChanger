using UnityEngine;

namespace EnvironmentChanger
{
    public class UpdateHook : MonoBehaviour
    {
        public delegate void OnUnityUpdate();

        public OnUnityUpdate onUnityUpdate = null;
        public bool once = true;

        void Update()
        {
            if (onUnityUpdate != null)
            {
                onUnityUpdate();
                if (once)
                {
                    Destroy(this);
                }
            }
        }

    }
}
