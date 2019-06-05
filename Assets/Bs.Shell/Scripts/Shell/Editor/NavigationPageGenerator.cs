using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    [CreateAssetMenu(fileName =nameof(NavigationPageGenerator), menuName = "Bs.Shell/Navigation/"+ nameof(NavigationPageGenerator))]
    public class NavigationPageGenerator : ScriptableObject
    {
        string outputPath = "NavigationPages";

        public void Generate()
        {
            CreateAllPagesFromControllersResources();
        }

        private void CreateAllPagesFromControllersResources()
        {
            var currentDirectory = GetPathOfParent();
            var directories = Directory.GetDirectories(currentDirectory);
            for (int i = 0; i < directories.Length; i++)
            {
                var directory = directories[i];
                var objects = LoadAllObjectsFromPath(directory);
                //var clones = CloneObjects(objects); //  Clone so we prevent crazy overwriting data issues.
                var controllers = objects.Cast<ControllerData>().ToArray();
                var navPage = CreateNavigationPage(controllers);
                AssetDatabase.CreateAsset(navPage, currentDirectory+outputPath);
            }
        }

        private List<Object> LoadAllObjectsFromPath(string path)
        {
            var controllersFromPath = Resources.LoadAll(path).ToList();
            return controllersFromPath;
        }

        private NavigationPage CreateNavigationPage(ControllerData[] controllers)
        {
            var navPage = NavigationPage.Create(controllers);
            return navPage;
        }

        protected string GetPathOfParent()
        {
            string thisPath = AssetDatabase.GetAssetPath(this);
            string parentPath = Directory.GetParent(thisPath).FullName;
            return parentPath;
        }
    }
}