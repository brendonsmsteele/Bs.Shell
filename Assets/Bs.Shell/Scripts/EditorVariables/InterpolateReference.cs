using System;

namespace Nc.Shell.Events
{
    [Serializable]
    public class InterpolateReference
    {
        public bool UseConstant = true;
        public float ConstantValue;
        public InterpolateVariable Variable;

        public float Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }
    }
}
