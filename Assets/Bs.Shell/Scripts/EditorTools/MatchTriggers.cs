using System;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

namespace Bs.Shell
{
    public abstract class MatchTriggers<TEnum> : ScriptableObject, IMatchTriggers
    {
        [SerializeField] AnimatorController animatorController;

        bool _match;
        public bool Match { get { return _match; } }

        protected void OnEnable()
        {
            TriggersMatch();
        }

        public bool TriggersMatch()
        {
            var names = Enum.GetNames(typeof(TEnum));
            var parameters = animatorController.parameters;
            var parameterNames = parameters.Select(x => x.name).ToArray();
            _match = names.SequenceEqual(parameterNames);
            return _match;
        }

        public void SetTriggers()
        {
            var names = Enum.GetNames(typeof(TEnum));
            var parameters = CreateParameters(names);
            animatorController.parameters = parameters;
            _match = true;
        }

        private AnimatorControllerParameter[] CreateParameters(string[] names)
        {
            var acp_array = new AnimatorControllerParameter[names.Length];
            for (int i = 0; i < names.Length; i++)
                acp_array[i] = CreateTriggerParameter(names[i]);
            return acp_array;
        }

        private AnimatorControllerParameter CreateTriggerParameter(string name)
        {
            var acp = new AnimatorControllerParameter();
            acp.name = name;
            acp.type = AnimatorControllerParameterType.Trigger;
            return acp;
        }
    }
}

