using UnityEngine;

namespace Bs.Shell.Hello
{
    public class HelloController : ControllerBase<HelloControllerData>
    {
        [SerializeField]
        private ShellServices services;

        public override void Bind(HelloControllerData data)
        {
        }

        public override ManualYieldInstruction Dispose()
        {
            //  Hello of how to delay dispose, for exit animations.
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
