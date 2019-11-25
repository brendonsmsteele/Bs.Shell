using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Bs.Shell.Navigation
{
    /// <summary>
    /// DEPRECATED
    /// </summary>
    [CreateAssetMenu(fileName =nameof(NavigationPageGenerator), menuName = "Bs.Shell/Navigation/"+ nameof(NavigationPageGenerator))]
    public class NavigationPageGenerator : ScriptableObject
    {
        [SerializeField] string pathToControllers = "NavigationMap/Controllers";
        [SerializeField] string pathToOutputFolder = "NavigationMap/Pages";

        public void Generate()
        {
            GenerateAllPagesFromControllers();
        }

        private void GenerateAllPagesFromControllers()
        {
            Watchdog.Stop();
            Watchdog.Start(CreateAllPagesRoutine());
        }

        CoroutineWatchdog Watchdog = new CoroutineWatchdog();
        private IEnumerator CreateAllPagesRoutine()
        {
            var rootDirectory = Path.Combine(Application.streamingAssetsPath, pathToControllers);
            var directories = Directory.GetDirectories(rootDirectory);
            for (int i = 0; i < directories.Length; i++)
            {
                var directory = directories[i];
                //var shortDirectory = directory.Substring(Application.streamingAssetsPath.Count());
                var waitForObjects = new GetAllObjectsFromFolderAsync(directory);
                yield return waitForObjects;
                var objects = waitForObjects.Objects;
                var controllers = objects.Cast<Model>().ToArray();
                var name = directory.Split('\\').LastOrDefault();
                var navPage = CreateNavigationPage(controllers, name);
                CreateNavigationPageAsset(navPage);
            }
            AssetDatabase.SaveAssets();
        }

        private class GetAllObjectsFromFolderAsync : CustomYieldInstruction
        {
            public override bool keepWaiting
            {
                get
                {
                    return Objects == null;
                }
            }

            public List<Object> Objects;

            public GetAllObjectsFromFolderAsync(string path)
            {
                RunCoroutine.Instance.StartCoroutine(Routine(path));
            }

            private IEnumerator Routine(string path)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                FileInfo[] allFiles = directoryInfo.GetFiles("*.*");
                List<Object> loadingObjects = new List<Object>();
                foreach (var file in allFiles)
                {
                    if (file.Name.Contains("meta"))
                        continue;

                    var getObject = new GetObjectFromPathAsync(file);
                    yield return getObject;
                    loadingObjects.Add(getObject.Object);
                }
                Objects = loadingObjects;
            }
        }

        private class GetObjectFromPathAsync : CustomYieldInstruction
        {
            public override bool keepWaiting { get { return Object == null; } }

            public Object Object = null;

            public GetObjectFromPathAsync(FileInfo fileInfo)
            {
                RunCoroutine.Instance.StartCoroutine(Routine(fileInfo));
            }

            private IEnumerator Routine(FileInfo fileInfo)
            {
                string fileWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.ToString());
                string[] nameData = fileWithoutExtension.Split(" "[0]);
                //3
                string tempPlayerName = "";
                int i = 0;
                foreach (string stringFromFileName in nameData)
                {
                    if (i != 0)
                    {
                        tempPlayerName = tempPlayerName + stringFromFileName + " ";
                    }
                    i++;
                }
                //4
                string wwwPlayerFilePath = "file://" + fileInfo.FullName.ToString();

                UnityWebRequest request = new UnityWebRequest(wwwPlayerFilePath);
                yield return request;
                //5
                //Object = (Object)request.Get;
            }

        }

        /*
         * 
         * var uwr = UnityWebRequestAssetBundle.GetAssetBundle("http://myserver/myBundle.unity3d");
        yield return uwr.SendWebRequest();

        // Get an asset from the bundle and instantiate it.
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
        var loadAsset = bundle.LoadAssetAsync<GameObject>("Assets/Players/MainPlayer.prefab");
        yield return loadAsset;

         * */

        private NavigationPage CreateNavigationPage(Model[] models, string name)
        {
            var navPage = NavigationPage.Create(models);
            navPage.name = name;
            return navPage;
        }

        private NavigationPage CreateNavigationPageAsset(NavigationPage navPage)
        {
            var outputPath = Application.streamingAssetsPath + pathToOutputFolder + navPage.name + ".asset";
            AssetDatabase.CreateAsset(navPage, outputPath);
            return navPage;
        }

    }
}