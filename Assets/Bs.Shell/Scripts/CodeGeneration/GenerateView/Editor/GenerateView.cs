using UnityEngine;

namespace Bs.Shell.CodeGeneration
{
    [CreateAssetMenu(fileName = nameof(GenerateView), menuName = "Bs.Shell/App/CodeGeneration/" + nameof(GenerateView))]
    public class GenerateView : Generate, IGenerate
    {
        [SerializeField] Object exampleView;

        public void Generate()
        {
            GenerateTemplate(@namespace, exampleView);
        }
    }
}
