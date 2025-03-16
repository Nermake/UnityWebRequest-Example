using System;
using MVP.Models;
using MVP.Views;
using Zenject;

namespace MVP.Presenters
{
    public abstract class Presenter<TModel, TView> : IInitializable, IDisposable 
        where TModel : IModel
        where TView : BaseView
    {
        public TView View => _view;
        
        protected readonly TModel _model;
        protected readonly TView _view;

        protected Presenter(TView view, TModel model)
        {
            _view = view;
            _model = model;
        }
        
        public virtual void Initialize()
        {
            _model.ModelChanged += OnModelChanged;
        }

        protected abstract void OnModelChanged();

        public virtual void Dispose()
        {
            _model.ModelChanged -= OnModelChanged;
        }
    }
}