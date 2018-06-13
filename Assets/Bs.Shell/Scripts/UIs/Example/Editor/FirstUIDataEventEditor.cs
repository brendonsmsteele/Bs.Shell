using UnityEditor;
using UnityEngine;

namespace Bs.Shell.UI
{
    [CustomEditor(typeof(FirstUIDataEvent))]
    public class FirstUIDataEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            FirstUIDataEvent myTarget = (FirstUIDataEvent)target;

            if (Application.isPlaying && myTarget.fakeData != null)
            {
                if (GUILayout.Button("Raise"))
                    myTarget.Raise(myTarget.fakeData);
            }
        }
    }
}