using System;
using UnityEngine;

namespace Bs.Shell.Controllers
{
    public class BGController : ControllerBase<BGController.Model>
    {
        [Serializable]
        public class Model : Shell.Model
        {
            public string PathToBg;
            public Model(string PathToBg)
            {
                this.PathToBg = PathToBg;
            }
        }

        public override void Refresh()
        {
            Debug.Log(model.Message);
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
