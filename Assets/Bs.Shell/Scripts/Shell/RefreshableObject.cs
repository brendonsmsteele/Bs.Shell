using UnityEngine;

namespace Bs.Shell
{
    /// <summary>
    /// This class exists to make shared editor scripts.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class RefreshableObject : MonoBehaviour, IRefreshable/*, IDirtyable*/
    {
        /// <summary>
        /// Overrided Refresh() and do whatever you want with the data.
        /// </summary>
        public virtual void Refresh()
        {

        }
    }
}
