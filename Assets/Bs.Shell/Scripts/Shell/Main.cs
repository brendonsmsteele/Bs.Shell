using Nc.Shell.Navigation;
using Nc.Shell.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Nc.Shell
{
    public class Main : MonoBehaviour
    {
        public ShellServices shellServices;

        void Start()
        {
            InitShell();
            InitServices();
            GoToFirstPage();
        }

        private void InitShell()
        {
            var app = ScriptableObject.CreateInstance<App>();
            app.Init();
        }

        private void InitServices()
        {
            shellServices.Init();
        }

        private void GoToFirstPage()
        {
            var models = new List<Model>();
            models.Add(new ExampleSceneController.Model());
            shellServices.NavigationMap.Navigate(NavigationTriggers.Next);
        }
    }
}