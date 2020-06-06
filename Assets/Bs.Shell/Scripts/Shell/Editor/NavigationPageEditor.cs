using UnityEditor;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    [CustomEditor(typeof(NavigationPage))]
    public class NavigationPageEditor : Editor
    {
        NavigationPage navigationPage;

        public void OnEnable()
        {
            navigationPage = (NavigationPage)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }

        private void RenderSpace()
        {
            GUILayout.Space(10);
        }

        private void Render<T>(IncludeModel<T> includeModel, SerializedProperty serializedProp)
            where T : Model
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);
            includeModel.Include = GUILayout.Toggle(includeModel.Include, includeModel.ControllerName);
            if (includeModel.Include)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                //GUILayout.Label("Hello");
                //includeModel.Render();
                //CreateEditor(serializedProp.serializedObject.targetObject).OnInspectorGUI();

                //  Show my serialized Model please!
                GUILayout.EndVertical();
            }
            RenderSpace();
            GUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }
}