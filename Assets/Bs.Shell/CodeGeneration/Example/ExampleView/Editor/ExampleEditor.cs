using UnityEditor;

namespace Bs.Shell.Views
{
    [CustomEditor(typeof(ExampleView))]
    public class ExampleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ExampleView myScript = (ExampleView)target;
            //if (GUILayout.Button("Build Object"))
            //{
            //    myScript.Foo();
            //}
        }
    }
}