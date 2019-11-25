using UnityEditor;
using UnityEngine;

namespace Bs.Shell
{
    [CustomEditor(typeof(MatchTriggers))]
    public class MatchNavigationTriggersEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MatchTriggers myScript = (MatchTriggers)target;

            if (myScript.Match)
            {
                GUILayout.Label("CLEAN MATCH");
            }
            else
            {
                GUILayout.Label("MISMATCH DETECTED PLEASE CORRECT");
                if (GUILayout.Button("Fix"))
                {
                    myScript.SetTriggers();
                }
            }
        }
    }
}