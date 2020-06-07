using System.Collections;
using UnityEngine;

namespace Nc.Shell.Async
{
    public class CoroutineWatchdog : CustomYieldInstruction
    {
        private Coroutine coroutine;

        /// <summary>
        /// Returns the designated coroutine, or null
        /// </summary>
        public Coroutine Coroutine
        {
            get { return coroutine; }
            private set { coroutine = value; }
        }

        /// <summary>
        /// Returns true if the coroutine is running, and false if the coroutine has finished running.
        /// via CustomYieldInstruction
        /// </summary>
        public override bool keepWaiting
        {
            get
            {
                return coroutine != null;
            }
        }

        public bool IsComplete
        {
            get
            {
                return coroutine == null;
            }
        }

        /// <summary>
        /// Starts the designated enumerator as a coroutine.
        /// </summary>
        /// <returns>true if the routine was started, false if the routine was not started</returns>
        public bool Start(IEnumerator enumerator)
        {
            bool run = !keepWaiting;

            if(run)
            {
                RunCoroutine.Instance.StartCoroutine(Run(enumerator));
            }

            return run;
        }

        private IEnumerator Run(IEnumerator enumerator)
        {
            coroutine = RunCoroutine.Instance.StartCoroutine(enumerator);
            yield return coroutine;
            coroutine = null;
        }
        
        /// <summary>
        /// Stop this routine
        /// </summary>
        /// <returns>true if the routine was stopped, false if the routine was not stopped</returns>
        public bool Stop()
        {
            bool stop = keepWaiting;

            if (stop)
            {
                RunCoroutine.Instance.StopCoroutine(coroutine);
                coroutine = null;
            }

            return stop;
        }
    }
}
