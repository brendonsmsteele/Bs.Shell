using UnityEngine;

namespace Bs.Shell
{
    public abstract class Service : ScriptableObject, IInit
    {
        public abstract void Init();
    }
}
