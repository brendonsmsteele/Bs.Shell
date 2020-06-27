using Nc.Shell.Async;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nc.Shell
{
    [CreateAssetMenu(fileName = nameof(App), menuName = Shell.Menu.Paths.APP + nameof(App))]
    public class App : ScriptableObject
    {
        static App _instance;
        public static App Instance
        {
            get { return _instance; }
        }

        Dictionary<Guid, IDisposableAsync> loadedControllers;
        Scene? scene = null;

        public void Init()
        {
            Debug.Log("Shell.Init");
            _instance = this;
            loadedControllers = new Dictionary<Guid, IDisposableAsync>();
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
        public SceneControllerToken<TModel> LoadSceneControllerAsync<TModel>(TModel model, Transform parent = null, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
            where TModel : SceneControllerModel
        {
            var controllerName = DeriveControllerName<TModel>();
            var token = new SceneControllerToken<TModel>(Guid.NewGuid(), model);
            scene = null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(controllerName, loadSceneMode);
            RunCoroutine.Instance.StartCoroutine(LoadController<TModel>(asyncOperation, token, controllerName, parent));
            return token; 
        }

        IEnumerator LoadController<TModel>(AsyncOperation asyncOperation, SceneControllerToken<TModel> token, string path, Transform parent = null)
            where TModel : SceneControllerModel
        {
            yield return asyncOperation;
            Debug.Log("Scene Loaded");

            if (scene.Value.rootCount == 1)
            {
                GameObject root = scene.Value.GetRootGameObjects()[0];
                SceneController<TModel> controller = root.GetComponent<SceneController<TModel>>();
                if (controller != null)
                {
                    loadedControllers.Add(token.guid, controller);
                    token.scene = scene.Value;
                    controller.token = token;
                }
                else
                {
                    throw new Exception("Exception, SceneController non existent on root go of scene. ~ " + path);
                }
            }
            else
            {
                throw new Exception("Exception you are trying to load a UI, UI's must be at the root of the scene. ~" + path);
                throw new Exception("Also make sure all gameObjects are nested under UI.");
            }
        }

        public TController GetController<TModel, TController>(SceneControllerToken controllerToken)
            where TModel : SceneControllerModel
            where TController : SceneController<TModel>
        {
            if (controllerToken.keepWaiting)
            {
                Debug.LogError("Token is not loaded!");
                return null;
            }
            if (loadedControllers.ContainsKey(controllerToken.guid))
                return loadedControllers[controllerToken.guid] as TController;
            return null;
        }

        IDisposableAsync GetDisposableUI(SceneControllerToken token)
        {
            if (token.keepWaiting)
            {
                Debug.LogError("Token is not loaded!");
                return null;
            }
            if (loadedControllers.ContainsKey(token.guid))
                return loadedControllers[token.guid];
            return null;
        }

        List<SceneControllerToken> tokensMarkedForDeath = new List<SceneControllerToken>();
        public ManualYieldInstruction UnloadSceneController(SceneControllerToken token)
        {
            if (token == null) return null;
            if (tokensMarkedForDeath.Contains(token))
            {
                Debug.LogError("Token is marked for death!");
                return null;
            }
            if (token.keepWaiting)
            {
                Debug.LogError("Token is not loaded!");
                return null;
            }
            if (loadedControllers.ContainsKey(token.guid))
            {
                ManualYieldInstruction disposeManualYieldInstruction = GetDisposableUI(token).Dispose();
                tokensMarkedForDeath.Add(token);
                RunCoroutine.Instance.StartCoroutine(WaitForDisposeToFinish(disposeManualYieldInstruction, token));
                return disposeManualYieldInstruction;
            }
            return null;
        }

        public IEnumerator WaitForDisposeToFinish(ManualYieldInstruction manualYield, SceneControllerToken uiToken)
        {
            yield return manualYield;
            tokensMarkedForDeath.Remove(uiToken);
            loadedControllers.Remove(uiToken.guid);
            SceneManager.UnloadSceneAsync(uiToken.scene);
        }

        /// <summary>
        /// Derive the controller name as convention to prevent a bunch of busy work.
        /// Nc.Shell.ExampleController+Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        private string DeriveControllerName<TModel>()
        {
            var typeString = typeof(TModel).ToString();
            var lastPeriod = typeString.LastIndexOf('.');
            lastPeriod++;
            var lastPlusSign = typeString.LastIndexOf('+');
            var lengthOfControllerName = lastPlusSign - lastPeriod;
            var controller = typeString.Substring(lastPeriod, lengthOfControllerName);
            return controller;
        }
    }
}