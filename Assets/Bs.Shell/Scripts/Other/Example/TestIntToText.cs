using Bs.Shell.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Bs.Shell.Examples
{
    [RequireComponent(typeof(Text))]
    public class TestIntToText : MonoBehaviour
    {
        public IntReference target;
        public string suffix;
        int _value = -1;
        int value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    text.text = suffix == null ? _value.ToString() : _value.ToString() + suffix;    // Set text
                }
            }
        }
        Text text;

        // Use  for initialization
        void Start()
        {
            text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            value = target.Value;
        }
    }
}
