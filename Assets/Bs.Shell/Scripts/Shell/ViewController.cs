using UnityEngine;

namespace Bs.Shell
{
    /// <summary>
    /// This view contains a model, and is responsible for binding a model to it's given GameObjects/Components
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class ViewController<TModel> : RefreshableObject, IController<TModel>, ICleanable
        where TModel : Model
    {
        [SerializeField]
        protected TModel _model;

        /// <summary>
        /// When you set the model Refresh is called immediately after.
        /// </summary>
        public TModel model
        {
            get => _model;
            set
            {
                _model = value;
                Refresh();
            }
        }

        /// <summary>
        /// Subscribe to event listeners
        /// </summary>
        protected abstract void OnEnable();

        /// <summary>
        /// Unsubscribe from event listeners
        /// </summary>
        protected abstract void OnDisable();

        /// <summary>
        /// Use this when pooling and reset state
        /// </summary>
        public virtual void Clean()
        {
        }
    }
}
