using UnityEngine;

/// <summary>
/// Add on your own services!
/// </summary>
namespace Bs.Shell.Example
{
    [CreateAssetMenu(fileName = nameof(ExampleServices), menuName = "Bs.Shell/Example/" + nameof(ExampleServices))]
    public class ExampleServices : ShellServices
    {
        //  Add your own services.
        public override void Init()
        {
            Debug.Log("Init my services thanks.");
            base.Init();
        }
    }
}

