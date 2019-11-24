using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    [CreateAssetMenu(fileName =nameof(NavigationController), menuName = "Bs.Shell.Navigation/" + nameof(NavigationController))]
    public class NavigationController : Service
    {
        [SerializeField] Animator mapPrefab;
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

        DiffableDictionary<ControllerData, ControllerToken> _activeControllers;
        DiffableDictionary<ControllerData, ControllerToken> ActiveControllers
        {
            get
            {
                if(_activeControllers == null)
                {
                    _activeControllers = new DiffableDictionary<ControllerData, ControllerToken>(
                    //  Add
                    (controllerData) =>
                    {
                        return LoadController(controllerData);
                    },
                    //  Update
                    (controllerData, controllerToken, i) =>
                    {
                        BindDataToController(controllerData, controllerToken);
                    },
                    //  Remove
                    (controllerData, controllerToken) =>
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

        private List<ControllerToken> controllerTokensQueuedToUnload = new List<ControllerToken>();
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
            ActiveControllers.Update(navigationPage.ActiveControllers);
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
        
        public NavigationPage CreateNavigationPage(params ControllerData[] activePages)
        {
            var page = NavigationPage.Create(activePages);
            return page;
        }

        #region BindDataToController

        private void BindDataToController(ControllerData controllerData, ControllerToken controllerToken)
        {
            controllerToken.Raise(controllerData);
        }
        
        #endregion


        #region LoadController

        private ControllerToken LoadController(ControllerData controllerData)
        {
            return LoadSceneFactory.LoadScene(controllerData);
        }

        public override void Init()
        {
        }

        #endregion
    }
}