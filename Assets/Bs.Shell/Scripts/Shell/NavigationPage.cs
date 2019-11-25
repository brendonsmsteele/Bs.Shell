using Bs.Shell.Controllers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    [CreateAssetMenu(fileName = nameof(NavigationPage), menuName = Menu.Paths.NAVIGATION + nameof(NavigationPage))]
    public class NavigationPage : ScriptableObject
    {
        public List<Model> ActiveControllers;

        public IncludeMainMenu mainMenu = new IncludeMainMenu();
        public IncludeBG bg = new IncludeBG();

        public void OnEnable()
        {
            
        }

        private void DeriveActiveControllers()
        {

        }

        public static NavigationPage Create(params Model[] activeControllers)
        {
            var page = ScriptableObject.CreateInstance<NavigationPage>();
            page.ActiveControllers = activeControllers.ToList();
            return page;
        }
    }
}