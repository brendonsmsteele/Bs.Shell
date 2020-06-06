using Bs.Shell.Controllers;
using System.Collections.Generic;
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
            GoToFirstPage();
            //loadAndGo.Load();
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
            var models = new List<Model>();
            models.Add(new ExampleController.Model());
            shellServices.NavigationMap.NavigateToPage(models);
            //shellServices.NavigationMap.Navigate(NavigationTriggers.Next);
        }
    }
}