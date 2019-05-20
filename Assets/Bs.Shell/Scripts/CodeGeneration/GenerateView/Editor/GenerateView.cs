using UnityEngine;

namespace Bs.Shell.CodeGeneration
{
    [CreateAssetMenu(fileName = nameof(GenerateView), menuName = "Bs.Shell/App/CodeGeneration/" + nameof(GenerateView))]
    public class GenerateView : Generate, IGenerate
    {
        [SerializeField] string name;
        [SerializeField] Object exampleView;

        public void Generate()
        {
            string nameSansController = name.Replace("View", "");
            string cleanedName = nameSansController + "View";
            GenerateTemplate(cleanedName, exampleView);
        }
    }
}
