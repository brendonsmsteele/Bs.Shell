using System;
using UnityEngine;

namespace Bs.Shell.Example
{
    [Serializable]
    public class ExampleViewModel
    {
        public string Message;

        public ExampleViewModel(string Message)
        {
            this.Message = Message;
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null || obj.GetType() != typeof(ExampleViewModel))
        //        return false;

        //    ExampleViewModel other = (ExampleViewModel)obj;
        //    return other.Message == this.Message;
        //}
    }

    public class ExampleView : View<ExampleViewModel>
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