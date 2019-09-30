using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    [ExecuteInEditMode]
    [CreateAssetMenu(menuName ="Create/" + nameof(NavigationHashNameMapBuilder), fileName = nameof(NavigationHashNameMapBuilder))]
    public class NavigationHashNameMapBuilder : ScriptableObject
    {
        [SerializeField] GameObject navigationMapAnimator;
        AnimatorController animatorController
        {
            get
            {
                return navigationMapAnimator.GetComponent<Animator>().runtimeAnimatorController as AnimatorController;
            }
        }

        bool _isPlayingOrWillChangePlaymode = false;
        bool isPlayingOrWillChangePlaymode
        {
            set
            {
                if(_isPlayingOrWillChangePlaymode != value)
                {
                    _isPlayingOrWillChangePlaymode = value;
                    if (value)
                        BuildHashNameMap();
                }
            }
        }

        public void OnDisable()
        {
            isPlayingOrWillChangePlaymode = EditorApplication.isPlayingOrWillChangePlaymode;
        }

        public void BuildHashNameMap()
        {
            Debug.Log("BuildHashNameMap");
            NavigationHashName.Map = new Dictionary<int, string>();
            ProcessSingleLayer(animatorController.layers[0]);
            PrintHashNameMap();
        }

        private void ProcessSingleLayer(AnimatorControllerLayer layer)
        {
            for (int i = 0; i < layer.stateMachine.anyStateTransitions.Length; i++)
            {
                var stateTransition = layer.stateMachine.anyStateTransitions[i];
                ProcessAnimatorStateTransitions(stateTransition);
            }
        }

        private void ProcessAnimatorStateTransitions(AnimatorStateTransition animatorStateTransition)
        {
            NavigationHashName.Map.Add(animatorStateTransition.GetHashCode(), animatorStateTransition.name);
        }

        private void PrintHashNameMap()
        {
            foreach (KeyValuePair<int, string> kvp in NavigationHashName.Map)
            {
                PrintKeyValuePair(kvp);
            }
        }

        private void PrintKeyValuePair(KeyValuePair<int, string> kvp)
        {
            var p = $"Hash equals {kvp.Key} : Name equals {kvp.Value}";
            Debug.Log(p);
        }
    }
}
