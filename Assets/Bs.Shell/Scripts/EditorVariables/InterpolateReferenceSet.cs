using UnityEngine;

namespace Bs.Shell.EditorVariables
{
    [CreateAssetMenu(fileName = nameof(InterpolateReferenceSet), menuName = "Bs.Shell/EditorVariables/" + nameof(InterpolateReference))]
    public class InterpolateReferenceSet : RuntimeSet<InterpolateReference> { }
}
