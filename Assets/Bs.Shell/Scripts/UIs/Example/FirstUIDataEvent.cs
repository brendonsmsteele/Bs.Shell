using Bs.Shell.ScriptableObjects;
using UnityEngine;

namespace Bs.Shell.UI
{
    [CreateAssetMenu(fileName = "FirstUIDataEvent", menuName = "Bs.Shell/UIDataEvent/FirstUIDataEvent")]
    public class FirstUIDataEvent : UIDataEvent<FirstUIData>, IUIDataEvent { }
}