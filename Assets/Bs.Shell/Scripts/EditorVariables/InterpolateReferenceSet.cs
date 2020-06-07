using UnityEngine;

namespace Nc.Shell.Events
{
    [CreateAssetMenu(fileName = nameof(InterpolateReferenceSet), menuName = "Nc.Shell/EditorVariables/" + nameof(InterpolateReference))]
    public class InterpolateReferenceSet : RuntimeSet<InterpolateReference> { }
}
