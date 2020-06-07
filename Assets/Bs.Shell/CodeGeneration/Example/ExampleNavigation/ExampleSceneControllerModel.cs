using Nc.Shell.UI;

namespace Nc.Shell.Navigation
{
    public class ExampleSceneControllerModel : SceneControllerModel
    {
        public override Model GetModel()
        {
            var model = new ExampleSceneController.Model();
            return model;
        }
    }
}
