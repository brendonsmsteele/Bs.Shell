using System.Collections.Generic;
namespace Bs.Shell.Navigation
{
    /// <summary>
    /// Aggregates SceneControllerModel(s)
    /// </summary>
    public class NavigationPage
    {
        public List<Model> models;
        public NavigationPage(List<Model> models)
        {
            this.models = models;
        }
    }
}