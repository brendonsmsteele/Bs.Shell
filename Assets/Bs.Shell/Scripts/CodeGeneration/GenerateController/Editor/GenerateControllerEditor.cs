using UnityEditor;
using UnityEngine;

namespace Bs.Shell.CodeGeneration
{
    [CustomEditor(typeof(GenerateController))]
    public class GenerateControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GenerateController myTarget = (GenerateController)target;

            if (GUILayout.Button("Generate"))
                myTarget.Generate();
        }
    }
}