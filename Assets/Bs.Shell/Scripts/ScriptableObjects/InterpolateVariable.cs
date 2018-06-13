using UnityEngine;

namespace Bs.Shell.ScriptableObjects
{
    [CreateAssetMenu(fileName = "InterpolateVariable", menuName = "Bs.Shell/ScriptableObjects/InterpolateVariable")]
    public class InterpolateVariable : ScriptableObject
    {
        float value = 0f;
        public float Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Mathf.Clamp01(value);
            }
        }
    }
}

