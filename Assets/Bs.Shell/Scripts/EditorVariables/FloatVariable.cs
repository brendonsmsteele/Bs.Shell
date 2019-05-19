using UnityEngine;

namespace Bs.Shell.EditorVariables
{
    [CreateAssetMenu(fileName = nameof(FloatVariable), menuName = "Bs.Shell/EditorVariables/" + nameof(FloatVariable))]
    public class FloatVariable : ScriptableObject
    {
        public float Value;
    }
}

