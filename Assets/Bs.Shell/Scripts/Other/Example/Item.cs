using Bs.Shell.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Bs.Shell.Examples
{
    public class Item : MonoBehaviour
    {
        public InterpolateReference interpolateReference;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (interpolateReference != null)
                Debug.Log(interpolateReference);
        }

        public void Bind(InterpolateReference target)
        {
            interpolateReference = target;
        }
    }
}
