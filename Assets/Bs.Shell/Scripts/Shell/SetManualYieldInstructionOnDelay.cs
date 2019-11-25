using System.Collections;
using UnityEngine;

namespace Bs.Shell
{
    public class SetManualYieldInstructionOnDelay
    {
        private ManualYieldInstruction yieldInstruction;
        private CoroutineWatchdog DisposeRoutineWatchdog = new CoroutineWatchdog();

        public ManualYieldInstruction ManualYieldInstruction
        {
            get
            {
                return yieldInstruction;
            }
        }

        public SetManualYieldInstructionOnDelay(float f)
        {
            yieldInstruction = new ManualYieldInstruction();
            DisposeRoutineWatchdog.Start(DisposeRoutine(f));
        }

        private IEnumerator DisposeRoutine(float f)
        {
            yield return new WaitForSeconds(f);
            yieldInstruction.IsDone = true;
        }
    }
}