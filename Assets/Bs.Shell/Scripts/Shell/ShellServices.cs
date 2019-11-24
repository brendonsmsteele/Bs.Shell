using Bs.Shell.Navigation;
using UnityEngine;

namespace Bs.Shell
{
    [CreateAssetMenu(fileName =nameof(ShellServices), menuName = "Bs.Shell/Shell/" + nameof(ShellServices))]
    public class ShellServices : ScriptableObject, IInit
    {
        static ShellServices _instance;
        public static ShellServices Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Resources.Load("ShellServices") as ShellServices;
                return _instance;
            }
        }

        public NavigationController NavigationMap;
        public ObjectPooler ObjectPooler;

        public virtual void Init()
        {
            Debug.Log("Init my services thanks.");
            NavigationMap.Init();
            ObjectPooler.Init();
        }
    }
}

