using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

/// <summary>
/// You have a list of Data.
/// You want a list of components that represent that data.
/// DiffableDictionary holds internally an ordered dictionary that binds myData -> myComponent.
/// You pass in a list of data. 
/// DiffableDictionary will compare every modified list of data with the list from before, and determine whether it needs to be added, removed, or updated in the internal list.
/// This uses object comparison by default; however, you can override comparison mechanism via the IEqualityComparer in the constructor.
/// </summary>
namespace Bs.Shell
{
    public class DiffableDictionary<TData, TComponent>
    where TData : class
    where TComponent : class
    {
        /// <summary>
        /// Action for Add
        /// </summary>
        Func<TData, TComponent> add;
        /// <summary>
        /// Action for Update.
        /// </summary>
        Action<TData, TComponent, int> update;
        /// <summary>
        /// Action for Remove.
        /// </summary>
        Action<TData, TComponent> remove;
        /// <summary>
        /// Action for on complete.
        /// </summary>
        Action complete;
        /// <summary>
        /// Mechanism for determining equality.
        /// </summary>
        IEqualityComparer<TData> comparer;

        /// <summary>
        /// OrderedDictionary that makes the dream work.
        /// </summary>
        OrderedDictionary cache;

        //  Constructor
        public DiffableDictionary(Func<TData, TComponent> add, Action<TData, TComponent, int> update, Action<TData, TComponent> remove, Action complete, IEqualityComparer<TData> comparer = null)
        {
            this.add = add;
            this.update = update;
            this.remove = remove;
            this.complete = complete;
            this.comparer = comparer;
        }

        //  Destructor
        ~DiffableDictionary()
        {
            Clear();
        }

        /// <summary>
        /// Pass in new data to affect your components.
        /// </summary>
        /// <param name="newDataArray"></param>
        public void Update(TData[] newDataArray)
        {
            Update(newDataArray.ToList());
        }

        /// <summary>
        /// Pass in new data to affect your components.
        /// </summary>
        /// <param name="newData"></param>
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
            OrderedDictionary newOrderedDictionary;
            if (comparer == null)
                newOrderedDictionary = new OrderedDictionary();
            else
                newOrderedDictionary = new OrderedDictionary((IEqualityComparer)comparer);

            //  Remove
            for (int i = cacheKeys.Count - 1; i >= 0; i--)
            {
                TData ck = cacheKeys[i];
                if(comparer == null)
                {
                    if (!newData.Contains(ck))
                    {
                        remove.Invoke(cacheKeys[i], cacheValues[i]);    //  Remove
                        cacheKeys.RemoveAt(i);
                        cacheValues.RemoveAt(i);
                    }
                }
                else
                {
                    if (!newData.Contains(ck, comparer))
                    {
                        remove.Invoke(cacheKeys[i], cacheValues[i]);    //  Remove
                        cacheKeys.RemoveAt(i);
                        cacheValues.RemoveAt(i);
                    }
                }
            }

            //  Add
            for (int i = 0; i < newData.Count; i++)
            {
                TData nd = newData[i];
                if(comparer == null)
                {
                    if (!cacheKeys.Contains(nd))
                    {
                        cacheKeys.Add(nd);
                        cacheValues.Add(add.Invoke(nd));    //  Add
                    }
                }
                else
                {
                    if (!cacheKeys.Contains(nd, comparer))
                    {
                        cacheKeys.Add(nd);
                        cacheValues.Add(add.Invoke(nd));    //  Add
                    }
                }
            }

            //  Update
            for (int i = 0; i < newData.Count; i++)
            {
                TData a = newData[i];
                int index = cacheKeys.IndexOf(a);
                TComponent b = cacheValues[index];
                update.Invoke(a, b, i);
                newOrderedDictionary.Add(a, b);
            }

            cache = newOrderedDictionary;

            //  Complete
            complete.Invoke();
        }

        /// <summary>
        /// Clear the dictionary and call Remove on all components.
        /// </summary>
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
        /// <summary>
        /// Get all data.
        /// </summary>
        /// <returns></returns>
        public TData[] GetKeys()
        {
            if (cache == null) return new TData[0];

            TData[] keys = new TData[cache.Count];
            cache.Keys.CopyTo(keys, 0);
            return keys;
        }

        /// <summary>
        /// Get all components.
        /// </summary>
        /// <returns></returns>
        public TComponent[] GetValues()
        {
            if (cache == null) return new TComponent[0];
            TComponent[] values = new TComponent[cache.Count];
            cache.Values.CopyTo(values, 0);
            return values;
        }

        /// <summary>
        /// Get component at index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TComponent GetValue(int index)
        {
            if (cache == null || index >= cache.Count) return null;

            return (TComponent)cache[index];
        }

        /// <summary>
        /// Get component paired to data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TComponent GetValue(TData key)
        {
            if (cache == null || !cache.Contains(key)) return null;

            return (TComponent)cache[key];
        }

        /// <summary>
        /// Count of the dictionary.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            if (cache == null) return -1;

            return cache.Keys.Count;
        }
        #endregion
    }
}
