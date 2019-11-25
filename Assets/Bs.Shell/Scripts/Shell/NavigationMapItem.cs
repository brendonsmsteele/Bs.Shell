using UnityEngine;
using UnityEngine.UI;

namespace Bs.Shell.Navigation
{
    public delegate void DelegateMessage(string message);
    
    public class NavigationMapItem : View<NavigationMapItem.Model>
    {
        public class Model : Shell.Model
        {
            public string Message;

            public Model(string message)
            {
                this.Message = message;
            }

            public override bool Equals(object obj)
            {
                if (obj == null || obj.GetType() != typeof(Model))
                    return false;

                Model other = (Model)obj;
                return other.Message == this.Message;
            }
        }

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
