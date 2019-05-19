using UnityEngine;

namespace Bs.Shell.EditorVariables
{
    [CreateAssetMenu(fileName = nameof(IntVariable), menuName = "Bs.Shell/EditorVariables/" + nameof(IntVariable))]
    public class IntVariable : ScriptableObject
    {
        public int Value;
    }
}

