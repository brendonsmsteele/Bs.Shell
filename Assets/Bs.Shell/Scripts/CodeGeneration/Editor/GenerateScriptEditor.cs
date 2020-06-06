using UnityEditor;
using UnityEngine;

namespace Bs.Shell.CodeGeneration
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