using Bs.Shell.ScriptableObjects;
using UnityEngine;

namespace Bs.Shell.UI
{
    [CreateAssetMenu(fileName = "SecondUIDataEvent", menuName = "Bs.Shell/UIDataEvent/SecondUIDataEvent")]
    public class SecondUIDataEvent : UIDataEvent<SecondUIData>, IUIDataEvent { }
}