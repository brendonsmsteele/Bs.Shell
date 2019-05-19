using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    [CreateAssetMenu(fileName = nameof(NavigationPage), menuName = "Bs.Shell/Navigation/" + nameof(NavigationPage))]
    public class NavigationPage : ScriptableObject
    {
        public List<ControllerData> ActiveControllers;

        public static NavigationPage Create(params ControllerData[] activeControllers)
        {
            var page = ScriptableObject.CreateInstance<NavigationPage>();
            page.ActiveControllers = activeControllers.ToList();
            return page;
        }
    }
}