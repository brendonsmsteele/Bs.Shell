using UnityEngine;

namespace Bs.Shell
{
    [CreateAssetMenu(menuName = nameof(Bs.Shell) + "/" + nameof(GetNavigationEnumTriggers), fileName = nameof(GetNavigationEnumTriggers))]
    public class GetNavigationEnumTriggers : GetEnumTriggers<NavigationTriggers>
    {
    }
}
