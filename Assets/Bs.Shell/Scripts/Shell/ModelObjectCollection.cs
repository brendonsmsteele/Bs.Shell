using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ModelCollection maintains a collection of views based on an input list of view models.
/// Calling Bind(List<Model> viewModels) will update the view representation 
/// If the bound collection contains one or more new view models, they will be created.
/// If the bound collection does not contain one or more view models currently represented by ModelCollection, they will be removed.
/// Any items that exist both in the bound collection and in the ModelCollection representation will be Refreshed with new view model data
/// </summary>
namespace Nc.Shell
{
    public class ModelObjectCollection<TModel> : MonoBehaviour
    where TModel : Model
    {
        public delegate void ViewEvent(TModel viewModel, ViewController<TModel> view, int siblingIndex);
        public ViewEvent OnViewAdded;
        public ViewEvent OnViewUpdated;
        public ViewEvent OnViewRemoved;

        bool OneOrMoreAdded = false;
        bool OneOrMoreRemoved = false;

        /// <summary>
        /// Encapsulates add, remove, and update and the functions that get called.
        /// </summary>
        DiffableDictionary<TModel, ViewController<TModel>> _viewDictionary;
        protected DiffableDictionary<TModel, ViewController<TModel>> ViewDictionary
        {
            get
            {
                if (_viewDictionary == null)
                {
                    _viewDictionary = new DiffableDictionary<TModel, ViewController<TModel>>(
                        AddView,
                        UpdateView,
                        RemoveView,
                        DictionaryUpdateComplete
                    );
                }
                return _viewDictionary;
            }

            private set { _viewDictionary = value; }
        }

        protected virtual ViewController<TModel> AddView(TModel model)
        {
            GameObject instantiatedObject = Instantiate(prefab) as GameObject;
            instantiatedObject.transform.SetParent(this.transform);
            instantiatedObject.transform.localPosition = Vector3.zero;
            instantiatedObject.transform.localEulerAngles = Vector3.zero;
            instantiatedObject.transform.localScale = Vector3.one;
            ViewController<TModel> view = instantiatedObject.GetComponent<ViewController<TModel>>();
            OnViewAdded?.Invoke(model, view, view.transform.GetSiblingIndex());
            OneOrMoreAdded = true;
            return view;
        }

        protected virtual void UpdateView(TModel model, ViewController<TModel> viewController, int siblingIndex)
        {
            viewController.model = model;
            viewController.transform.SetSiblingIndex(siblingIndex);
            OnViewUpdated?.Invoke(model, viewController, siblingIndex);
        }

        protected virtual void RemoveView(TModel viewModel, ViewController<TModel> viewController)
        {
            // do before destroying to give the listener access to the view
            OnViewRemoved?.Invoke(viewModel, viewController, viewController.transform.GetSiblingIndex());
            Destroy(viewController.gameObject);
            OneOrMoreRemoved = true;
        }

        protected virtual void DictionaryUpdateComplete()
        {
            isDirty = true;
        }

        /// <summary>
        /// The prefab that will be instantiated.
        /// Make sure your component is on the root of this prefab.
        /// </summary>
        public GameObject prefab;
        bool isDirty;

        private void OnDisable()
        {
            //Dispose();
            ViewDictionary.Clear();//ViewDictionary = null;
        }

        public virtual void Refresh()
        {

        }
        
        public virtual void RequestNewLayout()
        {

        }

        /// <summary>
        /// The ideal time to call refresh automatically.
        /// </summary>
        void LateUpdate()
        {
            if (isDirty)
            {
                Refresh();
                isDirty = false;
            }

            if(OneOrMoreAdded || OneOrMoreRemoved)
            {
                RequestNewLayout();
                OneOrMoreAdded = false;
                OneOrMoreRemoved = false;
            }
        }

        #region Calls
        /// <summary>
        /// Update your components with changed data.
        /// </summary>
        /// <param name="viewModels"></param>
        public void Bind(List<TModel> viewModels)
        {
            ViewDictionary.Update(viewModels);
        }

        /// <summary>
        /// Calls remove on all of your components, then clears the dictionary.
        /// </summary>
        public void Clear()
        {
            ViewDictionary.Clear();
        }

        /// <summary>
        /// Get all data.
        /// </summary>
        /// <returns></returns>
        public TModel[] GetKeys()
        {
            return ViewDictionary.GetKeys();
        }

        /// <summary>
        /// Get all components.
        /// </summary>
        /// <returns></returns>
        public ViewController<TModel>[] GetValues()
        {
            return ViewDictionary.GetValues();
        }

        /// <summary>
        /// Get component at index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ViewController<TModel> GetValue(int index)
        {
            return ViewDictionary.GetValue(index);
        }

        /// <summary>
        /// Get component linked to data.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ViewController<TModel> GetValue(TModel key)
        {
            return ViewDictionary.GetValue(key);
        }

        /// <summary>
        /// Number of count of dictionary.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return ViewDictionary.Count();
        }

        public int GetIndexOfView(ViewController<TModel> view)
        {
            int i = 0;
            foreach (var v in ViewDictionary.GetValues())
            {
                if (view.Equals(v))
                    return i;
                i++;
            }
            return -1;
        }
        #endregion
    }

}
