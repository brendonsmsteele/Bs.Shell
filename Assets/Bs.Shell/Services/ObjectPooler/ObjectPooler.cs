using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1.  Configure all prefabs you want to be poolable.
/// 2.  Give each prefab you want to pool a unique tag.
/// 3.  Use the Get and Put functions.
/// 4.  Pooler will automatically call Clean() on any ICleanable.
/// </summary>
namespace Bs.Shell
{
    [CreateAssetMenu(menuName = "Bs.Shell/Pool/" + nameof(ObjectPooler))]
    public class ObjectPooler : ScriptableObject, IInit
    {
        public static ObjectPooler Instance;

        public List<ObjectPoolItem> itemsToPool;
        public List<GameObject> pooledObjects;

        public void Init()
        {
            Instance = this;
            //  Init
            pooledObjects = new List<GameObject>();
            foreach (ObjectPoolItem item in itemsToPool)
            {
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
            }
        }

        public GameObject Get(string tag)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
                {
                    return pooledObjects[i];
                }
            }
            foreach (ObjectPoolItem item in itemsToPool)
            {
                if (item.objectToPool.tag == tag)
                {
                    if (item.shouldExpand)
                    {
                        GameObject obj = (GameObject)Instantiate(item.objectToPool);
                        obj.SetActive(false);
                        pooledObjects.Add(obj);
                        return obj;
                    }
                }
            }
            return null;
        }

        public void Put(GameObject gameObject)
        {
            ICleanable c = gameObject.GetComponent<ICleanable>();
            if (c != null)
                c.Clean();
            gameObject.SetActive(false);
        }
    }


}

