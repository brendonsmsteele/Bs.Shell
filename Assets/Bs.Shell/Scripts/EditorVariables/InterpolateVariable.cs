using UnityEngine;

namespace Nc.Shell.Events
{
    [CreateAssetMenu(fileName = nameof(InterpolateVariable), menuName = "Nc.Shell/EditorVariables/"+ nameof(InterpolateVariable))]
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

