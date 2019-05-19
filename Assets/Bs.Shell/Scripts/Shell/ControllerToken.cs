using Bs.Shell.EditorVariables;
using System;
using UnityEngine;
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

        /// <summary>
        /// Only use this if you know data is of type TData.
        /// </summary>
        /// <param name="data"></param>
        public override void Raise(ControllerData data)
        {
            if (data is TData)
            {
                var tData = (TData)data;
                Raise(tData);
            }
            else
                Debug.LogError("Data is not of correct type -> " + nameof(TData) );
        }

        public void Raise(TData tdata)
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

        public abstract void Raise(ControllerData data);
    }
}
