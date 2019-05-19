using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GenerateCode), menuName = "Bs.Shell/App/"+nameof(GenerateCode))]
public class GenerateCode : ScriptableObject
{
    public string className;
    public string fileName;

    public void GenerateUIScript()
    {
        string classNameSansUI = className.Replace("UI", "");
        string fileNameSansUI = fileName.Replace("UI", "");
        fileNameSansUI += "UI";
        GenerateTemplate(classNameSansUI, fileNameSansUI, "TYPEUI");
        GenerateTemplate(classNameSansUI, fileNameSansUI + "Data", "TypeUIData");
        GenerateTemplate(classNameSansUI, fileNameSansUI + "DataEvent", "TypeUIDataEvent");
        GenerateTemplate(classNameSansUI, fileNameSansUI + "DataEventEditor", "TYPEUIDataEventEditor", "Generated\\Editor");
    }

    public void GenerateButtonEditorScript()
    {
        GenerateTemplate(fileName, "TYPEButtonEditor", "Generated\\Editor");
    }

    void GenerateTemplate(string cleanedClassName, string cleanedFileName, string templateName, string outputPath = "Generated")
    {
        //  Read Template
        string thisPath = AssetDatabase.GetAssetPath(this);
        string parentPath = Directory.GetParent(thisPath).FullName;
        string templatePath = Path.Combine(parentPath, "Templates\\" + templateName) + ".txt";
        string template = File.ReadAllText(templatePath);
        //  Replace Variable
        string result = template.Replace("TEMPLATE", cleanedClassName);
        //  Output result and create directory if needed
        string generatedCodePath = Path.Combine(parentPath, outputPath);
        if (!Directory.Exists(generatedCodePath))
            Directory.CreateDirectory(generatedCodePath);
        generatedCodePath = Path.Combine(generatedCodePath, cleanedFileName) + ".cs";
        File.WriteAllText(generatedCodePath, result);
        // Refresh the asset database to show the (potentially) new file
        AssetDatabase.Refresh();
    }
}