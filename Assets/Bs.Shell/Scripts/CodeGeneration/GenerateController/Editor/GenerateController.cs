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
            GenerateTemplate(@namespace, exampleController);
            GenerateTemplate(@namespace, exampleControllerData);
            GenerateTemplate(@namespace, exampleControllerDataEvent);
            GenerateTemplate(@namespace, exampleControllerDataEventEditor, "/Editor");
        }
    }
}
