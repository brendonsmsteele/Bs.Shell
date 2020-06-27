using System.Collections.Generic;
using UnityEngine;

namespace Nc.Shell.Navigation
{
    public class NavigateTo : StateMachineBehaviour
    {
        [SerializeField] List<SceneProvider> sceneControllerModels;
        [SerializeField] ShellServices shellServices;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var models = new List<SceneControllerModel>();
            sceneControllerModels.ForEach(x=>models.Add(x.GetModel()));
            var navigationPage = new NavigationPage(models);
            shellServices.NavigationMap.NavigateToPage(navigationPage);
        }
    }
}

