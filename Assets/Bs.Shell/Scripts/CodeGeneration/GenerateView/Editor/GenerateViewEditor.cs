using UnityEditor;
using UnityEngine;

namespace Bs.Shell.CodeGeneration
{
    [CustomEditor(typeof(GenerateView))]
    public class GenerateViewEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GenerateView myTarget = (GenerateView)target;

            if (GUILayout.Button("Generate"))
                myTarget.Generate();
        }
    }
}