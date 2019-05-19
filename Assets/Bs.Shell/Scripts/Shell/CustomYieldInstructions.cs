using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Bs.Shell
{
    public class ManualYieldInstruction : CustomYieldInstruction
    {
        public bool IsDone = false;

        public override bool keepWaiting
        {
            get { return !IsDone; }
        }
    }

    public class WaitForGameObjectYieldInstruction : CustomYieldInstruction
    {
        public GameObject gameObject;

        public override bool keepWaiting
        {
            get { return gameObject != null; }
        }
    }

    public class WaitForControllerTokenYieldInstruction<TData, TUI> : CustomYieldInstruction
        where TData : ControllerData
        where TUI : ControllerBase<TData>
    {
        public ControllerToken<TData> controllerToken;

        public override bool keepWaiting
        {
            get { return !controllerToken.IsLoaded() || controllerToken.controllerDataEvent == null; }
        }
    }

    public class WaitForVector3YieldInstruction : CustomYieldInstruction
    {
        public Vector3? vector3;

        public override bool keepWaiting
        {
            get { return vector3 == null; }
        }
    }

    public class WaitForVideoPlayerIsPrepared : CustomYieldInstruction
    {
        VideoPlayer videoPlayer;

        public WaitForVideoPlayerIsPrepared(VideoPlayer videoPlayer)
        {
            this.videoPlayer = videoPlayer;
        }

        public override bool keepWaiting
        {
            get { return !videoPlayer.isPrepared; }
        }
    }

    public class WaitForTextureLoadFromResources : CustomYieldInstruction
    {
        private string path;
        private RawImage rawImage;
        private ResourceRequest loader;
        private bool isComplete;

        public WaitForTextureLoadFromResources(string path, RawImage rawImage)
        {
            this.path = path;
            this.rawImage = rawImage;
            LoadFromResourcesWatchdog.Start(LoadFromResources());
        }

        public void Stop()
        {
            LoadFromResourcesWatchdog.Stop();
            isComplete = true;
        }

        private CoroutineWatchdog LoadFromResourcesWatchdog = new CoroutineWatchdog();
        private IEnumerator LoadFromResources()
        {
            rawImage.color = Color.clear;
            loader = Resources.LoadAsync(path);
            yield return loader.isDone;
            if (loader.asset == null)
                rawImage.color = Color.red;
            else
            {
                rawImage.color = Color.white;
                rawImage.texture = (Texture)loader.asset;
                rawImage.SetNativeSize();
            }
            isComplete = true;
        }

        public override bool keepWaiting
        {
            get { return !isComplete; }
        }

    }

    public class WaitForGameObjectLoadFromResources : CustomYieldInstruction
    {
        private string path;
        private Transform transform;
        private bool isComplete;
        public GameObject Prefab;

        public WaitForGameObjectLoadFromResources(string path, Transform transform)
        {
            this.path = path;
            this.transform = transform;
            LoadFromResourcesWatchdog.Start(LoadFromResources());
        }

        private CoroutineWatchdog LoadFromResourcesWatchdog = new CoroutineWatchdog();
        private IEnumerator LoadFromResources()
        {
            var loader = Resources.LoadAsync(path);
            yield return loader;
            if(loader.asset == null)
            {
                Debug.LogError("Failed to load prefab at path " + path);
            }
            else
            {
                var clone = (GameObject)Object.Instantiate(loader.asset);
                clone.transform.parent = transform;
                clone.transform.localScale = Vector3.one;
                clone.transform.localEulerAngles = Vector3.zero;
                clone.transform.localPosition = Vector3.zero;
                Prefab = clone;
            }
            isComplete = true;
        }

        public void Stop()
        {
            LoadFromResourcesWatchdog.Stop();
            isComplete = true;
        }

        public override bool keepWaiting
        {
            get { return !isComplete; }
        }

    }


    /// <summary>
    /// Encapsulates all controllers loading
    /// </summary>
    public class WaitForControllersToFinishLoading : CustomYieldInstruction
    {
        List<ControllerToken> controllers = new List<ControllerToken>();

        public void Update(List<ControllerToken> controllers)
        {
            this.controllers = controllers;
        }

        public override bool keepWaiting
        {
            get
            {
                bool allControllersAreLoaded = true;
                foreach (var controller in controllers)
                {
                    if (!controller.IsLoadedAndAssetsArePreloaded())
                    {
                        allControllersAreLoaded = false;
                        break;
                    }
                }
                return !allControllersAreLoaded;
            }
        }
    }

    public class WaitForInstantiateAsync : CustomYieldInstruction
    {
        public WaitForInstantiateAsync(GameObject prefab)
        {
            //Object.Instantiate()
        }

        public void Abort()
        {

        }

        public override bool keepWaiting
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
    }

}
