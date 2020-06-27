using Nc.Shell.UI;

namespace Nc.Shell.Navigation
{
    public class ExampleSceneProvider : SceneProvider
    {
        public override SceneControllerModel GetModel()
        {
            var model = new ExampleSceneController.Model();
            return model;
        }
    }
}