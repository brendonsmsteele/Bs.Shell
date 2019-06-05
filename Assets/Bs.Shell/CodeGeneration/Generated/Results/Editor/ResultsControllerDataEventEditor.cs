using UnityEditor;
using UnityEngine;

namespace Bs.Shell.Results
{
    [CustomEditor(typeof(ResultsControllerDataEvent))]
    public class ResultsControllerDataEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ResultsControllerDataEvent myTarget = (ResultsControllerDataEvent)target;

            if (Application.isPlaying && myTarget.fakeData != null)
            {
                if (GUILayout.Button("Raise"))
                    myTarget.Raise(myTarget.fakeData);
            }
        }
    }
}