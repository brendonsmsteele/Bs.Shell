using System;
using UnityEngine;

namespace Bs.Shell.Example
{
    public class ExampleItem : View<ExampleItem.Model>
    {
        [Serializable]
        public class Model : Shell.Model
        {
            public string Message;

            public Model(string message)
            {
                this.Message = message;
            }
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
    }
}
