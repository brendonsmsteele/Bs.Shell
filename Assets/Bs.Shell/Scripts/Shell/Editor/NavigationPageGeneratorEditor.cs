using UnityEditor;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    [CustomEditor(typeof(NavigationPageGenerator), true)]
    public class NavigationPageGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            NavigationPageGenerator myTarget = (NavigationPageGenerator)target;

            if (GUILayout.Button("Generate"))
                myTarget.Generate();

            DrawDefaultInspector();
        }
    }
}