using UnityEngine;

namespace Nc.Shell
{
    /// <summary>
    /// This class exists to make shared editor scripts.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public abstract class RefreshableObject : MonoBehaviour, IRefreshable
    {
        /// <summary>
        /// Overrided Refresh() and do whatever you want with the data.
        /// </summary>
        public abstract void Refresh();
    }
}
