using UnityEngine;

namespace Nc.Shell
{
    public abstract class GetTriggers : ScriptableObject, IGetTriggers
    {
        public abstract string[] Triggers();
    }
}

