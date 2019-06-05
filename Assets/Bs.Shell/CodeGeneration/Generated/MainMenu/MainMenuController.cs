using UnityEngine;

namespace Bs.Shell.MainMenu
{
    public class MainMenuController : ControllerBase<MainMenuControllerData>
    {
        [SerializeField]
        private ShellServices services;

        public override void Bind(MainMenuControllerData data)
        {
        }

        public override ManualYieldInstruction Dispose()
        {
            //  MainMenu of how to delay dispose, for exit animations.
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
