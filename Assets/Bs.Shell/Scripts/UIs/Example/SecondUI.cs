using Bs.Shell.Examples;
using Bs.Shell.ScriptableObjects;
using UnityEngine;

namespace Bs.Shell.UI
{
    public class SecondUI : UIBase<SecondUIData>
	{
        public DiffableDictionary<InterpolateReference, Item> items;
        GameObject prefab;

        protected override void Start()
        {
            items = new DiffableDictionary<InterpolateReference, Item>
                (
                    (data) =>
                    {   //  Add
                        GameObject go = Instantiate(prefab);
                        go.transform.SetParent(this.transform);
                        Item ti = go.GetComponent<Item>();
                        return ti;
                    },
                    (data, item, index) =>
                    {   //  Update
                        item.Bind(data);
                        item.transform.SetSiblingIndex(index);
                    },
                    (data, item) =>
                    {   //  Remove
                        Destroy(item.gameObject);
                    },
                    () => Refresh()
                );
        }

		public override void Bind(SecondUIData data)
		{
            prefab = data.prefab;
            if (prefab == null || data.interpolateRefSet.Items == null || data.interpolateRefSet.Items.Count == 0)
                items.Clear();
            else
                items.Update(data.interpolateRefSet.Items);
		}
	}
}