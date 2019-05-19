using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    [CreateAssetMenu(fileName =nameof(NavigationMap), menuName ="Bs.Shell/" + nameof(NavigationMap))]
    public class NavigationMap : ScriptableObject
    {
        [SerializeField] NavigationMapTree navigationMapTreeReference;
        NavigationMapTree _navigationMapTree;
        NavigationMapTree navigationMapTree
        {
            get
            {
                if(_navigationMapTree == null)
                {
                    var clone = Instantiate(navigationMapTreeReference.gameObject);
                    DontDestroyOnLoad(clone);
                    _navigationMapTree = clone.GetComponent<NavigationMapTree>();
                }
                return _navigationMapTree;
            }
        }

        //  Consider making this more generic.  
        [SerializeField] NavigationMapView navigationMapViewReference;
        NavigationMapView _navigationMapView;
        NavigationMapView navigationMapView
        {
            get
            {
                if (_navigationMapView == null)
                {
                    var clone = Instantiate(navigationMapView.gameObject);
                    DontDestroyOnLoad(clone);
                    _navigationMapView = clone.GetComponent<NavigationMapView>();
                }
                return _navigationMapView;
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

        #region NavPanel
        bool _showNavPanel = false;
        public bool ShowNavPanel
        {
            get
            {
                return _showNavPanel;
            }
            set
            {
                if (_showNavPanel != value)
                {
                    _showNavPanel = value;
                    navigationMapView.Bind(new NavigationMapViewModel(GetTriggers(), _showNavPanel));
                }
            }
        }
        #endregion

        // Use this for initialization
        public void Init()
        {
            navigationMapView.Bind(new NavigationMapViewModel(GetTriggers(), false));
            navigationMapView.OnMessage += NavigationMapView_OnMessage;
        }

        public void OnDisable()
        {
            Dispose();
        }

        void Dispose()
        {
            Destroy(navigationMapTree.gameObject);
            Destroy(navigationMapView.gameObject);
        }


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
            Debug.Log("Navigate to " + destination);
            navigationMapTree.Animator.SetTrigger(destination);
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

        List<string> GetTriggers()
        {
            var triggers = new List<string>();
            navigationMapTree.Animator.GetTriggers(triggers);
            return triggers;
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

        #endregion


        private void NavigationMapView_OnMessage(string message)
        {
            Navigate(message);
        }

    }
}