using Bs.Shell.UI;
using UnityEngine;

namespace Bs.Shell
{
    public class Main : MonoBehaviour
    {
        void Start()
        {
            //  Init the Shell
            var app = ScriptableObject.CreateInstance<Shell.App>();
            app.Init();
            //  Load first UI ~ Do this to register with the shell.
            Shell.App.Instance.LoadUIAsync<FirstUIData, FirstUI>(   "FirstUI",
                                                                    null,
                                                                    null,
                                                                    UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

}