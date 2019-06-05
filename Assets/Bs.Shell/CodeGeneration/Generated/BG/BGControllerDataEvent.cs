using Bs.Shell.EditorVariables;
using UnityEngine;

namespace Bs.Shell.BG
{
    [CreateAssetMenu(fileName = nameof(BGControllerDataEvent), menuName = "Bs.Shell/Controllers/" + nameof(BGControllerDataEvent))]
    public class BGControllerDataEvent : ControllerDataEvent<BGControllerData>, IControllerDataEvent { }
}