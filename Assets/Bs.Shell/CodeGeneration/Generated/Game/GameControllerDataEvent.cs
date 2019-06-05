using Bs.Shell.EditorVariables;
using UnityEngine;

namespace Bs.Shell.Game
{
    [CreateAssetMenu(fileName = nameof(GameControllerDataEvent), menuName = "Bs.Shell/Controllers/" + nameof(GameControllerDataEvent))]
    public class GameControllerDataEvent : ControllerDataEvent<GameControllerData>, IControllerDataEvent { }
}