using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bs.Shell
{
    public class ControllerToken<TModel> : ControllerToken
        where TModel : Model
    {
        public ControllerDataEvent<TModel> controllerDataEvent;

        public ControllerToken(Guid guid)
        {
            this.guid = guid;
        }

        public bool Equals(ControllerToken otherUIToken)
        {
            return (IsLoaded() && guid == otherUIToken.guid);
        }

        /// <summary>
        /// Only use this if you know data is of type TData.
        /// </summary>
        /// <param name="model"></param>
        public override void Raise(Model model)
        {
            if (model is TModel)
            {
                var tData = (TModel)model;
                Raise(tData);
            }
            else
                Debug.LogError("Data is not of correct type -> " + nameof(TModel) );
        }

        public void Raise(TModel tdata)
        {
            if (!IsLoaded())
                return;
            controllerDataEvent.Raise(tdata);
        }
    }

    public abstract class ControllerToken
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

        public abstract void Raise(Model model);
    }
}
