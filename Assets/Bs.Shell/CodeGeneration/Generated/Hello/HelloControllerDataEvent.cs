using Bs.Shell.EditorVariables;
using UnityEngine;

namespace Bs.Shell.Hello
{
    [CreateAssetMenu(fileName = nameof(HelloControllerDataEvent), menuName = "Bs.Shell/Hello/" + nameof(HelloControllerDataEvent))]
    public class HelloControllerDataEvent : ControllerDataEvent<HelloControllerData>, IControllerDataEvent { }
}