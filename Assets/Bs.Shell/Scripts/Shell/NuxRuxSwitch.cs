using UnityEngine;

namespace Bs.Shell.Navigation
{
    public class NuxRuxSwitch : StateMachineBehaviour
    {
        [SerializeField] ShellServices shellServices;
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(true)
                shellServices.NavigationMap.Navigate(NavigationTriggers.Nux);
            else
                shellServices.NavigationMap.Navigate(NavigationTriggers.Rux);
        }
    }
}

