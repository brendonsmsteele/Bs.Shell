namespace Bs.Shell
{
    public abstract class SceneController<TModel> : ViewController<TModel>, IDisposableController
        where TModel : Model
    {
        SceneControllerToken _token;
        public SceneControllerToken token
        {
            get { return _token; }
            set
            {
                _token = value;
                OnAssignedControllerToken();
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

        public ManualYieldInstruction GetTimedYield(float f)
        {
            //return new SetManualYieldInstructionAfterDelayAndOtherYieldIsDone(f, NavigationMapController.Instance);
            return new SetManualYieldInstructionOnDelay(f).ManualYieldInstruction;
        }
    }
}