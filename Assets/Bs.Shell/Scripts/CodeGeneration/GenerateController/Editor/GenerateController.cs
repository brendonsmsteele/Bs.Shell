using UnityEngine;

namespace Bs.Shell.CodeGeneration
{
    [CreateAssetMenu(fileName = nameof(GenerateController), menuName = Shell.Menu.Paths.CODEGENERATION + nameof(GenerateController))]
    public class GenerateController : Generate, IGenerate
    {
        [SerializeField] Object exampleController;
        [SerializeField] Object exampleControllerDataEvent;

        public void Generate()
        {
            GenerateTemplate(@namespace, exampleController);
            GenerateTemplate(@namespace, exampleControllerDataEvent);
        }
    }
}
