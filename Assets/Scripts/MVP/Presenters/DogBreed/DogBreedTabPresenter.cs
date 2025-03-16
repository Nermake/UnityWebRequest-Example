using EventSystem;
using EventSystem.Signals;
using Logic.DogBreeds;
using MVP.Models;
using MVP.Views;

namespace MVP.Presenters
{
    public class DogBreedTabPresenter : TabPresenter<
        DogBreedService,
        DogBreedListView,
        DogBreedModel,
        DogBreedPresenter,
        DogBreedTab,
        DogBreedView>
    {
        private readonly DogBreedPresenter.Factory _factory;
        
        public DogBreedTabPresenter(
            DogBreedService service,
            DogBreedListView listView,
            DogBreedPresenter.Factory factory,
            DogBreedTab tab,
            EventBus eventBus)
            : base(service, listView, tab, eventBus)
        {
            _factory = factory;
        }

        protected override void SubscribeSignals()
        {
            _eventBus.Subscribe<OpenDogBreedTabSignal>(OnOpenDogBreedTab);
            _eventBus.Subscribe<CloseDogBreedTabSignal>(OnCloseDogBreedTab);
            _eventBus.Subscribe<StartDataLoadingSignal>(OnStartDataLoading);
            _eventBus.Subscribe<FinishDataLoadingSignal>(OnFinishDataLoading);
            _eventBus.Subscribe<UnlockRaycastSignal>(OnUnlockRaycast);
            _eventBus.Subscribe<LockRaycastSignal>(OnLockRaycast);
        }
        
        protected override void UnsubscribeSignals()
        {
            _eventBus.Unsubscribe<OpenDogBreedTabSignal>(OnOpenDogBreedTab);
            _eventBus.Unsubscribe<CloseDogBreedTabSignal>(OnCloseDogBreedTab);
            _eventBus.Unsubscribe<StartDataLoadingSignal>(OnStartDataLoading);
            _eventBus.Unsubscribe<FinishDataLoadingSignal>(OnFinishDataLoading);
            _eventBus.Unsubscribe<UnlockRaycastSignal>(OnUnlockRaycast);
            _eventBus.Unsubscribe<LockRaycastSignal>(OnLockRaycast);
        }
        
        protected override DogBreedPresenter CreatePresenter(DogBreedModel model, DogBreedView view)
        {
            return _factory.Create(model, view, _eventBus);
        }
        
        private void OnOpenDogBreedTab(OpenDogBreedTabSignal obj)
        {
            _tab.Show();
        }
        
        private void OnCloseDogBreedTab(CloseDogBreedTabSignal obj)
        {
            _tab.Hide();
        }
        
        private void OnStartDataLoading(StartDataLoadingSignal obj)
        {
            _tab.StartLoading();
        }
        
        private void OnFinishDataLoading(FinishDataLoadingSignal obj)
        {
            _tab.FinishLoading();
        }
        
        private void OnLockRaycast(LockRaycastSignal obj)
        {
            _tab.LockRaycast();
        }
        
        private void OnUnlockRaycast(UnlockRaycastSignal obj)
        {
            _tab.UnlockRaycast();
        }
    }
}