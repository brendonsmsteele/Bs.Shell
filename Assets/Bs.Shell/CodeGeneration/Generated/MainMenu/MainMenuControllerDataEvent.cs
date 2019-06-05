using Bs.Shell.EditorVariables;
using UnityEngine;

namespace Bs.Shell.MainMenu
{
    [CreateAssetMenu(fileName = nameof(MainMenuControllerDataEvent), menuName = "Bs.Shell/Controllers/" + nameof(MainMenuControllerDataEvent))]
    public class MainMenuControllerDataEvent : ControllerDataEvent<MainMenuControllerData>, IControllerDataEvent { }
}