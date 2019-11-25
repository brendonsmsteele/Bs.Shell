using UnityEditor;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    [CustomEditor(typeof(NavigationPage))]
    public class NavigationPageEditor : Editor
    {
        NavigationPage navigationPage;

        SerializedProperty mainMenuProp;
        SerializedProperty bgProp;
        SerializedProperty gameProp;
        SerializedProperty artGalleryProp;
        SerializedProperty resultsProp;

        public void OnEnable()
        {
            navigationPage = (NavigationPage)target;
        }

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();

            //Render(navigationPage.mainMenu);
            //Render(navigationPage.bG);
            //Render(navigationPage.game);
            //Render(navigationPage.artGallery);
            //Render(navigationPage.results);

            //mainMenu.Include = GUILayout.Toggle(mainMenu.Include, mainMenu.ControllerName);
            //if (mainMenu.Include)
            //{
            //    GUILayout.Label("includeMainMenu");
            //}

            //bG.Include = GUILayout.Toggle(bG.Include, bG.ControllerName);
            //if (bG.Include)
            //{
            //    GUILayout.Label("includeBG");
            //}

            //if (GUILayout.Button("Build Object"))
            //{
            //    myScript.Foo();
            //}
        }

        private void RenderSpace()
        {
            GUILayout.Space(10);
        }

        private void Render<T>(IncludeModel<T> includeModel)
            where T : Model
        {
            GUILayout.BeginVertical("box");
            includeModel.Include = GUILayout.Toggle(includeModel.Include, includeModel.ControllerName);
            if (includeModel.Include)
            {
                GUILayout.BeginVertical("box");
                includeModel.Render();
                EditorGUILayout.PropertyField(mainMenuProp, new GUIContent("main menu"));
                GUILayout.EndVertical();
            }
            RenderSpace();
            GUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }


}