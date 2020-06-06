using Bs.Shell.Controllers;

namespace Bs.Shell.Navigation
{
    public class ExampleSceneControllerModel : SceneControllerModel
    {
        public override Model GetModel()
        {
            var model = new ExampleController.Model();
            return model;
        }
    }
}
