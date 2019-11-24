using UnityEditor;
using UnityEngine;

namespace Bs.Shell
{
    [CustomEditor(typeof(MatchNavigationTriggers))]
    public class MatchNavigationTriggersEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MatchNavigationTriggers myScript = (MatchNavigationTriggers)target;

            if (myScript.Match)
            {
                GUILayout.Label("CLEAN MATCH");
            }
            else
            {
                GUILayout.Label("MISMATCH DETECTED PLEASE CORRECT");
                if (GUILayout.Button("Match"))
                {
                    myScript.SetTriggers();
                }
            }
        }
    }
}