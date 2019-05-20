using UnityEditor;
using UnityEngine;

namespace Bs.Shell.CodeGeneration
{
    [CustomEditor(typeof(GenerateDBV))]
    public class GenerateDBVEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            IGenerate myTarget = (IGenerate)target;

            if (GUILayout.Button("Generate"))
                myTarget.Generate();
        }
    }
}