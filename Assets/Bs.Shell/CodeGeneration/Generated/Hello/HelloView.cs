using System;
using UnityEngine;

namespace Bs.Shell.Hello
{
    [Serializable]
    public class HelloViewModel
    {
        public string Message;

        public HelloViewModel(string Message)
        {
            this.Message = Message;
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null || obj.GetType() != typeof(HelloViewModel))
        //        return false;

        //    HelloViewModel other = (HelloViewModel)obj;
        //    return other.Message == this.Message;
        //}
    }

    public class HelloView : View<HelloViewModel>
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


        #region Events

        #endregion
    }
}