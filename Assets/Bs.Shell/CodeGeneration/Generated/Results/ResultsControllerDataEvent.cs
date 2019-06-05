using Bs.Shell.EditorVariables;
using UnityEngine;

namespace Bs.Shell.Results
{
    [CreateAssetMenu(fileName = nameof(ResultsControllerDataEvent), menuName = "Bs.Shell/Controllers/" + nameof(ResultsControllerDataEvent))]
    public class ResultsControllerDataEvent : ControllerDataEvent<ResultsControllerData>, IControllerDataEvent { }
}