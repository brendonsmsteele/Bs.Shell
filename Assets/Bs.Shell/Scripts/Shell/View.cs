using System;
using UnityEngine;
using UnityEngine.UI;

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

    /// <summary>
    /// A generic view which represents data in the form of TViewModel.
    /// You can create an individual view and call Bind(TViewModel viewModel), which will kickoff the Refresh() method,
    /// Or you can create a DataBoundViews<TViewModel>, which will cleanly maintain collections of views
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class View<TModel> : RefreshableObject
        where TModel : class
    {
        RectTransform root { get { return (RectTransform)this.transform; } }
        CanvasScaler _canvasScaler;
        CanvasScaler canvasScaler
        {
            get
            {
                if (_canvasScaler == null)
                    _canvasScaler = GetComponentInParent<CanvasScaler>();
                return _canvasScaler;
            }
        }

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
        /// <param name="viewModel">Model representing the data the view should reflect</param>
        public void Bind(TModel viewModel)
        { 
            if (_model != viewModel)
            {
                _model = viewModel;
            }

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

        /// <summary>
        /// To use this correctly your UI element needs to be anchored to the bottom-left corner of the screen.
        /// bottom-left is 0,0  top-right is 1,1
        /// </summary>
        /// <param name="normalizedScreenPos"></param>
        protected void MoveAnchoredPositionToViewPortPosition(Vector2 normalizedScreenPos)
        {
            //  TODO:  Consider using Screen.Width Screen.Height, if CanvasScalar is not always present.
            root.anchoredPosition = new Vector2(canvasScaler.referenceResolution.x * normalizedScreenPos.x,
                                                canvasScaler.referenceResolution.y * normalizedScreenPos.y);
        }

    }

}
