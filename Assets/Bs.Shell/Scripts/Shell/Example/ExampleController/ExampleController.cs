using System;
using UnityEngine;

namespace Bs.Shell.Controllers
{
    public class ExampleController : ControllerBase<ExampleController.Model>
    {
        [Serializable]
        public class Model : Shell.Model
        {
            public Model()
            {
            }
        }

        public override void Refresh()
        {
            Debug.Log(model.Message);
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
