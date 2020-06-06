using System;
using UnityEngine;

namespace Bs.Shell.Views
{
    public class ExampleView : ViewController<ExampleView.Model>
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
        }

        protected override void OnEnable()
        {
        }

        protected override void OnDisable()
        {
        }
    }
}