using UnityEngine;

namespace Bs.Shell.Hello
{
    [CreateAssetMenu(fileName = nameof(HelloControllerData), menuName = "Bs.Shell/Hello/" + nameof(HelloControllerData))]
    public class HelloControllerData : ControllerData
    {
        public static HelloControllerData Create()
        {
            var data = ScriptableObject.CreateInstance<HelloControllerData>();
            return data;
        }

        //  If same type.
        public override bool Equals(object other)
        {
            return other is HelloControllerData;
        }
    }
}