using Bs.Shell.ScriptableObjects;
using UnityEngine;

namespace Bs.Shell.UI
{
    [CreateAssetMenu(fileName = "ItemUIDataEvent", menuName = "Bs.Shell/UIDataEvent/ItemUIDataEvent")]
    public class ItemUIDataEvent : UIDataEvent<ItemUIData>, IUIDataEvent { }
}