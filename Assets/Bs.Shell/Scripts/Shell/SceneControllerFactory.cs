using Bs.Shell.Controllers;
using System;

namespace Bs.Shell
{
    public static class SceneControllerFactory
    {
        public static SceneControllerToken LoadScene(Model model)
        {
            if(model.GetType() == typeof(ExampleController.Model))
                return LoadScene((ExampleController.Model)model);
            else
                throw new NotImplementedException($"Missing unbox for model of type {model.GetType()}");
        }

        public static SceneControllerToken LoadScene<TModel>(TModel model)
            where TModel : Model
        {
            return new LoadScene<TModel>().waitForToken.controllerToken;
        }

        public static void SetModel(Model model, SceneControllerToken sceneControllerToken)
        {
            if (model.GetType() == typeof(ExampleController.Model))
                SetModel((ExampleController.Model)model, sceneControllerToken);
            else
                throw new NotImplementedException($"Missing unbox for model of type {model.GetType()}");
        }

        public static void SetModel<TModel>(TModel model, SceneControllerToken token)
            where TModel : Model
        {
            if (token.GetType() != typeof(SceneControllerToken<TModel>))
                throw new Exception($"Model doesn't match token.  model: {model.GetType()} token: {token.GetType()}");

            var unpackToken = (SceneControllerToken<TModel>)token;
            unpackToken.SetModel(model);
        }
    }
}
