using System;
using System.Collections;

namespace Bs.Shell
{
    public class UnloadSceneBase
    {
        public Action UnloadSceneComplete;
    }

    public class UnloadScene : UnloadSceneBase
    {
        public UnloadScene(ControllerToken token)
        {
            UnloadSceneWatchdog.Start(UnloadSceneRoutine(token));
        }

        private CoroutineWatchdog UnloadSceneWatchdog = new CoroutineWatchdog();
        private IEnumerator UnloadSceneRoutine(ControllerToken token)
        {
            //  Return a token that is ready when the scene is finished Unloading!
            var unloader = Bs.Shell.App.Instance.UnloadUI(token);
            yield return unloader;
            if (UnloadSceneComplete != null)
                UnloadSceneComplete();
        }
    }
}