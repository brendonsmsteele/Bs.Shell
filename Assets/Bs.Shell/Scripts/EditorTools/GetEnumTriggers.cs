using System;

namespace Bs.Shell
{
    public abstract class GetEnumTriggers<T> : GetTriggers
    {
        public override string[] Triggers()
        {
            AssertTIsEnum();
            var names = Enum.GetNames(typeof(T));
            return names;
        }

        private void AssertTIsEnum()
        {
            if (!typeof(T).IsEnum)
                throw new Exception("T must be an enum");
        }
    }
}