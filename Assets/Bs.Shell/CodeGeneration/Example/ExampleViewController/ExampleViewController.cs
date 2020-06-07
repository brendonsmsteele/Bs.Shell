using System;

namespace Nc.Shell.UI
{
    public class ExampleViewController : ViewController<ExampleViewController.Model>, ICleanable
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

        public override void SetInteractable(bool interactable)
        {
        }

        public override void SetShow(bool show)
        {
        }

        public void Clean()
        {
        }
    }
}