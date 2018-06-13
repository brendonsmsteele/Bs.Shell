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
            //  When you call items.Update(myList);...  
            //  DiffableDictionary auto detects new, same, or missing items, and will call the functions that you pass in.
            //  new items call Add() then Update();
            //  same items call Update();
            //  missing items call Remove();
            //  Refresh() is called at the end of the entire process.  You may want to tell your items to animate or something.
            //  Notice you pass in your own functions!
            //  This is very good for instantiating prefabs based off of a list of data.
            //  DiffableDictionary gives you a Key/Value link between your data and your component.
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

        public override void Refresh()
        {
            Debug.Log("Complete");
        }
    }
}