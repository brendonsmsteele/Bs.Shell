using Nc.Shell.Events;
using UnityEngine;

namespace Nc.Shell.Examples
{
    public class Item : MonoBehaviour
    {
        public InterpolateReference interpolateReference;

        float? _interpolateValue;
        float? interpolateValue
        {
            get
            {
                return _interpolateValue;
            }
            set
            {
                if(_interpolateValue != value)
                {
                    _interpolateValue = value;
                    Debug.Log(_interpolateValue);
                }
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (interpolateReference != null)
            {
                interpolateValue = interpolateReference.Value;
            }
        }

        public void Bind(InterpolateReference target)
        {
            interpolateReference = target;
        }
    }
}
