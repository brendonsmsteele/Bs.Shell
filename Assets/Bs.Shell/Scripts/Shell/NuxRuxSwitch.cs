using UnityEngine;

namespace Bs.Shell.Navigation
{
    public class NuxRuxSwitch : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(true)
                ShellServices.Instance.NavigationMap.Navigate(NavigationTriggers.Nux);
            else
                ShellServices.Instance.NavigationMap.Navigate(NavigationTriggers.Rux);
        }
    }
}

