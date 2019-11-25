using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Bs.Shell
{
    public interface IChangeable
    {
        event Action OnChanged;
    }

    public interface IDirtyable
    {
        bool IsDirty { get; }
        void MakeDirty();
    }

    public interface IBindable<TModel>
        where TModel : Model
    {
        void Bind(TModel model);
    }

    /// <summary>
    /// A generic view which represents data in the form of TViewModel.
    /// You can create an individual view and call Bind(TViewModel viewModel), which will kickoff the Refresh() method,
    /// Or you can create a DataBoundViews<TViewModel>, which will cleanly maintain collections of views
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class View<TModel> : RefreshableObject, IBindable<TModel>
        where TModel : Model
    {
        [SerializeField]
        private TModel _model;

        /// <summary>
        /// Individual views get their model
        /// </summary>
        protected TModel model
        {
            get { return _model; }
        }

        /// <summary>
        /// Binds the view to a new model, and kicks off the Refresh() method if it is new
        /// </summary>
        /// <param name="model">Model representing the data the view should reflect</param>
        public void Bind(TModel model)
        { 
            _model = model;
            Refresh();
        }


        /// <summary>
        /// IMPORTANT you must call base.OnEnable();
        /// </summary>
        protected virtual void OnEnable()
        {
            AddEventListeners();
        }

        /// <summary>
        /// IMPORTANT you must call base.OnDiable();
        /// </summary>
        protected virtual void OnDisable()
        {
            RemoveEventListeners();
        }

        /// <summary>
        /// Add button listeners.
        /// </summary>
        protected abstract void AddEventListeners();

        /// <summary>
        /// Remove button listeners.
        /// </summary>
        protected abstract void RemoveEventListeners();

        public virtual void Dispose()
        {
            //Debug.LogWarning("Dispose NotImplemented on View");
        }

        private void InvokeNewAsyncBind()
        {
            CancelAsyncRefresh();
            InvokeAsyncRefresh();
        }

        #region AsyncRefresh

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        protected virtual async Task AsyncRefresh(CancellationToken cancellationToken) { }

        private void CancelAsyncRefresh()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
        }

        private void InvokeAsyncRefresh()
        {
            this.AsyncRefresh(cancellationTokenSource.Token);
        }

        #endregion
    }
}
