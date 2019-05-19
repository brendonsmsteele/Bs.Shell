using UnityEditor;
using UnityEngine;

namespace Bs.Shell
{
    [CustomEditor(typeof(RefreshableObject), true)]
    public class RefreshableObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RefreshableObject myTarget = (RefreshableObject)target;

            if (Application.isPlaying)
            {
                if (GUILayout.Button("Refresh"))
                    myTarget.Refresh();
            }

            DrawDefaultInspector();
        }
    }
}