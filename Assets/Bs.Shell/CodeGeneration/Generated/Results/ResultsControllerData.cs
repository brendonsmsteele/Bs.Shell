using UnityEngine;

namespace Bs.Shell.Results
{
    [CreateAssetMenu(fileName = nameof(ResultsControllerData), menuName = "Bs.Shell/Controllers/" + nameof(ResultsControllerData))]
    public class ResultsControllerData : ControllerData
    {
        public static ResultsControllerData Create()
        {
            var data = ScriptableObject.CreateInstance<ResultsControllerData>();
            return data;
        }

        //  If same type.
        public override bool Equals(object other)
        {
            return other is ResultsControllerData;
        }
    }
}