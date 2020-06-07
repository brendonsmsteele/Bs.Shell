#if UNITY_EDITOR
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

namespace Nc.Shell
{
    [CreateAssetMenu(menuName = nameof(Nc.Shell) + "/" + nameof(GetAnimatorTriggers), fileName = nameof(GetAnimatorTriggers))]
    public class GetAnimatorTriggers : GetTriggers
    {
        [SerializeField] AnimatorController animatorController;

        public override string[] Triggers()
        {
            return animatorController.parameters.Select(x => x.name).ToArray();
        }
    }
}
#endif