using UnityEngine;

namespace Bs.Shell.Navigation
{
    [RequireComponent(typeof(Animator))]
    public class NavigationMapTree : MonoBehaviour
    {
        Animator _animator;
        public Animator Animator
        {
            get
            {
                if (_animator == null)
                    _animator = GetComponent<Animator>();
                return _animator;
            }
        }

        [SerializeField] AnimatorStateChangedBroadcaster animatorStateChangedBroadcaster;

        public event AnimatorStateChangedBroadcaster.OnStateChangedDelegate OnStateChanged;

        private void OnEnable()
        {
            animatorStateChangedBroadcaster.OnStateChanged += AnimatorStateChangedBroadcaster_OnStateChanged;
        }

        private void OnDisable()
        {
            animatorStateChangedBroadcaster.OnStateChanged -= AnimatorStateChangedBroadcaster_OnStateChanged;
        }

        private void AnimatorStateChangedBroadcaster_OnStateChanged(string clipName)
        {
            OnStateChanged?.Invoke(clipName);
        }
    }
}

