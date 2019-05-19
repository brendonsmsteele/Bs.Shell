using Bs.Shell.Navigation;
using UnityEngine;

namespace Bs.Shell
{
    [CreateAssetMenu(fileName =nameof(ShellServices), menuName = "Bs.Shell/Shell/" + nameof(ShellServices))]
    public class ShellServices : ScriptableObject, IInit
    {
        public NavigationMap NavigationMap;
        public ObjectPooler ObjectPooler;

        public virtual void Init()
        {
            Debug.Log("Init my services thanks.");
            NavigationMap.Init();
            ObjectPooler.Init();
        }
    }
}

