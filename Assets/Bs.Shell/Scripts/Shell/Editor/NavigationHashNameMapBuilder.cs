#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    [InitializeOnLoad]
    public static class NavigationHashNameMapBuilder
    {
        static NavigationHashNameMapBuilder()
        {
            EditorApplication.playModeStateChanged += LoadDefaultScene;
        }

        static void LoadDefaultScene(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                BuildHashNameMap();
            }
        }

        static void BuildHashNameMap()
        {
            Debug.Log("BuildHashNameMap");
            var NavigationMapAnimator = Resources.Load("NavigationMapAnimator") as GameObject;
            var animatorController = NavigationMapAnimator.GetComponent<Animator>().runtimeAnimatorController as AnimatorController;
            NavigationHashName.Map = new Dictionary<int, string>();
            Process(animatorController.layers[0]);
            PrintHashNameMap();
        }

        static void Process(AnimatorControllerLayer layer)
        {
            Process(layer.stateMachine);
        }

        static void Process(AnimatorStateMachine animatorStateMachine)
        {
            //  Add all states
            Process(animatorStateMachine.states);
            //  Recursive
            for (int i = 0; i < animatorStateMachine.stateMachines.Length; i++)
            {
                var childAnimatorStateMachine = animatorStateMachine.stateMachines[i];
                Process(childAnimatorStateMachine.stateMachine);
            }
        }

        static void Process(ChildAnimatorState[] childAnimatorStates)
        {
            for (int i = 0; i < childAnimatorStates.Length; i++)
            {
                var state = childAnimatorStates[i].state;
                Process(state);
            }
        }

        static void Process(AnimatorState animatorState)
        {
            NavigationHashName.Map.Add(animatorState.GetHashCode(), animatorState.name);
        }

        static void PrintHashNameMap()
        {
            foreach (KeyValuePair<int, string> kvp in NavigationHashName.Map)
            {
                PrintKeyValuePair(kvp);
            }
        }

        static void PrintKeyValuePair(KeyValuePair<int, string> kvp)
        {
            var p = $"Hash equals {kvp.Key} : Name equals {kvp.Value}";
            Debug.Log(p);
        }
    }
}
#endif