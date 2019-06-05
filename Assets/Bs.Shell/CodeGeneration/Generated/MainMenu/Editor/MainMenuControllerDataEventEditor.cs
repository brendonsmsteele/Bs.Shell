using UnityEditor;
using UnityEngine;

namespace Bs.Shell.MainMenu
{
    [CustomEditor(typeof(MainMenuControllerDataEvent))]
    public class MainMenuControllerDataEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            MainMenuControllerDataEvent myTarget = (MainMenuControllerDataEvent)target;

            if (Application.isPlaying && myTarget.fakeData != null)
            {
                if (GUILayout.Button("Raise"))
                    myTarget.Raise(myTarget.fakeData);
            }
        }
    }
}