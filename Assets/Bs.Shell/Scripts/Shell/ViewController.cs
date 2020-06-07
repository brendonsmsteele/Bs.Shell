using UnityEngine;

namespace Nc.Shell
{
    /// <summary>
    /// This view contains a model, and is responsible for binding a model to it's given GameObjects/Components
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class ViewController<TModel> : RefreshableObject, IController<TModel>, IInteractable, IShowable, ICleanable
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
        /// Sub/Unsub to event listeners.
        /// </summary>
        public abstract void SetInteractable(bool interactable);

        /// <summary>
        /// Show or Hide you decide.
        /// </summary>
        public abstract void SetShow(bool show);

        /// <summary>
        /// Call clean when pooling
        /// </summary>
        public virtual void Clean()
        {
        }
    }
}
