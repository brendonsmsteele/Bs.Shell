using UnityEngine;

namespace Bs.Shell.Navigation
{
    [RequireComponent(typeof(Animator))]
    public class NavigationMapTree : MonoBehaviour
    {
        public Attached<Animator> Animator;

        Attached<AnimatorStateChangedBroadcaster> animatorStateChangedBroadcaster;

        public delegate void OnStateChangedDelegate(string clipName);
        public event OnStateChangedDelegate OnStateChanged;

        private void OnEnable()
        {
            animatorStateChangedBroadcaster.Value.OnStateChanged += AnimatorStateChangedBroadcaster_OnStateChanged;
        }

        private void OnDisable()
        {
            animatorStateChangedBroadcaster.Value.OnStateChanged -= AnimatorStateChangedBroadcaster_OnStateChanged;
        }

        private void AnimatorStateChangedBroadcaster_OnStateChanged(int animatorStateHash)
        {
            var clipName = NavigationHashName.Map[animatorStateHash];
            OnStateChanged?.Invoke(clipName);
        }
    }
}

