using Bs.Shell.ScriptableObjects;
using UnityEngine;

namespace Bs.Shell.UI
{
	public class FirstUI : UIBase<FirstUIData>
	{
		// Use this for initialization
		protected override void Start () {
		
		}

		// Update is called once per frame
		protected override void Update () {
		
		}

		public override void Bind(FirstUIData data)
		{
            Debug.Log(data.message);
		}
	}
}