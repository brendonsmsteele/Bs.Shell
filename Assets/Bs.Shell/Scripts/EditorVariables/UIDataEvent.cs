using System.Collections.Generic;
using UnityEngine;

namespace Bs.Shell.EditorVariables
{
    public class ControllerDataEvent<TData> : ControllerDataEvent
        where TData : ControllerData
    {
        List<ControllerBase<TData>> listeners = new List<ControllerBase<TData>>();

        public void Raise(TData data)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(data);
        }
        public void RegisterListener(ControllerBase<TData> listener)
        {
            if(!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(ControllerBase<TData> listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }

        public TData fakeData;

        public void OnEnable()
        {
            if(fakeData != null)
                fakeData.RequestBind += FakeData_Bind;
        }

        public void OnDisable()
        {
            if (fakeData != null)
                fakeData.RequestBind -= FakeData_Bind;
        }

        private void FakeData_Bind(object sender, System.EventArgs e)
        {
            Raise(fakeData);
        }
    }

    public class ControllerDataEvent : ScriptableObject { }
}