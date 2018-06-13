using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bs.Shell.Examples
{
    public class TestData : ScriptableObject
    {
        public Guid id;
        public Color color;
    }

    public class TestDiffableDictionary : MonoBehaviour
    {

        public DiffableDictionary<TestData, Item> items;
        public GameObject prefab;

        // Use this for initialization
        void Start()
        {
            items = new DiffableDictionary<TestData, Item>
                (
                    (data) =>
                    {   //  Add
                    Debug.Log("Add");
                        GameObject go = Instantiate(prefab);
                        go.transform.SetParent(this.transform);
                        Item ti = go.GetComponent<Item>();
                        return ti;
                    },
                    (data, item, index) =>
                    {   //  Update
                    Debug.Log("Update");

                        item.Bind(data);
                        item.transform.SetSiblingIndex(index);
                    },
                    (data, item) =>
                    {   //  Remove
                    Debug.Log("Remove");

                        Destroy(item.gameObject);
                    },
                    () => Refresh()
                );
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Refresh()
        {
            Debug.Log("Refresh");
        }

        public void CreateNewListFromScratch()
        {
            List<TestData> newData = new List<TestData>();
            for (int i = 0; i < 4; i++)
            {
                TestData data = ScriptableObject.CreateInstance<TestData>();
                data.id = Guid.NewGuid();
                data.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
                newData.Add(data);
            }
            items.Update(newData);
        }

        public void RemoveOneAtRandom()
        {
            int randIndex = UnityEngine.Random.Range(0, items.Count());
            List<TestData> keys = items.GetKeys().ToList();
            keys.RemoveAt(randIndex);
            items.Update(keys);
        }

        public void Shuffle()
        {
            List<TestData> keys = items.GetKeys().ToList();
            keys.Shuffle();
            items.Update(keys);
        }

        public void Clear()
        {
            items.Clear();
        }

        public void ChangeColors()
        {
            List<TestData> keys = items.GetKeys().ToList();
            for (int i = 0; i < keys.Count; i++)
                keys[i].color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
            items.Update(keys);
        }
    }

}
