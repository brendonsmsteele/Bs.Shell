using System;
using UnityEngine;

namespace Bs.Shell.Example
{
    [Serializable]
    public class ExampleItemViewModel
    {
        public string Message; 

        public ExampleItemViewModel(string message)
        {
            this.Message = message;
        }
        //public override bool Equals(object obj)
        //{
        //    if (obj == null || obj.GetType() != typeof(ExampleItemViewModel))
        //        return false;

        //    ExampleItemViewModel other = (ExampleItemViewModel)obj;
        //    return other.Message == this.Message;
        //}
    }
    public class ExampleItem : View<ExampleItemViewModel>
    {
        public override void Refresh()
        {
            Debug.Log(model.Message);
        }

        protected override void AddEventListeners()
        {
        }

        protected override void RemoveEventListeners()
        {
        }
    }
}
