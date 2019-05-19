using System;
using System.Collections;

namespace Bs.Shell
{
    public class LoadSceneBase
    {
        public Action LoadSceneComplete;
    }

    public class LoadScene<TData, TController> : LoadSceneBase
        where TData : ControllerData
        where TController : ControllerBase<TData>
    {
        public WaitForControllerTokenYieldInstruction<TData, ControllerBase<TData>> waitForToken;

        public LoadScene(TData data)
        {
            LoadSceneWatchdog.Start(LoadSceneRoutine(data));
        }

        private CoroutineWatchdog LoadSceneWatchdog = new CoroutineWatchdog();
        private IEnumerator LoadSceneRoutine(TData data)
        {
            //  Return a token that is ready when the scene is finished loading!
            waitForToken = Bs.Shell.App.Instance.LoadControllerAsync<TData, TController>(null);
            yield return waitForToken;
            //  Call bind on the token!
            waitForToken.controllerToken.controllerDataEvent.Raise(data);

            if (LoadSceneComplete != null)
                LoadSceneComplete();
        }
    }
}