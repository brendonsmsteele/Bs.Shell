using Bs.Shell.EditorVariables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bs.Shell
{
    [CreateAssetMenu(fileName = nameof(App), menuName = "Bs.Shell/" + nameof(App))]
    public class App : ScriptableObject
    {
        static App _instance;
        public static App Instance
        {
            get { return _instance; }
        }

        Dictionary<Guid, IDisposableController> loadedUIs;
        Scene? scene = null;

        public void Init()
        {
            Debug.Log("Shell.Init");
            _instance = this;
            loadedUIs = new Dictionary<Guid, IDisposableController>();
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Debug.Log("<color=cyan>" + arg0.name + "</color>");
            scene = arg0;
        }

        /// Dictate the type of UI to look for when scene is loaded.
        /// Pass in the UIDataEvent, you create and manage your own UIDataEvent.
        /// All UIs only add to the scene.
        public WaitForControllerTokenYieldInstruction<TData, ControllerBase<TData>> LoadControllerAsync<TData, TController>(ControllerDataEvent<TData> controllerDataEvent, Transform parent = null, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
            where TData : ControllerData
            where TController : ControllerBase<TData>
        {
            var type = typeof(TController);
            string path = type.Name;
            WaitForControllerTokenYieldInstruction<TData, ControllerBase<TData>> waitForUIToken = new WaitForControllerTokenYieldInstruction<TData, ControllerBase<TData>>();
            waitForUIToken.controllerToken = new ControllerToken<TData>(Guid.NewGuid());
            scene = null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(path, loadSceneMode);
            RunCoroutine.Instance.StartCoroutine(LoadController<TData, TController>(asyncOperation, waitForUIToken, path, controllerDataEvent, parent));
            return waitForUIToken; 
        }

        IEnumerator LoadController<TData, TController>(AsyncOperation asyncOperation, WaitForControllerTokenYieldInstruction<TData, ControllerBase<TData>> waitForControllerToken, string path, ControllerDataEvent<TData> controllerDataEvent, Transform parent = null)
            where TData : ControllerData
            where TController : ControllerBase<TData>
        {
            yield return asyncOperation;
            Debug.Log("Scene Loaded");

            if (scene.Value.rootCount == 1)
            {
                GameObject root = scene.Value.GetRootGameObjects()[0];
                TController controller = root.GetComponent<TController>();
                if (controller != null)
                {
                    loadedUIs.Add(waitForControllerToken.controllerToken.guid, controller);
                    if(controllerDataEvent == null)
                    {
                        waitForControllerToken.controllerToken.controllerDataEvent = controller.Event;
                    }
                    else
                    {
                        waitForControllerToken.controllerToken.controllerDataEvent = controllerDataEvent;
                        controller.Event = controllerDataEvent;
                    }
                    waitForControllerToken.controllerToken.scene = scene.Value;
                    
                    if (parent != null)
                        root.transform.SetParent(parent, true);
                    controller.token = waitForControllerToken.controllerToken;
                }
                else
                {
                    Debug.LogError("Exception, UI non existent on root go of scene. ~ " + path);
                }
            }
            else
            {
                Debug.LogError("Exception you are trying to load a UI, UI's must be at the root of the scene. ~" + path);
                Debug.LogError("Also make sure all gameObjects are nested under UI.");
            }
        }

        public TController GetController<TData, TController>(ControllerToken controllerToken)
            where TData : ControllerData
            where TController : ControllerBase<TData>
        {
            if (!controllerToken.IsLoaded())
            {
                Debug.LogError("UIToken is not loaded!");
                return null;
            }
            if (loadedUIs.ContainsKey(controllerToken.guid))
                return loadedUIs[controllerToken.guid] as TController;
            return null;
        }

        IDisposableController GetDisposableUI(ControllerToken uiToken)
        {
            if (!uiToken.IsLoaded())
            {
                Debug.LogError("UIToken is not loaded!");
                return null;
            }
            if (loadedUIs.ContainsKey(uiToken.guid))
                return loadedUIs[uiToken.guid];
            return null;
        }

        List<ControllerToken> uiTokensMarkedForDeath = new List<ControllerToken>();
        public ManualYieldInstruction UnloadUI(ControllerToken uiToken)
        {
            if (uiToken == null) return null;
            if (uiTokensMarkedForDeath.Contains(uiToken))
            {
                Debug.LogError("UIToken is marked for death!");
                return null;
            }
            if (!uiToken.IsLoaded())
            {
                Debug.LogError("UIToken is not loaded!");
                return null;
            }
            if (loadedUIs.ContainsKey(uiToken.guid))
            {
                ManualYieldInstruction disposeManualYieldInstruction = GetDisposableUI(uiToken).Dispose();
                uiTokensMarkedForDeath.Add(uiToken);
                RunCoroutine.Instance.StartCoroutine(WaitForDisposeToFinish(disposeManualYieldInstruction, uiToken));
                return disposeManualYieldInstruction;
            }
            return null;
        }

        public IEnumerator WaitForDisposeToFinish(ManualYieldInstruction manualYield, ControllerToken uiToken)
        {
            yield return manualYield;
            uiTokensMarkedForDeath.Remove(uiToken);
            loadedUIs.Remove(uiToken.guid);
            SceneManager.UnloadSceneAsync(uiToken.scene);
        }

        #region Callbacks

        #endregion
    }

}
