using Bs.Shell.ScriptableObjects;
using UnityEngine;

namespace Bs.Shell.UI
{
	public class SecondUI : UIBase<SecondUIData>
	{
		// Use this for initialization
		protected override void Start () {
		
		}

		// Update is called once per frame
		protected override void Update () {
		
		}

		public override void Bind(SecondUIData data)
		{
            Debug.Log(data.message);
		}
	}
}