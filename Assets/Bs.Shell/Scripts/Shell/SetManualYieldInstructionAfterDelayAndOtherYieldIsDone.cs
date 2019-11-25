using System.Collections;
using UnityEngine;

namespace Bs.Shell
{
    public class SetManualYieldInstructionAfterDelayAndOtherYieldIsDone
    {
        private ManualYieldInstruction returnYieldInstruction;
        private CustomYieldInstruction customYieldInstruction;
        private CoroutineWatchdog DisposeRoutineWatchdog = new CoroutineWatchdog();

        public ManualYieldInstruction ManualYieldInstruction
        {
            get
            {
                return returnYieldInstruction;
            }
        }

        public SetManualYieldInstructionAfterDelayAndOtherYieldIsDone(float f, CustomYieldInstruction customYieldInstruction)
        {
            returnYieldInstruction = new ManualYieldInstruction();
            this.customYieldInstruction = customYieldInstruction;
            DisposeRoutineWatchdog.Start(DisposeRoutine(f));
        }

        private IEnumerator DisposeRoutine(float f)
        {
            yield return new WaitForSeconds(f);
            yield return customYieldInstruction;
            returnYieldInstruction.IsDone = true;
        }
    }
}