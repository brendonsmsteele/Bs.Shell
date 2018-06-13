using Bs.Shell.ScriptableObjects;
using System;
using UnityEngine;

namespace Bs.Shell
{
    public class UIBase<TData> : MonoBehaviour, IUI<TData>, IDisposableUI
    where TData : UIData
    {
        protected bool isDirty;

        Guid guid;
        public Guid Guid
        {
            get { return guid; }
        }

        public virtual void Bind(TData data)
        {
            throw new NotImplementedException();
        }

        public virtual void Refresh()
        {
            throw new NotImplementedException();
        }

        public virtual ManualYieldInstruction Dispose()
        {
            ManualYieldInstruction manualYield = new ManualYieldInstruction();
            manualYield.IsDone = true;
            return manualYield;
        }

        // Use this for initialization
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {
        }

        protected virtual void OnEnable()
        {
            if (Event != null)
                Event.RegisterListener(this);
        }

        protected virtual void OnDisable()
        {
            if (Event != null)
                Event.UnregisterListener(this);
        }

        [SerializeField] UIDataEvent _Event;
        public UIDataEvent<TData> Event
        {
            get
            {
                return (UIDataEvent<TData>)_Event;
            }
            set
            {
                if (_Event != value)
                {
                    //  Unsubscribe old
                    if (_Event != null)
                        ((UIDataEvent<TData>)_Event).UnregisterListener(this);

                    //  Subscribe new if not null
                    _Event = value;
                    if (_Event != null)
                        ((UIDataEvent<TData>)_Event).RegisterListener(this);
                }
            }
        }

        public void OnEventRaised(TData data)
        {
            this.Bind(data);
        }
    }
}