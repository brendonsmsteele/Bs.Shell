using UnityEngine;

namespace Bs.Shell.Navigation
{
    public class Main : MonoBehaviour
    {
        public ShellServices services;

        void Start()
        {
            //  Init the Shell
            InitShell();
            InitServices();
            NavigateToFirstPage();
        }

        private void InitShell()
        {
            var app = ScriptableObject.CreateInstance<Bs.Shell.App>();
            app.Init();
        }

        private void InitServices()
        {
            services.Init();
        }

        private void NavigateToFirstPage()
        {
            services.NavigationMap.Navigate(NavigationTriggers.Next);
        }
    }
}