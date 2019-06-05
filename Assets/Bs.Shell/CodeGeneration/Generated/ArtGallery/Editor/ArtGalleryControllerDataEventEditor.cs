using UnityEditor;
using UnityEngine;

namespace Bs.Shell.ArtGallery
{
    [CustomEditor(typeof(ArtGalleryControllerDataEvent))]
    public class ArtGalleryControllerDataEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ArtGalleryControllerDataEvent myTarget = (ArtGalleryControllerDataEvent)target;

            if (Application.isPlaying && myTarget.fakeData != null)
            {
                if (GUILayout.Button("Raise"))
                    myTarget.Raise(myTarget.fakeData);
            }
        }
    }
}