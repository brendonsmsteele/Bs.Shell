using UnityEngine;

namespace Bs.Shell
{
    public class ManualYieldInstruction : CustomYieldInstruction
    {
        public bool IsDone = false;

        public override bool keepWaiting
        {
            get { return !IsDone; }
        }
    }

    public class WaitForGameObjectYieldInstruction : CustomYieldInstruction
    {
        public GameObject gameObject;

        public override bool keepWaiting
        {
            get { return gameObject != null; }
        }
    }

    public class WaitForUITokenYieldInstruction<TData, TUI> : CustomYieldInstruction
        where TData : UIData
        where TUI : UIBase<TData>
    {
        public UIToken<TData> uiToken;

        public override bool keepWaiting
        {
            get { return !uiToken.IsLoaded() || uiToken.uiDataEvent == null; }
        }
    }

    public class WaitForVector3YieldInstruction : CustomYieldInstruction
    {
        public Vector3? vector3;

        public override bool keepWaiting
        {
            get { return vector3 == null; }
        }
    }

}
