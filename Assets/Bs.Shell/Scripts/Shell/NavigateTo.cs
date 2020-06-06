using UnityEngine;

namespace Bs.Shell.Navigation
{
    public class NavigateTo : StateMachineBehaviour
    {
        [SerializeField] NavigationPage page;
        [SerializeField] ShellServices shellServices;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (page != null)
                shellServices.NavigationMap.NavigateToPage(page);
            else
                Debug.LogWarning($"No Navigation Page assigned for {stateInfo.fullPathHash} in {animator.gameObject.name} animator.");
        }
    }
}

