using UnityEngine;
using UnityEngine.UI;

namespace Bs.Shell.Navigation
{
    public delegate void DelegateMessage(string message);
    public class NavigationMapItemViewModel
    {
        public string Message; 

        public NavigationMapItemViewModel(string message)
        {
            this.Message = message;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(NavigationMapItemViewModel))
                return false;

            NavigationMapItemViewModel other = (NavigationMapItemViewModel)obj;
            return other.Message == this.Message;
        }
    }
    public class NavigationMapItem : View<NavigationMapItemViewModel>
    {
        [SerializeField] Text text;
        [SerializeField] Button button;
        public event DelegateMessage OnMessage;

        public override void Refresh()
        {
            //Debug.Log(Model.Message);
            text.text = model.Message;
        }

        protected override void AddEventListeners()
        {
            button.onClick.AddListener(HandleButtonClick);
        }

        protected override void RemoveEventListeners()
        {
            button.onClick.RemoveAllListeners();
        }

        private void HandleButtonClick()
        {
            OnMessage?.Invoke(model.Message);
        }

    }
}
