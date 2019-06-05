using UnityEngine;

namespace Bs.Shell.Game
{
    [CreateAssetMenu(fileName = nameof(GameControllerData), menuName = "Bs.Shell/Controllers/" + nameof(GameControllerData))]
    public class GameControllerData : ControllerData
    {
        public static GameControllerData Create()
        {
            var data = ScriptableObject.CreateInstance<GameControllerData>();
            return data;
        }

        //  If same type.
        public override bool Equals(object other)
        {
            return other is GameControllerData;
        }
    }
}