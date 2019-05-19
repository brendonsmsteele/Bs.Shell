using UnityEngine;

namespace Bs.Shell.Example
{
    public class ExampleController : ControllerBase<ExampleControllerData>
    {
        [SerializeField]
        private ExampleServices services;

        public override void Bind(ExampleControllerData data)
        {
        }

        public override ManualYieldInstruction Dispose()
        {
            //  Example of how to delay dispose, for exit animations.
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
