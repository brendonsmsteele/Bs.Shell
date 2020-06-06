using Bs.Shell.Controllers;
using UnityEngine;

namespace Bs.Shell
{
    public static class LoadSceneFactory
    {
        public static SceneControllerToken LoadScene(Model model)
        {
            if (model is ExampleController.Model)
                return new LoadScene<ExampleController.Model, ExampleController>((ExampleController.Model)model).waitForToken.controllerToken;

            Debug.LogError("LoadScene not yet supported for -> " + model.ToString());
            return null;
        }
    }
}
