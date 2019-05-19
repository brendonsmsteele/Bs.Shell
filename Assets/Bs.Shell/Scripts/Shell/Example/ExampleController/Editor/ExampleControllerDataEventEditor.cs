using UnityEditor;
using UnityEngine;

namespace Bs.Shell.Example
{
    [CustomEditor(typeof(ExampleControllerDataEvent))]
    public class ExampleControllerDataEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ExampleControllerDataEvent myTarget = (ExampleControllerDataEvent)target;

            if (Application.isPlaying && myTarget.fakeData != null)
            {
                if (GUILayout.Button("Raise"))
                    myTarget.Raise(myTarget.fakeData);
            }
        }
    }
}