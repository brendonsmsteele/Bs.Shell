using UnityEngine;

namespace Bs.Shell.Navigation
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AnimatorStateChangedBroadcaster))]
    public class NavigationMapTree : MonoBehaviour
    {
        public Animator Animator;
        [SerializeField] AnimatorStateChangedBroadcaster animatorStateChangedBroadcaster;

        public delegate void OnStateChangedDelegate(string clipName);
        public event OnStateChangedDelegate OnStateChanged;

        private void OnEnable()
        {
            animatorStateChangedBroadcaster.OnStateChanged += AnimatorStateChangedBroadcaster_OnStateChanged;
        }

        private void OnDisable()
        {
            animatorStateChangedBroadcaster.OnStateChanged -= AnimatorStateChangedBroadcaster_OnStateChanged;
        }

        private void AnimatorStateChangedBroadcaster_OnStateChanged(int animatorStateHash)
        {
            if (NavigationHashName.Map == null)
            {
                Debug.Log("NavigationHashName.Map not yet created.");
                return;
            }
            var clipName = NavigationHashName.Map[animatorStateHash];
            OnStateChanged?.Invoke(clipName);
        }
    }
}