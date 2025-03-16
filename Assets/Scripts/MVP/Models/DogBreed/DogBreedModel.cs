using System;
using EventSystem;
using EventSystem.Signals;
using Zenject;

namespace MVP.Models
{
    public class DogBreedModel : IInitializable, IDisposable, IModel
    {
        public event Action ModelChanged;

        public int ID { get; private set; }
        public int Number { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        
        private readonly EventBus _eventBus;

        public DogBreedModel(EventBus eventBus, int id)
        {
            _eventBus = eventBus;
            ID = id;
            Number = id + 1;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<UpdateDogBreedDataSignal>(OnUpdateDogBreedAttributesSignal);
        }

        private void OnUpdateDogBreedAttributesSignal(UpdateDogBreedDataSignal signal)
        {
            var attribute = signal.BreedDats[ID].attributes;

            Name = attribute.name;
            Description = attribute.description;
            
            ModelChanged?.Invoke();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<UpdateDogBreedDataSignal>(OnUpdateDogBreedAttributesSignal);
        }
        
        public sealed class Factory : PlaceholderFactory<EventBus, int, DogBreedModel>
        {
        }
    }
}