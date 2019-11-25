using System.Collections.Generic;

namespace Bs.Shell
{
    public class ControllerDataEvent<TModel> : ControllerDataEvent
        where TModel : Model
    {
        List<ControllerBase<TModel>> listeners = new List<ControllerBase<TModel>>();

        public void Raise(TModel model)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].Bind(model);
        }

        public void RegisterListener(ControllerBase<TModel> listener)
        {
            if(!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(ControllerBase<TModel> listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }

    public class ControllerDataEvent { }
}