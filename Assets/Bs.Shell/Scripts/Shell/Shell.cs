using Bs.Shell.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bs.Shell
{
    [CreateAssetMenu(fileName = "App", menuName = "Bs.Shell/App")]
    public class App : ScriptableObject
    {
        static App _instance;
        public static App Instance
        {
            get { return _instance; }
        }

        Dictionary<Guid, IDisposableUI> loadedUIs;
        Scene? scene = null;

        public void Init()
        {
            Debug.Log("Shell.Init");
            _instance = this;
            loadedUIs = new Dictionary<Guid, IDisposableUI>();
            if(RunCoroutine.Instance == null)
            {
                //  Create the RunCoroutine object if able.
                var go = new GameObject();
                go.AddComponent<RunCoroutine>();
                go.name = "RunCoroutine - App";
                DontDestroyOnLoad(go);
            }
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Debug.Log(arg0.name);
            scene = arg0;
        }

        /// Dictate the type of UI to look for when scene is loaded.
        /// Pass in the UIDataEvent, you create and manage your own UIDataEvent.
        /// All UIs only add to the scene.
        public WaitForUITokenYieldInstruction<TData, UIBase<TData>> LoadUIAsync<TData, TUI>(string path, UIDataEvent<TData> uiDataEvent, Transform parent = null)
            where TData : UIData
            where TUI : UIBase<TData>
        {
            WaitForUITokenYieldInstruction<TData, UIBase<TData>> waitForUIToken = new WaitForUITokenYieldInstruction<TData, UIBase<TData>>();
            waitForUIToken.uiToken = new UIToken<TData>(Guid.NewGuid());
            scene = null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(path, LoadSceneMode.Additive);
            RunCoroutine.Instance.StartCoroutine(LoadUI<TData, TUI>(asyncOperation, waitForUIToken, path, uiDataEvent, parent));
            return waitForUIToken; 
        }

        IEnumerator LoadUI<TData, TUI>(AsyncOperation asyncOperation, WaitForUITokenYieldInstruction<TData, UIBase<TData>> waitForUIToken, string path, UIDataEvent<TData> uiDataEvent, Transform parent = null)
            where TData : UIData
            where TUI : UIBase<TData>
        {
            yield return asyncOperation;
            Debug.Log("Scene Loaded");

            if (scene.Value.rootCount == 1)
            {
                GameObject root = scene.Value.GetRootGameObjects()[0];
                TUI ui = root.GetComponent<TUI>();
                if (ui != null)
                {
                    loadedUIs.Add(waitForUIToken.uiToken.guid, ui);
                    waitForUIToken.uiToken.uiDataEvent = uiDataEvent;
                    waitForUIToken.uiToken.scene = scene.Value;
                    ui.Event = uiDataEvent;
                    if (parent != null)
                        root.transform.SetParent(parent, true);
                }
                else
                {
                    Debug.LogError("Exception, UI non existent on root go of scene. ~ " + path);
                }
            }
            else
            {
                Debug.LogError("Exception you are trying to load a UI, UI's must be at the root of the scene. ~" + path);
            }
        }

        public TUI GetUI<TData, TUI>(UIToken uiToken)
            where TData : UIData
            where TUI : UIBase<TData>
        {
            if (!uiToken.IsLoaded())
            {
                Debug.LogError("UIToken is not loaded!");
                return null;
            }
            if (loadedUIs.ContainsKey(uiToken.guid))
                return loadedUIs[uiToken.guid] as TUI;
            return null;
        }

        IDisposableUI GetDisposableUI(UIToken uiToken)
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

        List<UIToken> uiTokensMarkedForDeath = new List<UIToken>();
        public void UnloadUI(UIToken uiToken)
        {
            if (uiTokensMarkedForDeath.Contains(uiToken))
            {
                Debug.LogError("UIToken is marked for death!");
                return;
            }
            if (!uiToken.IsLoaded())
            {
                Debug.LogError("UIToken is not loaded!");
                return;
            }
            if (loadedUIs.ContainsKey(uiToken.guid))
            {
                ManualYieldInstruction disposeManualYieldInstruction = GetDisposableUI(uiToken).Dispose();
                uiTokensMarkedForDeath.Add(uiToken);
                RunCoroutine.Instance.StartCoroutine(WaitForDisposeToFinish(disposeManualYieldInstruction, uiToken));
            }
        }

        public IEnumerator WaitForDisposeToFinish(ManualYieldInstruction manualYield, UIToken uiToken)
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
