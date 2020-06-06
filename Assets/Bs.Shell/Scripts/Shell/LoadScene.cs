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
        where TController : SceneController<TModel>
    {
        public WaitForControllerTokenYieldInstruction<TModel, SceneController<TModel>> waitForToken;

        public LoadScene(TModel data)
        {
            LoadSceneWatchdog.Start(LoadSceneRoutine(data));
        }

        private CoroutineWatchdog LoadSceneWatchdog = new CoroutineWatchdog();
        private IEnumerator LoadSceneRoutine(TModel model)
        {
            //  Return a token that is ready when the scene is finished loading!
            waitForToken = App.Instance.LoadControllerAsync<TModel, TController>();
            yield return waitForToken;
            //  Call bind on the token!
            waitForToken.controllerToken.sceneController.model = model;
            LoadSceneComplete?.Invoke();
        }
    }
}