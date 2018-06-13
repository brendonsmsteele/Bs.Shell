using Bs.Shell.ScriptableObjects;
using UnityEngine;

namespace Bs.Shell.UI
{
    [CreateAssetMenu(fileName = "SecondUIData", menuName = "Bs.Shell/UIData/SecondUIData")]
    public class SecondUIData : UIData
    {
        public string message = "HelloWorld";
        public GameObject prefab;
        public InterpolateReferenceSet interpolateRefSet;
    }
}