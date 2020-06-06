using UnityEngine;

namespace Bs.Shell.Navigation
{
    public abstract class SceneControllerModel : StateMachineBehaviour
    {
        public abstract Model GetModel();
    }
}