using System;

namespace Nc.Shell.Events
{
    [Serializable]
    public class IntReference
    {
        public bool UseConstant = true;
        public int ConstantValue;
        public IntVariable Variable;

        public int Value
        {
            get { return UseConstant || Variable == null ? ConstantValue : Variable.Value; }
        }
    }
}
