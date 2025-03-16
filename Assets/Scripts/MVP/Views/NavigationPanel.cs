using EventSystem;
using EventSystem.Signals;
using UnityEngine;
using Zenject;

namespace MVP.Views
{
    public class NavigationPanel : MonoBehaviour
    {
        [Inject] private EventBus _eventBus;

        public void OnWeatherClicked()
        {
            _eventBus.Invoke(new OpenWeatherTabSignal());
            _eventBus.Invoke(new CloseDogBreedTabSignal());
        }
        
        public void OnDogBreedsClicked()
        {
            _eventBus.Invoke(new OpenDogBreedTabSignal());
            _eventBus.Invoke(new CloseWeatherTabSignal());
        }
    }
}