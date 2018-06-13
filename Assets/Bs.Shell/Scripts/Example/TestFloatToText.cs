using Bs.Shell.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bs.Shell.Examples
{
    [RequireComponent(typeof(Text))]
    public class TestFloatToText : MonoBehaviour
    {

        public FloatReference target;
        public string suffix;
        public int numOfDecimalPlaces = 1;
        float _value = -1f;
        float value
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
                    float truncatedFloat = 0f;
                    if (numOfDecimalPlaces == 0)
                        truncatedFloat = (float)Math.Truncate(_value);
                    else
                        truncatedFloat = (float)(Math.Truncate(_value * 10f * numOfDecimalPlaces) / 10f * numOfDecimalPlaces);
                    string formattedTextString = "";
                    formattedTextString = truncatedFloat.ToString();
                    if (numOfDecimalPlaces > 0 && truncatedFloat % 1 == 0)
                        formattedTextString = formattedTextString + ".0";
                    if (suffix != null)
                        formattedTextString = formattedTextString + suffix;
                    text.text = formattedTextString;    // Set text
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