using System.Collections.Generic;
using UnityEngine;

namespace Bs.Shell.ScriptableObjects
{
    public class UIDataEvent<TData> : UIDataEvent
        where TData : UIData
    {
        List<UIBase<TData>> listeners = new List<UIBase<TData>>();

        public void Raise(TData data)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(data);
        }

        public void RegisterListener(UIBase<TData> listener)
        {
            if(!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(UIBase<TData> listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }

        public TData fakeData;
    }

    public class UIDataEvent : ScriptableObject { }
}