using UnityEditor;
using UnityEngine;

namespace Bs.Shell.UI
{
    [CustomEditor(typeof(ItemUIDataEvent))]
    public class ItemUIDataEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ItemUIDataEvent myTarget = (ItemUIDataEvent)target;

            if (Application.isPlaying && myTarget.fakeData != null)
            {
                if (GUILayout.Button("Raise"))
                    myTarget.Raise(myTarget.fakeData);
            }
        }
    }
}