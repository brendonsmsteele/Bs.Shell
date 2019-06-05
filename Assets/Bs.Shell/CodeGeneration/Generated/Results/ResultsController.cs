using UnityEngine;

namespace Bs.Shell.Results
{
    public class ResultsController : ControllerBase<ResultsControllerData>
    {
        [SerializeField]
        private ShellServices services;

        public override void Bind(ResultsControllerData data)
        {
        }

        public override ManualYieldInstruction Dispose()
        {
            //  Results of how to delay dispose, for exit animations.
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
