using UnityEngine;

namespace Bs.Shell.ArtGallery
{
    public class ArtGalleryController : ControllerBase<ArtGalleryControllerData>
    {
        [SerializeField]
        private ShellServices services;

        public override void Bind(ArtGalleryControllerData data)
        {
        }

        public override ManualYieldInstruction Dispose()
        {
            //  ArtGallery of how to delay dispose, for exit animations.
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
