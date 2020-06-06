using System;
using System.Collections;

namespace Bs.Shell
{
    public class LoadSceneBase
    {
        public Action LoadSceneComplete;
    }

    public class LoadScene<TModel> : LoadSceneBase
        where TModel : Model
    {
        public WaitForControllerTokenYieldInstruction<TModel> waitForToken;

        public LoadScene()
        {
            LoadSceneWatchdog.Start(LoadSceneRoutine());
        }

        private CoroutineWatchdog LoadSceneWatchdog = new CoroutineWatchdog();
        private IEnumerator LoadSceneRoutine()
        {
            //  Return a token that is ready when the scene is finished loading!
            waitForToken = App.Instance.LoadControllerAsync<TModel>();
            yield return waitForToken;
            //  Call bind on the token!
            LoadSceneComplete?.Invoke();
        }
    }
}