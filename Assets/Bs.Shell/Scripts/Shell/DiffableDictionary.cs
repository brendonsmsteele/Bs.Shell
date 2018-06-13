using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Bs.Shell
{
    public class DiffableDictionary<TData, TComponent>
    where TData : class
    where TComponent : class
    {
        Func<TData, TComponent> add;
        Action<TData, TComponent, int> update;
        Action<TData, TComponent> remove;
        Action complete;

        OrderedDictionary cache;

        public DiffableDictionary(Func<TData, TComponent> add, Action<TData, TComponent, int> update, Action<TData, TComponent> remove, Action complete)
        {
            this.add = add;
            this.update = update;
            this.remove = remove;
            this.complete = complete;
        }

        public void Update(TData[] newDataArray)
        {
            Update(newDataArray.ToList());
        }

        public void Update(List<TData> newData)
        {
            if (cache == null) cache = new OrderedDictionary();
            ICollection keys = cache.Keys;
            List<TData> cacheKeys = new List<TData>();
            foreach (var key in keys)
                cacheKeys.Add((TData)key);
            ICollection values = cache.Values;
            List<TComponent> cacheValues = new List<TComponent>();
            foreach (var val in values)
                cacheValues.Add((TComponent)val);
            OrderedDictionary newOrderedDictionary = new OrderedDictionary();

            //  Remove
            for (int i = cacheKeys.Count - 1; i >= 0; i--)
            {
                TData ck = cacheKeys[i];
                if (!newData.Contains(ck))
                {
                    remove.Invoke(cacheKeys[i], cacheValues[i]);    //  Remove
                    cacheKeys.RemoveAt(i);
                    cacheValues.RemoveAt(i);
                }
            }

            //  Add
            for (int i = 0; i < newData.Count; i++)
            {
                TData nd = newData[i];
                if (!cacheKeys.Contains(nd))
                {
                    cacheKeys.Add(nd);
                    cacheValues.Add(add.Invoke(nd));    //  Add
                }
            }

            //  Update
            for (int i = 0; i < newData.Count; i++)
            {
                TData a = newData[i];
                TComponent b = cacheValues[cacheKeys.IndexOf(a)];
                update.Invoke(a, b, i);
                newOrderedDictionary.Add(a, b);
            }

            //  Complete
            complete.Invoke();

            cache = newOrderedDictionary;
        }

        public void Clear()
        {
            if (cache == null || cache.Count == 0) return;

            TData[] keys = GetKeys();
            TComponent[] values = new TComponent[keys.Length];
            cache.Values.CopyTo(values, 0);
            for (int i = 0; i < keys.Length; i++)
            {
                TData key = keys[i];
                TComponent val = values[i];
                remove.Invoke(key, val);
            }
            cache.Clear();
        }


        #region Get
        public TData[] GetKeys()
        {
            if (cache == null) return null;

            TData[] keys = new TData[cache.Count];
            cache.Keys.CopyTo(keys, 0);
            return keys;
        }

        public TComponent GetValue(int index)
        {
            if (cache == null || index >= cache.Count) return null;

            return (TComponent)cache[index];
        }

        public TComponent GetValue(TData key)
        {
            if (cache == null || !cache.Contains(key)) return null;

            return (TComponent)cache[key];
        }

        public int Count()
        {
            if (cache == null) return -1;

            return cache.Keys.Count;
        }
        #endregion
    }
}