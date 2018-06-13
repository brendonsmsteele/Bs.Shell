using UnityEditor;
using UnityEngine;

namespace Bs.Shell.UI
{
    [CustomEditor(typeof(SecondUIDataEvent))]
    public class SecondUIDataEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            SecondUIDataEvent myTarget = (SecondUIDataEvent)target;

            if (Application.isPlaying && myTarget.fakeData != null)
            {
                if (GUILayout.Button("Raise"))
                    myTarget.Raise(myTarget.fakeData);
            }
        }
    }
}