using UnityEngine;

namespace Bs.Shell.Example
{
    [CreateAssetMenu(fileName = nameof(ExampleControllerData), menuName = "Bs.Shell/Example/" + nameof(ExampleControllerData))]
    public class ExampleControllerData : ControllerData
    {
        public static ExampleControllerData Create()
        {
            var data = ScriptableObject.CreateInstance<ExampleControllerData>();
            return data;
        }

        //  If same type.
        public override bool Equals(object other)
        {
            return other is ExampleControllerData;
        }
    }
}