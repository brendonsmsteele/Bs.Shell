using UnityEngine;

namespace Bs.Shell.MainMenu
{
    [CreateAssetMenu(fileName = nameof(MainMenuControllerData), menuName = "Bs.Shell/Controllers/" + nameof(MainMenuControllerData))]
    public class MainMenuControllerData : ControllerData
    {
        public string Message;
        public Sprite Sprite;

        public static MainMenuControllerData Create()
        {
            var data = ScriptableObject.CreateInstance<MainMenuControllerData>();
            return data;
        }

        //  If same type.
        public override bool Equals(object other)
        {
            return other is MainMenuControllerData;
        }
    }
}