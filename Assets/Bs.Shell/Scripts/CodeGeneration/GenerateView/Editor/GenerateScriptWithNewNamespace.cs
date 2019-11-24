using UnityEngine;

namespace Bs.Shell.CodeGeneration
{
    [CreateAssetMenu(fileName = nameof(GenerateScriptWithNewNamespace), menuName = "Bs.Shell/App/CodeGeneration/" + nameof(GenerateScriptWithNewNamespace))]
    public class GenerateScriptWithNewNamespace : Generate, IGenerate
    {
        [SerializeField] Object exampleView;

        public void Generate()
        {
            GenerateTemplate(@namespace, exampleView);
        }
    }
}
