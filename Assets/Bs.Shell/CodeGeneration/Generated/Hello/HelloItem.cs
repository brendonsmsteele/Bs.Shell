using System;
using UnityEngine;

namespace Bs.Shell.Hello
{
    [Serializable]
    public class HelloItemViewModel
    {
        public string Message; 

        public HelloItemViewModel(string message)
        {
            this.Message = message;
        }
        //public override bool Equals(object obj)
        //{
        //    if (obj == null || obj.GetType() != typeof(HelloItemViewModel))
        //        return false;

        //    HelloItemViewModel other = (HelloItemViewModel)obj;
        //    return other.Message == this.Message;
        //}
    }
    public class HelloItem : View<HelloItemViewModel>
    {
        public override void Refresh()
        {
            Debug.Log(ViewModel.Message);
        }

        protected override void AddEventListeners()
        {
        }

        protected override void RemoveEventListeners()
        {
        }
    }
}
