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
    }
}

