using UnityEngine;

namespace Nc.Shell.Events
{
    [CreateAssetMenu(fileName = nameof(IntVariable), menuName = "Nc.Shell/EditorVariables/" + nameof(IntVariable))]
    public class IntVariable : ScriptableObject
    {
        public int Value;
    }
}

