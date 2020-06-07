using UnityEngine;

namespace Nc.Shell.Navigation
{
    public abstract class SceneControllerModel : StateMachineBehaviour
    {
        public abstract Model GetModel();
    }
}