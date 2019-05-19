using Bs.Shell.Example;
using UnityEngine;

namespace Bs.Shell
{
    public static class LoadSceneFactory
    {
        public static ControllerToken LoadScene(ControllerData data)
        {
            if (data is ExampleControllerData)
                return new LoadScene<ExampleControllerData, ExampleController>((ExampleControllerData)data).waitForToken.controllerToken;

            Debug.LogError("LoadScene not yet supported for -> " + data.ToString());
            return null;
        }
    }
}
