using UnityEditor;

namespace Nc.Shell.Events
{
    [CustomEditor(typeof(InterpolateVariable))]
    public class InterpolateVariableEditor : Editor
    {
        float value;

        public override void OnInspectorGUI()
        {
            InterpolateVariable myTarget = (InterpolateVariable)target;

            value = EditorGUILayout.Slider(value, 0f, 1f);
            myTarget.Value = value;
        }
    }
}
