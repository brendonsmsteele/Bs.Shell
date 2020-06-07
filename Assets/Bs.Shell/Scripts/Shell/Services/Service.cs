using Nc.Shell.UI;
using UnityEngine;

namespace Nc.Shell.Services
{
    public abstract class Service : ScriptableObject, IInit
    {
        public abstract void Init();
    }
}
