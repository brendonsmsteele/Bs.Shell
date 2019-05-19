using Bs.Shell.EditorVariables;
using System;
using UnityEngine.SceneManagement;

namespace Bs.Shell
{
    public class ControllerToken<TData> : ControllerToken
        where TData : ControllerData
    {
        public ControllerDataEvent<TData> controllerDataEvent;

        public ControllerToken(Guid guid)
        {
            this.guid = guid;
        }

        public bool Equals(ControllerToken otherUIToken)
        {
            return (IsLoaded() && guid == otherUIToken.guid);
        }
    }

    public class ControllerToken
    {
        public Guid guid;
        public Scene scene;
        public bool disposeInProgress;
        public bool preloadingSceneAssets = true;

        public bool IsLoaded()
        {
            return !disposeInProgress && scene.isLoaded;
        }

        public bool IsLoadedAndAssetsArePreloaded()
        {
            return IsLoaded() && !preloadingSceneAssets;
        }
    }
}
