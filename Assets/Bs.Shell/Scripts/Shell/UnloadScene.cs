using Nc.Shell.Async;
using Nc.Shell.UI;
using System;
using System.Collections;

namespace Nc.Shell.Navigation
{
    public class UnloadSceneBase
    {
        public Action UnloadSceneComplete;
    }

    public class UnloadScene : UnloadSceneBase
    {
        public UnloadScene(SceneControllerToken token)
        {
            UnloadSceneWatchdog.Start(UnloadSceneRoutine(token));
        }

        private CoroutineWatchdog UnloadSceneWatchdog = new CoroutineWatchdog();
        private IEnumerator UnloadSceneRoutine(SceneControllerToken token)
        {
            //  Return a token that is ready when the scene is finished Unloading!
            var unloader = App.Instance.UnloadUI(token);
            yield return unloader;
            if (UnloadSceneComplete != null)
                UnloadSceneComplete();
        }
    }
}