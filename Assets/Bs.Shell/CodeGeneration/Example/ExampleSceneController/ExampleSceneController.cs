using System;

namespace Nc.Shell.UI
{
    public class ExampleSceneController : SceneController<ExampleSceneController.Model>
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
    }
}
