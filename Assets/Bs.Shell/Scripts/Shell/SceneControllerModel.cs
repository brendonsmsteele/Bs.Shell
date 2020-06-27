using System;

namespace Nc.Shell
{
    [Serializable]
    public abstract class SceneControllerModel : Model
    {
        public abstract SceneControllerToken LoadScene();
    }
}