﻿using Nc.Shell.Events;
using UnityEngine;

namespace Nc.Shell.Examples
{
    public class TestInterpolateToFloatReference : MonoBehaviour
    {
        public InterpolateReference interpolation;
        public FloatReference min, max, value;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            value.Variable.Value = Mathf.Lerp(min.Value, max.Value, interpolation.Value);
        }
    }
}