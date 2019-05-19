using Bs.Shell.Navigation;
using UnityEngine;

namespace Bs.Shell
{
    public class ShellServices : ScriptableObject, IInit
    {
        public NavigationMap NavigationMap;

        public virtual void Init()
        {
            Debug.Log("Init my services thanks.");
            NavigationMap.Init();
        }
    }
}

