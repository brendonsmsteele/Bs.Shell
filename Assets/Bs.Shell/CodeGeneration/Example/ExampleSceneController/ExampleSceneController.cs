using Nc.Shell.Navigation;
using System;

namespace Nc.Shell.UI
{
    public class ExampleSceneController : SceneController<ExampleSceneController.Model>
    {
        [Serializable]
        public class Model : SceneControllerModel
        {
            public Model()
            {
            }

            public override SceneControllerToken LoadScene()
            {
                return App.Instance.LoadSceneControllerAsync<Model>(this);
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
