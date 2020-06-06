using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bs.Shell
{
    [CreateAssetMenu(fileName = nameof(App), menuName = Shell.Menu.Paths.APP + nameof(App))]
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
        public WaitForControllerTokenYieldInstruction<TModel, SceneController<TModel>> LoadControllerAsync<TModel, TController>(Transform parent = null, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
            where TModel : Model
            where TController : SceneController<TModel>
        {
            var type = typeof(TController);
            string path = type.Name;
            WaitForControllerTokenYieldInstruction<TModel, SceneController<TModel>> waitForUIToken = new WaitForControllerTokenYieldInstruction<TModel, SceneController<TModel>>();
            waitForUIToken.controllerToken = new SceneControllerToken<TModel>(Guid.NewGuid());
            scene = null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(path, loadSceneMode);
            RunCoroutine.Instance.StartCoroutine(LoadController<TModel, TController>(asyncOperation, waitForUIToken, path, parent));
            return waitForUIToken; 
        }

        IEnumerator LoadController<TModel, TController>(AsyncOperation asyncOperation, WaitForControllerTokenYieldInstruction<TModel, SceneController<TModel>> waitForControllerToken, string path, Transform parent = null)
            where TModel : Model
            where TController : SceneController<TModel>
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
                    waitForControllerToken.controllerToken.scene = scene.Value;
                    controller.token = waitForControllerToken.controllerToken;
                }
                else
                {
                    Debug.LogError("Exception, SceneController non existent on root go of scene. ~ " + path);
                }
            }
            else
            {
                Debug.LogError("Exception you are trying to load a UI, UI's must be at the root of the scene. ~" + path);
                Debug.LogError("Also make sure all gameObjects are nested under UI.");
            }
        }

        public TController GetController<TModel, TController>(SceneControllerToken controllerToken)
            where TModel : Model
            where TController : SceneController<TModel>
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

        IDisposableController GetDisposableUI(SceneControllerToken uiToken)
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

        List<SceneControllerToken> uiTokensMarkedForDeath = new List<SceneControllerToken>();
        public ManualYieldInstruction UnloadUI(SceneControllerToken uiToken)
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

        public IEnumerator WaitForDisposeToFinish(ManualYieldInstruction manualYield, SceneControllerToken uiToken)
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
