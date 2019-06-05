using UnityEngine;

namespace Bs.Shell.Game
{
    public class GameController : ControllerBase<GameControllerData>
    {
        [SerializeField]
        private ShellServices services;

        public override void Bind(GameControllerData data)
        {
        }

        public override ManualYieldInstruction Dispose()
        {
            //  Game of how to delay dispose, for exit animations.
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
