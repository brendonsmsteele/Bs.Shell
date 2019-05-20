using UnityEditor;
using UnityEngine;

namespace Bs.Shell.Hello
{
    [CustomEditor(typeof(HelloControllerDataEvent))]
    public class HelloControllerDataEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            HelloControllerDataEvent myTarget = (HelloControllerDataEvent)target;

            if (Application.isPlaying && myTarget.fakeData != null)
            {
                if (GUILayout.Button("Raise"))
                    myTarget.Raise(myTarget.fakeData);
            }
        }
    }
}