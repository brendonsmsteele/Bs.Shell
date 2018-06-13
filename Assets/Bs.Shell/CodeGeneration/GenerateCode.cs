using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class GenerateCode : ScriptableObject
{
    public string stringToUserp;
    public string fileName;

    public void GenerateUIScript()
    {
        GenerateTemplate(fileName, "TYPEUI");
        GenerateTemplate(fileName + "DataEventEditor", "TYPEUIDataEventEditor", "Generated\\Editor");
    }

    public void GenerateButtonEditorScript()
    {
        GenerateTemplate(fileName, "TYPEButtonEditor", "Generated\\Editor");
    }

    void GenerateTemplate(string newName, string templateName, string outputPath = "Generated")
    {
        //  Read Template
        string thisPath = AssetDatabase.GetAssetPath(this);
        string parentPath = Directory.GetParent(thisPath).FullName;
        string templatePath = Path.Combine(parentPath, "Templates\\" + templateName) + ".txt";
        string template = File.ReadAllText(templatePath);
        //  Replace Variable
        string result = template.Replace("TEMPLATE", stringToUserp);
        //  Output result and create directory if needed
        string generatedCodePath = Path.Combine(parentPath, outputPath);
        if (!Directory.Exists(generatedCodePath))
            Directory.CreateDirectory(generatedCodePath);
        generatedCodePath = Path.Combine(generatedCodePath, newName) + ".cs";
        File.WriteAllText(generatedCodePath, result);
        // Refresh the asset database to show the (potentially) new file
        AssetDatabase.Refresh();
    }
}