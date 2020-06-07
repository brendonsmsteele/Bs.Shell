using UnityEngine;

namespace Nc.Shell.Events
{
    [CreateAssetMenu(fileName = nameof(FloatVariable), menuName = "Nc.Shell/EditorVariables/" + nameof(FloatVariable))]
    public class FloatVariable : ScriptableObject
    {
        public float Value;
    }
}

