using UnityEngine;

namespace Bs.Shell
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorStateChangedBroadcaster : MonoBehaviour
    {
        public delegate void OnStateChangedDelegate(string clipName);
        public event OnStateChangedDelegate OnStateChanged;

        Animator _animator;
        Animator animator
        {
            get
            {
                if (_animator == null)
                    _animator = GetComponent<Animator>();
                return _animator;
            }
        }

        string _currentClip;
        string currentClip
        {
            set
            {
                if(_currentClip != value)
                {
                    _currentClip = value;
                    OnStateChanged?.Invoke(_currentClip);
                }
            }
        }

        void Update()
        {
            currentClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }
    }
}