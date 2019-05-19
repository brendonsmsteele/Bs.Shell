using Bs.Shell.EditorVariables;
using System;
using System.Collections;
using UnityEngine;

namespace Bs.Shell
{
    public abstract class ControllerBase<TData> : MonoBehaviour, IController<TData>, IDisposableController
    where TData : ControllerData
    {
        ControllerToken _token;
        public ControllerToken token
        {
            get { return _token; }
            set
            {
                _token = value;
                OnAssignedControllerToken();
            }
        }

        Guid guid;
        public Guid Guid
        {
            get { return guid; }
        }

        public abstract void Bind(TData data);

        public virtual ManualYieldInstruction Dispose()
        {
            ManualYieldInstruction manualYield = new ManualYieldInstruction();
            manualYield.IsDone = true;
            return manualYield;
        }

        protected virtual void OnAssignedControllerToken()
        {
            token.preloadingSceneAssets = false;
        }


        /// <summary>
        /// IMPORTANT you must call base.OnEnable();
        /// </summary>
        protected virtual void OnEnable()
        {
            if (Event != null)
                Event.RegisterListener(this);
            AddEventListeners();
        }

        /// <summary>
        /// IMPORTANT you must call base.OnDiable();
        /// </summary>
        protected virtual void OnDisable()
        {
            if (Event != null)
                Event.UnregisterListener(this);
            RemoveEventListeners();
        }

        /// <summary>
        /// Add button listeners.
        /// </summary>
        protected abstract void AddEventListeners();

        /// <summary>
        /// Remove button listeners.
        /// </summary>
        protected abstract void RemoveEventListeners();

        [SerializeField] ControllerDataEvent _Event;
        public ControllerDataEvent<TData> Event
        {
            get
            {
                return (ControllerDataEvent<TData>)_Event;
            }
            set
            {
                if (_Event != value)
                {
                    //  Unsubscribe old
                    if (_Event != null)
                        ((ControllerDataEvent<TData>)_Event).UnregisterListener(this);

                    //  Subscribe new if not null
                    _Event = value;
                    if (_Event != null)
                        ((ControllerDataEvent<TData>)_Event).RegisterListener(this);
                }
            }
        }

        public void OnEventRaised(TData data)
        {
            this.Bind(data);
        }

        /// <summary>
        /// Check for null because token is null if app not started from Main.unity
        /// </summary>
        protected void PreloadingSceneAssetsComplete()
        {
            if (token != null)
                token.preloadingSceneAssets = false;    //  Releases after the thread is done locking up.  klol
        }


        #region Dispose Sugar
        public ManualYieldInstruction GetTimedYield(float f)
        {
            //return new SetManualYieldInstructionAfterDelayAndOtherYieldIsDone(f, NavigationMapController.Instance);
            return new SetManualYieldInstructionOnDelay(f).ManualYieldInstruction;
        }

        private class SetManualYieldInstructionOnDelay
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


        private class SetManualYieldInstructionAfterDelayAndOtherYieldIsDone
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
        #endregion
    }
}