using System;
using EventSystem;
using EventSystem.Signals;
using Zenject;

namespace Logic.Core
{
    public class TabManager : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;

        public TabManager(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _eventBus.Subscribe<OpenWeatherTabSignal>(OnOpenWeatherTab);
            _eventBus.Subscribe<OpenDogBreedTabSignal>(OnOpenDogBreedsTab);
            _eventBus.Subscribe<EscapeClickedSignal>(OnEscapeClicked);
            _eventBus.Subscribe<Alpha1ClickedSignal>(OnAlpha1Clicked);
            _eventBus.Subscribe<Alpha2ClickedSignal>(OnAlpha2Clicked);
        }

        private void OnOpenWeatherTab(OpenWeatherTabSignal obj)
        {
            _eventBus.Invoke(new UpdateWeatherSignal());
        }
        
        private void OnOpenDogBreedsTab(OpenDogBreedTabSignal obj)
        {
            _eventBus.Invoke(new UpdateDogBreedSignal());
        }
        
        private void OnEscapeClicked(EscapeClickedSignal obj)
        {
            _eventBus.Invoke(new CloseDogBreedTabSignal());
            _eventBus.Invoke(new CloseWeatherTabSignal());
        }

        private void OnAlpha1Clicked(Alpha1ClickedSignal obj)
        {
            _eventBus.Invoke(new OpenWeatherTabSignal());
            _eventBus.Invoke(new CloseDogBreedTabSignal());
        }

        private void OnAlpha2Clicked(Alpha2ClickedSignal obj)
        {
            _eventBus.Invoke(new OpenDogBreedTabSignal());
            _eventBus.Invoke(new CloseWeatherTabSignal());
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<OpenWeatherTabSignal>(OnOpenWeatherTab);
            _eventBus.Unsubscribe<OpenDogBreedTabSignal>(OnOpenDogBreedsTab);
            _eventBus.Unsubscribe<EscapeClickedSignal>(OnEscapeClicked);
            _eventBus.Unsubscribe<Alpha1ClickedSignal>(OnAlpha1Clicked);
            _eventBus.Unsubscribe<Alpha2ClickedSignal>(OnAlpha2Clicked);
        }
    }
}