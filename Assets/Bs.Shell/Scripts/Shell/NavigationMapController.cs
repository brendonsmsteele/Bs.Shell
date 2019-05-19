using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    public class NavigationMapController : MonoBehaviour
    {
        static NavigationMapController _instance;
        public static NavigationMapController Instance
        {
            get
            {
                if (_instance == null)
                {
                    //  TODO:  Loading in is less convenient than just having it available.
                    //var go = Instantiate(Resources.Load("NavigationMapController")) as GameObject;
                    //go.hideFlags = HideFlags.DontSave;
                    _instance = GameObject.FindWithTag("NavigationMapController").GetComponent<NavigationMapController>();
                }

                return _instance;
            }
        }


        [SerializeField] Animator animator;
        [SerializeField] NavigationMapView navigationMapView;
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
                        if (controllerToken.IsLoaded())
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


        bool _showNavPanel = false;
        bool showNavPanel
        {
            get
            {
                return _showNavPanel;
            }
            set
            {
                if(_showNavPanel != value)
                {
                    _showNavPanel = value;
                    navigationMapView.Bind(new NavigationMapViewModel(GetTriggers(), _showNavPanel));
                }
            }
        }

        void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
        }

        // Use this for initialization
        void Start()
        {
            navigationMapView.Bind(new NavigationMapViewModel(GetTriggers(), false));
            navigationMapView.OnMessage += NavigationMapView_OnMessage;
        }

       

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                showNavPanel = !showNavPanel;
        }

        /// <summary>
        /// Navigate by setting trigger.
        /// </summary>
        /// <param name="destination"></param>
        public void NavigateFromTrigger(string destination)
        {
            Debug.Log("Navigate to " + destination);
            animator.SetTrigger(destination);
            _lastNavigatedTrigger = destination;
        }

        string _lastNavigatedTrigger;
        public string LastNavigatedTrigger
        {
            get { return _lastNavigatedTrigger; }
        }
        

        /// <summary>
        /// Called by the animator, binds appropriate data.
        /// </summary>
        /// <param name="controllerData"></param>
        public void NavigateFromNavigationPage(NavigationPage navigationPage)
        {
            ActiveControllers.Update(navigationPage.ActiveControllers);
        }

        private void ResolveType(ControllerData data)
        {

        }

        public NavigationPage CreateNavigationPage(params ControllerData[] activePages)
        {
            var page = NavigationPage.Create(activePages);
            return page;
        }

        #region BindDataToController
        private void BindDataToController(ControllerData controllerData, ControllerToken controllerToken)
        {
            //if (controllerData is CinematicControllerData)
            //    BindDataToController((CinematicControllerData)controllerData, (ControllerToken<CinematicControllerData>)controllerToken);

            //else if (controllerData is VideoControllerData)
            //    BindDataToController((VideoControllerData)controllerData, (ControllerToken<VideoControllerData>)controllerToken);

            //else if (controllerData is DialogueControllerData)
            //    BindDataToController((DialogueControllerData)controllerData, (ControllerToken<DialogueControllerData>)controllerToken);

            //else if (controllerData is FirstHideoutControllerData)
            //    BindDataToController((FirstHideoutControllerData)controllerData, (ControllerToken<FirstHideoutControllerData>)controllerToken);

            //else if (controllerData is HideoutSettingsControllerData)
            //    BindDataToController((HideoutSettingsControllerData)controllerData, (ControllerToken<HideoutSettingsControllerData>)controllerToken);

            //else if (controllerData is HideoutQuestsControllerData)
            //    BindDataToController((HideoutQuestsControllerData)controllerData, (ControllerToken<HideoutQuestsControllerData>)controllerToken);

            //else if (controllerData is HideoutInventoryControllerData)
            //    BindDataToController((HideoutInventoryControllerData)controllerData, (ControllerToken<HideoutInventoryControllerData>)controllerToken);

            //else if (controllerData is HideoutLabControllerData)
            //    BindDataToController((HideoutLabControllerData)controllerData, (ControllerToken<HideoutLabControllerData>)controllerToken);

            //else if (controllerData is HideoutWaypointsControllerData)
            //    BindDataToController((HideoutWaypointsControllerData)controllerData, (ControllerToken<HideoutWaypointsControllerData>)controllerToken);

            //else if (controllerData is CombatSetupControllerData)
            //    BindDataToController((CombatSetupControllerData)controllerData, (ControllerToken<CombatSetupControllerData>)controllerToken);

            //else if (controllerData is BackgroundControllerData)
            //    BindDataToController((BackgroundControllerData)controllerData, (ControllerToken<BackgroundControllerData>)controllerToken);

            //else if (controllerData is CombatControllerData)
            //    BindDataToController((CombatControllerData)controllerData, (ControllerToken<CombatControllerData>)controllerToken);

            //else if (controllerData is LoginControllerData)
            //    BindDataToController((LoginControllerData)controllerData, (ControllerToken<LoginControllerData>)controllerToken);

            //else if (controllerData is ComicStripControllerData)
            //    BindDataToController((ComicStripControllerData)controllerData, (ControllerToken<ComicStripControllerData>)controllerToken);

            //else if (controllerData is MusicControllerData)
            //    BindDataToController((MusicControllerData)controllerData, (ControllerToken<MusicControllerData>)controllerToken);

            //else if (controllerData is LoadingScreenControllerData)
            //    BindDataToController((LoadingScreenControllerData)controllerData, (ControllerToken<LoadingScreenControllerData>)controllerToken);

            if (true)
                Debug.Log("Hellow");
            else
            {
                Debug.LogError(controllerData.GetType().ToString() + " <- ControllerData not yet supported!");
            }
        }

        //private void BindDataToController(CinematicControllerData controllerData, ControllerToken<CinematicControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(VideoControllerData controllerData, ControllerToken<VideoControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(DialogueControllerData controllerData, ControllerToken<DialogueControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(FirstHideoutControllerData controllerData, ControllerToken<FirstHideoutControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(HideoutSettingsControllerData controllerData, ControllerToken<HideoutSettingsControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(HideoutQuestsControllerData controllerData, ControllerToken<HideoutQuestsControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(HideoutInventoryControllerData controllerData, ControllerToken<HideoutInventoryControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(HideoutLabControllerData controllerData, ControllerToken<HideoutLabControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(HideoutWaypointsControllerData controllerData, ControllerToken<HideoutWaypointsControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(CombatSetupControllerData controllerData, ControllerToken<CombatSetupControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(BackgroundControllerData controllerData, ControllerToken<BackgroundControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(CombatControllerData controllerData, ControllerToken<CombatControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(ComicStripControllerData controllerData, ControllerToken<ComicStripControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(MusicControllerData controllerData, ControllerToken<MusicControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}

        //private void BindDataToController(LoadingScreenControllerData controllerData, ControllerToken<LoadingScreenControllerData> controllerToken)
        //{
        //    controllerToken.controllerDataEvent.Raise(controllerData);
        //}
        #endregion


        #region LoadController

        private ControllerToken LoadController(ControllerData controllerData)
        {
            //if (controllerData is CinematicControllerData)
            //    return LoadController((CinematicControllerData)controllerData);

            //else if (controllerData is VideoControllerData)
            //    return LoadController((VideoControllerData)controllerData);

            //else if (controllerData is DialogueControllerData)
            //    return LoadController((DialogueControllerData)controllerData);

            //else if (controllerData is FirstHideoutControllerData)
            //    return LoadController((FirstHideoutControllerData)controllerData);

            //else if (controllerData is HideoutSettingsControllerData)
            //    return LoadController((HideoutSettingsControllerData)controllerData);

            //else if (controllerData is HideoutQuestsControllerData)
            //    return LoadController((HideoutQuestsControllerData)controllerData);

            //else if (controllerData is HideoutInventoryControllerData)
            //    return LoadController((HideoutInventoryControllerData)controllerData);

            //else if (controllerData is HideoutLabControllerData)
            //    return LoadController((HideoutLabControllerData)controllerData);

            //else if (controllerData is HideoutWaypointsControllerData)
            //    return LoadController((HideoutWaypointsControllerData)controllerData);

            //else if (controllerData is CombatSetupControllerData)
            //    return LoadController((CombatSetupControllerData)controllerData);

            //else if (controllerData is CombatSetupControllerData)
            //    return LoadController((CombatSetupControllerData)controllerData);

            //else if (controllerData is BackgroundControllerData)
            //    return LoadController((BackgroundControllerData)controllerData);

            //else if (controllerData is CombatControllerData)
            //    return LoadController((CombatControllerData)controllerData);

            //else if (controllerData is WitnessCogHackControllerData)
            //    return LoadController((WitnessCogHackControllerData)controllerData);

            //else if (controllerData is LoginControllerData)
            //    return LoadController((LoginControllerData)controllerData);

            //else if (controllerData is ComicStripControllerData)
            //    return LoadController((ComicStripControllerData)controllerData);

            //else if (controllerData is MusicControllerData)
            //    return LoadController((MusicControllerData)controllerData);

            //else if (controllerData is LoadingScreenControllerData)
            //    return LoadController((LoadingScreenControllerData)controllerData);


            if (true)
                return null;
            else
            {
                Debug.LogError(controllerData.GetType().ToString() + " <- ControllerData not yet supported!");
                return null;
            }
        }

        //private ControllerToken LoadController(CinematicControllerData data)
        //{
        //    var loader = new LoadScene<CinematicControllerData, CinematicController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(VideoControllerData data)
        //{
        //    var loader = new LoadScene<VideoControllerData, VideoController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(DialogueControllerData data)
        //{
        //    var loader = new LoadScene<DialogueControllerData, DialogueController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(FirstHideoutControllerData data)
        //{
        //    var loader = new LoadScene<FirstHideoutControllerData, FirstHideoutController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(HideoutSettingsControllerData data)
        //{
        //    var loader = new LoadScene<HideoutSettingsControllerData, HideoutSettingsController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(HideoutQuestsControllerData data)
        //{
        //    var loader = new LoadScene<HideoutQuestsControllerData, HideoutQuestsController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(HideoutInventoryControllerData data)
        //{
        //    var loader = new LoadScene<HideoutInventoryControllerData, HideoutInventoryController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(HideoutLabControllerData data)
        //{
        //    var loader = new LoadScene<HideoutLabControllerData, HideoutLabController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(HideoutWaypointsControllerData data)
        //{
        //    var loader = new LoadScene<HideoutWaypointsControllerData, HideoutWaypointsController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(CombatSetupControllerData data)
        //{
        //    var loader = new LoadScene<CombatSetupControllerData, CombatSetupController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(BackgroundControllerData data)
        //{
        //    var loader = new LoadScene<BackgroundControllerData, BackgroundController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(CombatControllerData data)
        //{
        //    var loader = new LoadScene<CombatControllerData, CombatController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(WitnessCogHackControllerData data)
        //{
        //    var loader = new LoadScene<WitnessCogHackControllerData, WitnessCogHackController>(data);
        //    return loader.waitForToken.controllerToken;
        //}


        //private ControllerToken LoadController(LoginControllerData data)
        //{
        //    var loader = new LoadScene<LoginControllerData, LoginController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(ComicStripControllerData data)
        //{
        //    var loader = new LoadScene<ComicStripControllerData, ComicStripController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(MusicControllerData data)
        //{
        //    var loader = new LoadScene<MusicControllerData, MusicController>(data);
        //    return loader.waitForToken.controllerToken;
        //}

        //private ControllerToken LoadController(LoadingScreenControllerData data)
        //{
        //    var loader = new LoadScene<LoadingScreenControllerData, LoadingScreenController>(data);
        //    return loader.waitForToken.controllerToken;
        //}



        #endregion


        private List<string> GetTriggers()
        {
            var parameters = animator.parameters;
            var triggers = new List<string>();
            foreach (var currParam in parameters)
            {
                if (currParam.type == AnimatorControllerParameterType.Trigger)
                {
                    triggers.Add(currParam.name);
                }
            }
            return triggers;
        }

        private void NavigationMapView_OnMessage(string message)
        {
            NavigateFromTrigger(message);
        }

    }
}