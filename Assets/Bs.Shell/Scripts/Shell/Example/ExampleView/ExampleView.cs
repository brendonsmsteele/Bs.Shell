using System;
using UnityEngine;

namespace Bs.Shell.Views
{
    public class ExampleView : View<ExampleView.Model>
    {
        [Serializable]
        public class Model : Shell.Model
        {
            public Model()
            {
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


        #region Events

        #endregion
    }
}