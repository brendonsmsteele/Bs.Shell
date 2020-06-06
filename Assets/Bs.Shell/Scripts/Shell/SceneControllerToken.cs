using System;
using UnityEngine.SceneManagement;

namespace Bs.Shell
{
    public class SceneControllerToken<TModel> : SceneControllerToken
        where TModel : Model
    {
        public SceneController<TModel> sceneController {
            get
            {
                if(scene == null)
                    return null;
                var root = scene.GetRootGameObjects()[0];
                var target = root.GetComponent<SceneController<TModel>>();
                if (target == null)
                    throw new Exception($"Missing SceneController type: {nameof(SceneController<TModel>)} at scene: {scene.name} / gameObject: {root.name}");
                return target;
            }
        }

        public SceneControllerToken(Guid guid)
        {
            this.guid = guid;
        }

        public bool Equals(SceneControllerToken otherUIToken)
        {
            return (IsLoaded() && guid == otherUIToken.guid);
        }

        public void Raise(TModel model)
        {
            if (!IsLoaded())
                return;
            sceneController.model = model;
        }
    }

    public abstract class SceneControllerToken
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
