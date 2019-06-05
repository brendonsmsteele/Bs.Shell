using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    [CreateAssetMenu(fileName =nameof(NavigationPageLoader), menuName = "Bs.Shell/Navigation/"+ nameof(NavigationPageLoader))]
    public class NavigationPageLoader : Service
    {
        string pathToNavPagesInResources = "NavigationMap/NavigationPages";

        Dictionary<string, NavigationPage> allPages = new Dictionary<string, NavigationPage>();

        public override void Init()
        {
            LoadAllPagesFromResources();
        }

        private void LoadAllPagesFromResources()
        {
            allPages = new Dictionary<string, NavigationPage>();
            var objects = LoadAllObjectsFromPath(pathToNavPagesInResources);
            var clones = CloneObjects(objects); //  Clone so we prevent crazy overwriting data issues.
            var controllers = clones.Cast<ControllerData>().ToArray();
            var navPage = CreateNavigationPage(controllers);
            CloneControllers(navPage);
            var key = navPage.name;
            allPages.Add(key, navPage);
        }

        private List<Object> LoadAllObjectsFromPath(string path)
        {
            var controllersFromPath = Resources.LoadAll(path).ToList();
            return controllersFromPath;
        }
       
        private List<Object> CloneObjects(List<Object> objects)
        {
            var clones = new List<Object>();
            foreach (var obj in objects)
                clones.Add(Instantiate(obj));
            return clones;
        }

        private NavigationPage CloneControllers(NavigationPage navPage)
        {
            var clonedControllers = new List<ControllerData>();
            foreach (var controller in navPage.ActiveControllers)
                clonedControllers.Add(Instantiate(controller));
            navPage.ActiveControllers = clonedControllers;
            return navPage;
        }

        private NavigationPage CreateNavigationPage(ControllerData[] controllers)
        {
            var navPage = NavigationPage.Create(controllers);
            return navPage;
        }

        public NavigationPage GetPage(string key)
        {
            if (allPages.ContainsKey(key))
                return allPages[key];
            return null;
        }
    }
}