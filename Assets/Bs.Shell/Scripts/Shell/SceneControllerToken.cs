using Nc.Shell.Navigation;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nc.Shell
{
    /// <summary>
    /// Contains all navigation meta data for a SceneController
    /// You can .SetModel() to morph the SceneController
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class SceneControllerToken<TModel> : SceneControllerToken
        where TModel : SceneControllerModel
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
        private TModel model;

        public SceneControllerToken(Guid guid, TModel model)
        {
            this.guid = guid;
            this.model = model;
        }

        public bool Equals(SceneControllerToken otherUIToken)
        {
            return (IsLoaded() && guid == otherUIToken.guid);
        }

        private void SetModel()
        {
            if (keepWaiting)
            {
                Debug.LogWarning("SceneController is still loading, will not set model.");
                return;
            }
            sceneController.model = model;
        }

        public override void TrySetModel(SceneControllerModel model)
        {
            try
            {
                var unbox = (TModel)model;
                this.model = unbox;
                SetModel();
            }
            catch(InvalidCastException e)
            {
                Debug.LogException(e);
            }
        }
    }

    public abstract class SceneControllerToken : CustomYieldInstruction, ILoadable
    {
        public Guid guid;
        public Scene scene;
        public bool disposeInProgress;
        public bool preloadingSceneAssets = true;

        public override bool keepWaiting
        {
            get
            {
                return !IsLoaded();
            }
        }

        public bool IsLoaded()
        {
            return !disposeInProgress && scene.isLoaded;
        }

        public bool IsLoadedAndAssetsArePreloaded()
        {
            return IsLoaded() && !preloadingSceneAssets;
        }

        public abstract void TrySetModel(SceneControllerModel model);
    }
}
