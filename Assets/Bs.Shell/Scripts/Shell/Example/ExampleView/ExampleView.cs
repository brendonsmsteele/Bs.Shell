using System;
using UnityEngine;

namespace Bs.Shell.Example
{
    public class ExampleView : View<ExampleView.Model>
    {
        [Serializable]
        public class Model
        {
            public string Message;

            public Model(string Message)
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


        #region Events

        #endregion
    }
}