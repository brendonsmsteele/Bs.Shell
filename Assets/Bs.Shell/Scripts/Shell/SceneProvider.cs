using UnityEngine;

namespace Nc.Shell.Navigation
{
    public abstract class SceneProvider : StateMachineBehaviour
    {
        public abstract SceneControllerModel GetModel();
    }
}