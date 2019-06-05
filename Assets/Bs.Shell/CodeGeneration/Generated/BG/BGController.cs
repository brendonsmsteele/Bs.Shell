using UnityEngine;

namespace Bs.Shell.BG
{
    public class BGController : ControllerBase<BGControllerData>
    {
        [SerializeField]
        private ShellServices services;

        public override void Bind(BGControllerData data)
        {
        }

        public override ManualYieldInstruction Dispose()
        {
            //  BG of how to delay dispose, for exit animations.
            return GetTimedYield(1.2f);
            //return NavigationMapController.Instance.AllActiveControllersAreLoaded;
            return base.Dispose();
        }

        protected override void AddEventListeners()
        {
            RemoveEventListeners();
        }

        protected override void RemoveEventListeners()
        {
        }
    }
}
