using Bs.Shell.ScriptableObjects;
using System;
using UnityEngine.SceneManagement;

namespace Bs.Shell
{
    public class UIToken<TData> : UIToken
        where TData : UIData
    {
        public UIDataEvent<TData> uiDataEvent;

        public UIToken(Guid guid)
        {
            this.guid = guid;
        }

        public bool Equals(UIToken otherUIToken)
        {
            return (IsLoaded() && guid == otherUIToken.guid);
        }
    }

    public class UIToken
    {
        public Guid guid;
        public Scene scene;
        public bool disposeInProgress;

        public bool IsLoaded()
        {
            return !disposeInProgress;
        }
    }
}
