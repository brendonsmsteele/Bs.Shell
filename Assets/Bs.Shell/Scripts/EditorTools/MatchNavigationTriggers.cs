using UnityEngine;

namespace Bs.Shell
{
    [CreateAssetMenu(menuName ="Bs.Shell/"+nameof(MatchNavigationTriggers), fileName =nameof(MatchNavigationTriggers))]
    public class MatchNavigationTriggers : MatchTriggers<NavigationTriggers>
    {
    }
}
