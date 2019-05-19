using Bs.Shell.EditorVariables;
using UnityEngine;

namespace Bs.Shell.Examples
{
    [RequireComponent(typeof(Animation))]
    public class TestInterpolateAnimation : MonoBehaviour
    {

        public string clipName = "Take 001";
        public InterpolateReference target;
        float _value = -1f;
        public float value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;

                    AnimationState animState = animation[clipName];
                    animState.normalizedTime = _value;
                    animState.speed = 0f;
                    animation.Play(clipName);
                }
            }
        }
        Animation animation;

        void Start()
        {
            animation = GetComponent<Animation>();
        }

        // Update is called once per frame
        void Update()
        {
            value = target.Value;
        }
    }
}
