using Bs.Shell.ScriptableObjects;
using UnityEngine;

namespace Bs.Shell.UI
{
	public class ItemUI : UIBase<ItemUIData>
	{
		// Use this for initialization
		protected override void Start () {
		
		}

		// Update is called once per frame
		protected override void Update () {
		
		}

		public override void Bind(ItemUIData data)
		{
            Debug.Log(data.message);
		}
	}
}