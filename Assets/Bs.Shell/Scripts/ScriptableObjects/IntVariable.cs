using UnityEngine;

namespace Bs.Shell.ScriptableObjects
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Bs.Shell/ScriptableObjects/IntVariable")]
    public class IntVariable : ScriptableObject
    {
        public int Value;
    }
}

