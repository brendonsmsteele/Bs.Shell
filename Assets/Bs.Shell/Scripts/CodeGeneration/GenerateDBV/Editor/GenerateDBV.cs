using UnityEngine;

namespace Bs.Shell.CodeGeneration
{
    [CreateAssetMenu(fileName = nameof(GenerateDBV), menuName = "Bs.Shell/App/CodeGeneration/" + nameof(GenerateDBV))]
    public class GenerateDBV : Generate, IGenerate
    {
        [SerializeField] string name;
        [SerializeField] Object exampleDBV;
        [SerializeField] Object exampleItem;

        public void Generate()
        {
            string nameSansController = name.Replace("DBV", "");
            string cleanedName = nameSansController + "DBV";
            GenerateTemplate(cleanedName, exampleDBV);
            GenerateTemplate(cleanedName, exampleItem);
        }
    }
}
