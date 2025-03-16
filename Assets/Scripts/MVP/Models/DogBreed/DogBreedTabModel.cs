using System;
using EventSystem;
using EventSystem.Signals;
using Game.Const;
using Game.Type.JSON;
using Logic.Core;
using Logic.DogBreeds;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace MVP.Models
{
    public class DogBreedTabModel : IInitializable, IDisposable, ITabModel
    {
        private readonly EventBus _eventBus;
        private readonly DogBreedService _weatherService;
        private readonly RequestManager _requestManager;
        private readonly DogBreedModel.Factory _factory;

        private bool _isActive = false;

        public DogBreedTabModel(EventBus eventBus, DogBreedService weatherService, RequestManager requestManager, DogBreedModel.Factory factory)
        {
            _eventBus = eventBus;
            _weatherService = weatherService;
            _requestManager = requestManager;
            _factory = factory;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<OpenDogBreedTabSignal>(OnOpenDogBreedTabSignal);
            _eventBus.Subscribe<CloseDogBreedTabSignal>(OnCloseDogBreedTabSignal);
            
            _eventBus.Invoke(new StartDataLoadingSignal());
            _requestManager.AddRequest(UnityWebRequest.Get(StringConst.DOG_BREED_URL), Callback);
        }

        private void OnOpenDogBreedTabSignal(OpenDogBreedTabSignal obj)
        {
            _isActive = false;
            _requestManager.CancelLastRequest();
        }

        private void OnCloseDogBreedTabSignal(CloseDogBreedTabSignal obj)
        {
            _isActive = true;
        }

        private void Callback(string json)
        {
            var breed = JsonUtility.FromJson<BreedJSON>(json);
            var count = breed.data.Count;

            for (var i = 0; i < count; i++)
            {
                var model = _factory.Create(_eventBus, i);
                
                model.Initialize();

                _weatherService.AddModel(model);
            }
            
            _eventBus.Invoke(new FinishDataLoadingSignal());
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<OpenDogBreedTabSignal>(OnOpenDogBreedTabSignal);
            _eventBus.Unsubscribe<CloseDogBreedTabSignal>(OnCloseDogBreedTabSignal);
        }
    }
}