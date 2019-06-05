using Bs.Shell.EditorVariables;
using UnityEngine;

namespace Bs.Shell.ArtGallery
{
    [CreateAssetMenu(fileName = nameof(ArtGalleryControllerDataEvent), menuName = "Bs.Shell/Controllers/" + nameof(ArtGalleryControllerDataEvent))]
    public class ArtGalleryControllerDataEvent : ControllerDataEvent<ArtGalleryControllerData>, IControllerDataEvent { }
}