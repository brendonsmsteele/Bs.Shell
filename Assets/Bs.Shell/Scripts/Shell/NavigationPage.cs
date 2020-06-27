using System.Collections.Generic;
namespace Nc.Shell.Navigation
{
    /// <summary>
    /// Aggregates SceneControllerModel(s)
    /// </summary>
    public class NavigationPage
    {
        public List<SceneControllerModel> models;
        public NavigationPage(List<SceneControllerModel> models)
        {
            this.models = models;
        }
    }
}