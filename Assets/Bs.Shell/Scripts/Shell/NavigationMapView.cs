using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    public class NavigationMapViewModel
    {
        public List<string> Triggers;
        public bool Show;

        public NavigationMapViewModel(List<string> Triggers, bool Show)
        {
            this.Triggers = Triggers;
            this.Show = Show;
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null || obj.GetType() != typeof(NavigationMapViewModel))
        //        return false;

        //    NavigationMapViewModel other = (NavigationMapViewModel)obj;
        //    return other.Message == this.Message;
        //}
    }

    public class NavigationMapView : View<NavigationMapViewModel>
    {
        [SerializeField] NavigationMapDBV navigationMapDBV;
        [SerializeField] CanvasGroup canvasGroup;

        public event DelegateMessage OnMessage;

        public override void Refresh()
        {
            var data = ViewModel.Triggers.Select(x => { return new NavigationMapItemViewModel(x); }).ToList();
            navigationMapDBV.Bind(data);
            canvasGroup.alpha = ViewModel.Show ? 1f : 0f;
            canvasGroup.interactable = ViewModel.Show;
            canvasGroup.blocksRaycasts = ViewModel.Show;
        }

        protected override void AddEventListeners()
        {
            navigationMapDBV.OnMessage += NavigationMapDBV_OnMessage;
        }

        protected override void RemoveEventListeners()
        {
            navigationMapDBV.OnMessage -= NavigationMapDBV_OnMessage;
        }

        private void NavigationMapDBV_OnMessage(string message)
        {
            OnMessage?.Invoke(message);
        }


        #region Events

        #endregion
    }
}