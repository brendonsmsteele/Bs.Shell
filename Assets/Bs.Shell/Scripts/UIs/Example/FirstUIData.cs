using UnityEngine;

namespace Bs.Shell.UI
{
    [CreateAssetMenu(fileName = "FirstUIData", menuName = "Bs.Shell/UIData/FirstUIData")]
    public class FirstUIData : UIData
    {
        public string message = "HelloWorld";
        public bool loadSecondUI = true;
    }
}