using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerateCode))]
public class GenerateCodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GenerateCode myTarget = (GenerateCode)target;

        if (GUILayout.Button("GenerateUI"))
            myTarget.GenerateUIScript();

        if (GUILayout.Button("GenerateButtonEditor"))
            myTarget.GenerateButtonEditorScript();
    }
}
