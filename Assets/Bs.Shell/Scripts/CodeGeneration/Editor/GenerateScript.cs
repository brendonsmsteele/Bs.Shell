using UnityEngine;

namespace Bs.Shell.CodeGeneration
{
    [CreateAssetMenu(fileName = nameof(GenerateScript), menuName = "Bs.Shell/App/CodeGeneration/" + nameof(GenerateScript))]
    public class GenerateScript : Generate, IGenerate
    {
        [SerializeField] Object exampleView;

        public void Generate()
        {
            GenerateTemplate(@namespace, exampleView);
        }
    }
}
