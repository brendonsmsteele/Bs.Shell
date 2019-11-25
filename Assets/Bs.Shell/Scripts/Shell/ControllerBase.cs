using System;
using UnityEngine;

namespace Bs.Shell
{
    public abstract class ControllerBase<TModel> : View<TModel>, IDisposableController
        where TModel : Model
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

        [SerializeField] ControllerDataEvent _Event;
        public ControllerDataEvent<TModel> Event
        {
            get
            {
                return (ControllerDataEvent<TModel>)_Event;
            }
            set
            {
                if (_Event != value)
                {
                    //  Unsubscribe old
                    if (_Event != null)
                        ((ControllerDataEvent<TModel>)_Event).UnregisterListener(this);

                    //  Subscribe new if not null
                    _Event = value;
                    if (_Event != null)
                        ((ControllerDataEvent<TModel>)_Event).RegisterListener(this);
                }
            }
        }

        public new virtual ManualYieldInstruction Dispose()
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

        #endregion
    }
}