using UnityEngine;

namespace Nc.Shell.CodeGeneration
{
    [CreateAssetMenu(fileName = nameof(GenerateScript), menuName = "Nc.Shell/App/CodeGeneration/" + nameof(GenerateScript))]
    public class GenerateScript : Generate, IGenerate
    {
        [SerializeField] Object exampleView;

        public void Generate()
        {
            GenerateTemplate(@namespace, exampleView);
        }
    }
}
