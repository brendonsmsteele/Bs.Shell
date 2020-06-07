using Nc.Shell.Async;
using Nc.Shell.Services;
using Nc.Shell.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nc.Shell.Navigation
{
    [CreateAssetMenu(fileName =nameof(NavigationController), menuName = Shell.Menu.Paths.NAVIGATION + nameof(NavigationController))]
    public class NavigationController : Service
    {
        [SerializeField] GameObject mapPrefab;
        Animator _map;
        Animator map
        {
            get
            {
                if (_map == null)
                    _map = Instantiate(mapPrefab).GetComponent<Animator>();
                return _map;
            }
        }

        DiffableDictionary<Model, SceneControllerToken> _activeControllers;
        DiffableDictionary<Model, SceneControllerToken> ActiveControllers
        {
            get
            {
                if(_activeControllers == null)
                {
                    _activeControllers = new DiffableDictionary<Model, SceneControllerToken>(
                    //  Add
                    (model) =>
                    {
                        return LoadController(model);
                    },
                    //  Update
                    (model, controllerToken, i) =>
                    {
                        BindDataToController(model, controllerToken);
                    },
                    //  Remove
                    (model, controllerToken) =>
                    {
                        controllerTokensQueuedToUnload.Add(controllerToken);
                        //  TODO track this.
                    },
                    //  OnComplete
                    () =>
                    {
                        AllActiveControllersAreLoaded.IsDone = false;
                        MonitorLoadedControllersWatchdog.Stop();
                        MonitorLoadedControllersWatchdog.Start(MonitorLoadedControllersCoroutine());
                        UnloadControllerTokensQueuedToUnload();
                    });
                }
                return _activeControllers;
            }
        }

        private List<SceneControllerToken> controllerTokensQueuedToUnload = new List<SceneControllerToken>();
        private void UnloadControllerTokensQueuedToUnload()
        {
            for (int i = controllerTokensQueuedToUnload.Count-1; i >= 0; i--)
            {
                var unloader = new UnloadScene(controllerTokensQueuedToUnload[i]);
            }
            controllerTokensQueuedToUnload.Clear();
        }
       
        #region Monitor Loaded Controllers
        /// <summary>
        /// This thing is shared please don't do anything bad.
        /// </summary>
        public ManualYieldInstruction AllActiveControllersAreLoaded = new ManualYieldInstruction();

        WaitForControllersToFinishLoading WaitForActiveControllersToFinishLoading = new WaitForControllersToFinishLoading();
        CoroutineWatchdog MonitorLoadedControllersWatchdog = new CoroutineWatchdog();

        private IEnumerator MonitorLoadedControllersCoroutine()
        {
            var activeControllers = _activeControllers.GetValues().ToList();
            WaitForActiveControllersToFinishLoading.Update(activeControllers);
            yield return WaitForActiveControllersToFinishLoading;
            AllActiveControllersAreLoaded.IsDone = true;
        }
        #endregion


        /// <summary>
        /// Called by the animator, binds appropriate data.
        /// Separate function due to serialization in Unity animationclip event.
        /// </summary>
        /// <param name="controllerData"></param>
        public void NavigateToPage(NavigationPage navigationPage)
        {
            ActiveControllers.Update(navigationPage.models);
        }

        /// <summary>
        /// Navigate by setting trigger.
        /// </summary>
        /// <param name="destination"></param>
        public void Navigate(string destination)
        {
            map.SetTrigger(destination);
            _lastNavigatedTrigger = destination;
        }

        public void Navigate(NavigationTriggers navigationTriggers)
        {
            Navigate(navigationTriggers.ToString());
        }

        string _lastNavigatedTrigger;
        public string LastNavigatedTrigger
        {
            get { return _lastNavigatedTrigger; }
        }
        
        #region BindDataToController

        private void BindDataToController(Model model, SceneControllerToken controllerToken)
        {
            //  Need to bind this thing with data.
            SceneControllerFactory.SetModel(model, controllerToken);
        }
        
        #endregion


        #region LoadController

        private SceneControllerToken LoadController(Model model)
        {
            return SceneControllerFactory.LoadScene(model);
        }

        public override void Init()
        {
        }

        #endregion
    }
}