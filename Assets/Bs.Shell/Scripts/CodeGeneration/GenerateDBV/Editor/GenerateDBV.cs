using UnityEngine;

namespace Bs.Shell.CodeGeneration
{
    [CreateAssetMenu(fileName = nameof(GenerateDBV), menuName = Shell.Menu.Paths.CODEGENERATION + nameof(GenerateDBV))]
    public class GenerateDBV : Generate, IGenerate
    {
        [SerializeField] Object exampleDBV;
        [SerializeField] Object exampleItem;

        public void Generate()
        {
            GenerateTemplate(@namespace, exampleDBV);
            GenerateTemplate(@namespace, exampleItem);
        }
    }
}
