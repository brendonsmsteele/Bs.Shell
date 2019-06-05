using UnityEditor;
using UnityEngine;

namespace Bs.Shell.BG
{
    [CustomEditor(typeof(BGControllerDataEvent))]
    public class BGControllerDataEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            BGControllerDataEvent myTarget = (BGControllerDataEvent)target;

            if (Application.isPlaying && myTarget.fakeData != null)
            {
                if (GUILayout.Button("Raise"))
                    myTarget.Raise(myTarget.fakeData);
            }
        }
    }
}