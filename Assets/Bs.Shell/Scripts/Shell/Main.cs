using UnityEngine;

namespace Bs.Shell.Navigation
{
    public class Main : MonoBehaviour
    {
        //  TODO: Remove this lol
        public ShellServices shellServices;
        public LoadAndGo loadAndGo;

        void Start()
        {
            //  Init the Shell
            InitShell();
            InitServices();
            loadAndGo.Load();
        }

        private void InitShell()
        {
            var app = ScriptableObject.CreateInstance<Bs.Shell.App>();
            app.Init();
        }

        private void InitServices()
        {
            shellServices.Init();
        }

        private void GoToFirstPage()
        {
            shellServices.NavigationMap.Navigate(NavigationTriggers.Next);
        }
    }
}