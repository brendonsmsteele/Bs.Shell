using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

namespace Bs.Shell
{
    [CreateAssetMenu(menuName = nameof(Bs.Shell) + "/" + nameof(GetAnimatorTriggers), fileName = nameof(GetAnimatorTriggers))]
    public class GetAnimatorTriggers : GetTriggers
    {
        [SerializeField] AnimatorController animatorController;

        public override string[] Triggers()
        {
            return animatorController.parameters.Select(x => x.name).ToArray();
        }
    }
}
