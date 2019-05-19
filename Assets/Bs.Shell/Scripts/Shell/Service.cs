using UnityEngine;

namespace Bs.Shell
{
    public abstract class Service : ScriptableObject, IInit
    {
        public void Init()
        {
            throw new System.NotImplementedException();
        }
    }
}
