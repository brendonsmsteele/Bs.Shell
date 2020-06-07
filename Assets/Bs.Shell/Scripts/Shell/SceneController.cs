using Nc.Shell.Async;

namespace Nc.Shell
{
    public abstract class SceneController<TModel> : ViewController<TModel>, IDisposableAsync
        where TModel : Model
    {
        SceneControllerToken<TModel> _token;
        public SceneControllerToken<TModel> token
        {
            get { return _token; }
            set
            {
                _token = value;
                OnAssignedControllerToken();
            }
        }

        protected virtual void OnAssignedControllerToken()
        {
            token.preloadingSceneAssets = false;
        }

        public virtual ManualYieldInstruction Dispose()
        {
            ManualYieldInstruction manualYield = new TimedManualYieldInstruction(0f); 
            manualYield.IsDone = true;
            return manualYield;
        }
    }
}