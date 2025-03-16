using EventSystem;
using EventSystem.Signals;
using MVP.Models;
using MVP.Views;
using Zenject;

namespace MVP.Presenters
{
    public class DogBreedPresenter : Presenter<DogBreedModel, DogBreedView>
    {
        private readonly EventBus _eventBus;
        
        public DogBreedPresenter(DogBreedView view, DogBreedModel model, EventBus eventBus) : base(view, model)
        {
            _eventBus = eventBus;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            _view.Clicked += OnClicked;
        }

        private void OnClicked()
        {
            _eventBus.Invoke(new OpenPopupSignal(_model));
        }

        protected override void OnModelChanged()
        {
            _view.SetNumber(_model.Number.ToString());
            _view.SetName(_model.Name);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _view.Clicked -= OnClicked;
        }

        public sealed class Factory : PlaceholderFactory<DogBreedModel, DogBreedView, EventBus, DogBreedPresenter>
        {
        }
    }
}