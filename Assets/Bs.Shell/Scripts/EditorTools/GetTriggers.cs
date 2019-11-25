using UnityEngine;

namespace Bs.Shell
{
    public abstract class GetTriggers : ScriptableObject, IGetTriggers
    {
        public abstract string[] Triggers();
    }
}

