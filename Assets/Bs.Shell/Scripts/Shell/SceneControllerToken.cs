using System;
using UnityEngine.SceneManagement;

namespace Nc.Shell
{
    /// <summary>
    /// Contains all navigation meta data for a SceneController
    /// You can .SetModel() to morph the SceneController
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class SceneControllerToken<TModel> : SceneControllerToken
        where TModel : Model
    {
        private SceneController<TModel> sceneController {
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

        public void SetModel(TModel model)
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
