using UnityEditor;
using UnityEngine;

namespace Bs.Shell.Game
{
    [CustomEditor(typeof(GameControllerDataEvent))]
    public class GameControllerDataEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GameControllerDataEvent myTarget = (GameControllerDataEvent)target;

            if (Application.isPlaying && myTarget.fakeData != null)
            {
                if (GUILayout.Button("Raise"))
                    myTarget.Raise(myTarget.fakeData);
            }
        }
    }
}