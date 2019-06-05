using UnityEngine;

namespace Bs.Shell.BG
{
    [CreateAssetMenu(fileName = nameof(BGControllerData), menuName = "Bs.Shell/Controllers/" + nameof(BGControllerData))]
    public class BGControllerData : ControllerData
    {
        public static BGControllerData Create()
        {
            var data = ScriptableObject.CreateInstance<BGControllerData>();
            return data;
        }

        //  If same type.
        public override bool Equals(object other)
        {
            return other is BGControllerData;
        }
    }
}