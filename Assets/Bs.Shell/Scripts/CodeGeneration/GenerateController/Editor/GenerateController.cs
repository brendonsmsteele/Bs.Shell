using UnityEngine;

namespace Bs.Shell.CodeGeneration
{
    [CreateAssetMenu(fileName = nameof(GenerateController), menuName = "Bs.Shell/App/CodeGeneration/" + nameof(GenerateController))]
    public class GenerateController : Generate, IGenerate
    {
        [SerializeField] Object exampleController;
        [SerializeField] Object exampleControllerData;
        [SerializeField] Object exampleControllerDataEvent;
        [SerializeField] Object exampleControllerDataEventEditor;

        public void Generate()
        {
            string nameSansController = name.Replace("Controller", "");
            string cleanedName = nameSansController + "Controller";
            GenerateTemplate(cleanedName, exampleController);
            GenerateTemplate(cleanedName, exampleControllerData);
            GenerateTemplate(cleanedName, exampleControllerDataEvent);
            GenerateTemplate(cleanedName, exampleControllerDataEventEditor, "Generated\\Editor");
        }
    }
}
