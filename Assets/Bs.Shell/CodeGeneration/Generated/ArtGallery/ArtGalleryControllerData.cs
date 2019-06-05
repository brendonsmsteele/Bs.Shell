using UnityEngine;

namespace Bs.Shell.ArtGallery
{
    [CreateAssetMenu(fileName = nameof(ArtGalleryControllerData), menuName = "Bs.Shell/Controllers/" + nameof(ArtGalleryControllerData))]
    public class ArtGalleryControllerData : ControllerData
    {
        public static ArtGalleryControllerData Create()
        {
            var data = ScriptableObject.CreateInstance<ArtGalleryControllerData>();
            return data;
        }

        //  If same type.
        public override bool Equals(object other)
        {
            return other is ArtGalleryControllerData;
        }
    }
}