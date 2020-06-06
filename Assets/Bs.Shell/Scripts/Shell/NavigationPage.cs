using System.Collections.Generic;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    /// <summary>
    /// Displays every possible controller model.
    /// Controls models out/in always news new objects to prevent data destruction.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(NavigationPage), menuName = Menu.Paths.NAVIGATION + nameof(NavigationPage))]
    public class NavigationPage : ScriptableObject
    {
        public static NavigationPage Create(params Model[] activeModels)
        {
            var page = ScriptableObject.CreateInstance<NavigationPage>();
            for (int i = 0; i < activeModels.Length; i++)
                page.SetModel(activeModels[i]);
            return page;
        }

        public List<Model> ActiveControllers
        {
            get
            {
                var includes = IncludeActiveControllers();
                if (includes.Count == 0)
                    Debug.LogWarning($"NavigationPage {this.name} needs to include 1 or more controller model.");
                return includes;
            }
        }

        #region Sugar

        private List<Model> IncludeActiveControllers()
        {
            var models = new List<Model>();
            return models;
        }

        private void SetModel(Model model)
        {
        }

        #endregion
    }
}