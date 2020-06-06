using Bs.Shell;
using Bs.Shell.Navigation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadAndGo : MonoBehaviour
{
    [SerializeField] ShellServices shellServices;
    public void Load()
    {
        Addressables.LoadAsset<NavigationPage>("NavigationPage").Completed += onLoadDone;
    }

    private void onLoadDone(AsyncOperationHandle<NavigationPage> page)
    {
        shellServices.NavigationMap.NavigateToPage(page.Result);
    }
}