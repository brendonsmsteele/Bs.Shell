﻿using Nc.Shell.Async;
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

        Dictionary<Guid, IDisposableAsync> loadedUIs;
        Scene? scene = null;

        public void Init()
        {
            Debug.Log("Shell.Init");
            _instance = this;
            loadedUIs = new Dictionary<Guid, IDisposableAsync>();
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
        public WaitForControllerTokenYieldInstruction<TModel> LoadControllerAsync<TModel>(Transform parent = null, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
            where TModel : Model
        {
            var controllerName = DeriveControllerName<TModel>();
            WaitForControllerTokenYieldInstruction<TModel> waitForUIToken = new WaitForControllerTokenYieldInstruction<TModel>();
            waitForUIToken.controllerToken = new SceneControllerToken<TModel>(Guid.NewGuid());
            scene = null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(controllerName, loadSceneMode);
            RunCoroutine.Instance.StartCoroutine(LoadController<TModel>(asyncOperation, waitForUIToken, controllerName, parent));
            return waitForUIToken; 
        }

        IEnumerator LoadController<TModel>(AsyncOperation asyncOperation, WaitForControllerTokenYieldInstruction<TModel> waitForControllerToken, string path, Transform parent = null)
            where TModel : Model
        {
            yield return asyncOperation;
            Debug.Log("Scene Loaded");

            if (scene.Value.rootCount == 1)
            {
                GameObject root = scene.Value.GetRootGameObjects()[0];
                SceneController<TModel> controller = root.GetComponent<SceneController<TModel>>();
                if (controller != null)
                {
                    loadedUIs.Add(waitForControllerToken.controllerToken.guid, controller);
                    waitForControllerToken.controllerToken.scene = scene.Value;
                    controller.token = waitForControllerToken.controllerToken;
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

        IDisposableAsync GetDisposableUI(SceneControllerToken uiToken)
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