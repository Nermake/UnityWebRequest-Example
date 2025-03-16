using System;
using System.Collections.Generic;
using EventSystem;
using Logic.Core;
using MVP.Models;
using MVP.Views;
using Zenject;

namespace MVP.Presenters
{
    public abstract class TabPresenter<TService, TListView, TModel, TPresenter, TTab, TView> : IInitializable, IDisposable
        where TService : ModelService<TModel>
        where TListView : ListView<TView>
        where TModel : IModel
        where TPresenter : Presenter<TModel, TView>
        where TTab : TabView
        where TView : BaseView
    {
        private readonly TService _service; //Model:
        private readonly TListView _listView; //View:
        
        protected readonly TTab _tab;
        protected readonly EventBus _eventBus;

        private readonly Dictionary<TModel, Presenter<TModel, TView>> _presenters = new();

        protected TabPresenter(TService service, TListView listView,
            TTab tab, EventBus eventBus)
        {
            _service = service;
            _listView = listView;
            _tab = tab;
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            SubscribeSignals();

            _service.ModelAdded += OnModelAdded;
            _service.ModelRemoved += OnModelRemoved;

            var models = _service.Models;

            for (int i = 0, count = models.Count; i < count; i++)
            {
                var model = models[i];
                OnModelAdded(model);
            }
        }

        protected abstract void SubscribeSignals();
        protected abstract void UnsubscribeSignals();

        protected abstract TPresenter CreatePresenter(TModel model, TView view);

        private void OnModelAdded(TModel model)
        {
            if (_presenters.ContainsKey(model)) return;

            var view = _listView.SpawnElement();
            var presenter = CreatePresenter(model, view);

            presenter.Initialize();
            _presenters.Add(model, presenter);
        }

        private void OnModelRemoved(TModel model)
        {
            if (_presenters.Remove(model, out Presenter<TModel, TView> presenter))
            {
                presenter.Dispose();
                _listView.DespawnElement(presenter.View);
            }
        }

        public void Dispose()
        {
            UnsubscribeSignals();

            _service.ModelAdded -= OnModelAdded;
            _service.ModelRemoved -= OnModelRemoved;

            foreach (var presenter in _presenters.Values)
            {
                presenter.Dispose();
            }

            _presenters.Clear();
            _listView.Clear();
        }
    }
}