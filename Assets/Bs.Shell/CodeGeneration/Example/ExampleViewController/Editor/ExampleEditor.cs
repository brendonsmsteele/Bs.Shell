using UnityEditor;

namespace Nc.Shell.UI
{
    [CustomEditor(typeof(ExampleViewController))]
    public class ExampleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ExampleViewController myScript = (ExampleViewController)target;
            //if (GUILayout.Button("Build Object"))
            //{
            //    myScript.Foo();
            //}
        }
    }
}