using UnityEditor;
using UnityEngine;

namespace Nc.Shell.CodeGeneration
{
    [CustomEditor(typeof(GenerateScript))]
    public class GenerateScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GenerateScript myTarget = (GenerateScript)target;

            if (GUILayout.Button("Generate"))
                myTarget.Generate();
        }
    }
}