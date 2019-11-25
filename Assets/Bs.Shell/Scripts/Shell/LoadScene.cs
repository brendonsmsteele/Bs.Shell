using System;
using System.Collections;

namespace Bs.Shell
{
    public class LoadSceneBase
    {
        public Action LoadSceneComplete;
    }

    public class LoadScene<TModel, TController> : LoadSceneBase
        where TModel : Model
        where TController : ControllerBase<TModel>
    {
        public WaitForControllerTokenYieldInstruction<TModel, ControllerBase<TModel>> waitForToken;

        public LoadScene(TModel data)
        {
            LoadSceneWatchdog.Start(LoadSceneRoutine(data));
        }

        private CoroutineWatchdog LoadSceneWatchdog = new CoroutineWatchdog();
        private IEnumerator LoadSceneRoutine(TModel data)
        {
            //  Return a token that is ready when the scene is finished loading!
            waitForToken = Bs.Shell.App.Instance.LoadControllerAsync<TModel, TController>(null);
            yield return waitForToken;
            //  Call bind on the token!
            waitForToken.controllerToken.controllerDataEvent.Raise(data);

            if (LoadSceneComplete != null)
                LoadSceneComplete();
        }
    }
}