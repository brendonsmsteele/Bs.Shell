using System.IO;
using UnityEditor;
using UnityEngine;

namespace Nc.Shell.CodeGeneration
{
    public abstract class Generate : ScriptableObject
    {
        [SerializeField] protected string @namespace;

        protected void GenerateTemplate(string cleanedName, Object templateReference, string outputPath = "")
        {
            string pathOfUnityObject = GetPathOfUnityObject(templateReference);
            string template = ReadTextOfUnityObject(pathOfUnityObject);
            string result = ReplaceAllInstancesOfExampleWithCleanName(template, cleanedName);
            string outputFolder = FindOutputFolder("Generated/" + cleanedName + outputPath);
            string finalName = GenerateFinalName(cleanedName, templateReference.name);
            string finalPath = GenerateFinalPath(outputFolder, finalName);
            WriteTheFileToText(finalPath, result);
        }

        protected string GetPathOfUnityObject(Object templateReference)
        {
            return AssetDatabase.GetAssetPath(templateReference);
        }

        protected string ReadTextOfUnityObject(string path)
        {
            return File.ReadAllText(path);
        }

        protected string ReplaceAllInstancesOfExampleWithCleanName(string templateText, string cleanedName)
        {
            return templateText.Replace("Example", cleanedName);
        }

        protected string GetPathOfParent()
        {
            string thisPath = AssetDatabase.GetAssetPath(this);
            string parentPath = Directory.GetParent(thisPath).FullName;
            return parentPath;
        }

        protected string FindOutputFolder(string outputPath)
        {
            string pathToOutputFolder = Path.Combine(GetPathOfParent(), outputPath);
            if (!Directory.Exists(pathToOutputFolder))
                Directory.CreateDirectory(pathToOutputFolder);
            return pathToOutputFolder;
        }

        protected string GenerateFinalName(string cleanedName, string templateReferenceName)
        {
            return templateReferenceName.Replace("Example", cleanedName);
        }

        protected string GenerateFinalPath(string pathToOutputFolder, string cleanedName)
        {
            return Path.Combine(pathToOutputFolder, cleanedName) + ".cs";
        }

        protected void WriteTheFileToText(string path, string result)
        {
            File.WriteAllText(path, result);
            AssetDatabase.Refresh();
        }
    }

}
