using UnityEditor;
using UnityEngine;

namespace Bs.Shell.CodeGeneration
{
    [CustomEditor(typeof(GenerateScriptWithNewNamespace))]
    public class GenerateViewEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GenerateScriptWithNewNamespace myTarget = (GenerateScriptWithNewNamespace)target;

            if (GUILayout.Button("Generate"))
                myTarget.Generate();
        }
    }
}