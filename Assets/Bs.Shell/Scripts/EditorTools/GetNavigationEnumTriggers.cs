using UnityEngine;

namespace Nc.Shell.Navigation
{
    [CreateAssetMenu(menuName = nameof(Shell) + "/" + nameof(GetNavigationEnumTriggers), fileName = nameof(GetNavigationEnumTriggers))]
    public class GetNavigationEnumTriggers : GetEnumTriggers<NavigationTriggers>
    {
    }
}
