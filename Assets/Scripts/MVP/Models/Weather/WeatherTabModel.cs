using System;
using EventSystem;
using EventSystem.Signals;
using Game.Const;
using Game.Type.JSON;
using Logic.Core;
using Logic.Weather;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace MVP.Models
{
    public class WeatherTabModel : ITabModel, ITickable
    {
        private readonly EventBus _eventBus;
        private readonly WeatherService _weatherService;
        private readonly RequestManager _requestManager;
        private readonly WeatherModel.Factory _factory;

        private const float _duration = 5.0f;
        private float _remainingTime;
        private bool _isActive = false;
        
        public WeatherTabModel(EventBus eventBus, WeatherService weatherService, RequestManager requestManager, WeatherModel.Factory factory)
        {
            _eventBus = eventBus;
            _weatherService = weatherService;
            _requestManager = requestManager;
            _factory = factory;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<OpenWeatherTabSignal>(OnOpenWeatherTabSignal);
            _eventBus.Subscribe<CloseWeatherTabSignal>(OnCloseWeatherTabSignal);
            
            _eventBus.Invoke(new StartDataLoadingSignal());
            _requestManager.AddRequest(UnityWebRequest.Get(StringConst.WEATHER_URL), Callback);

            _remainingTime = _duration;
        }

        private void OnCloseWeatherTabSignal(CloseWeatherTabSignal obj)
        {
            _isActive = false;
            _requestManager.CancelLastRequest();
        }

        private void OnOpenWeatherTabSignal(OpenWeatherTabSignal obj)
        {
            _isActive = true;
            _remainingTime = _duration;
        }

        public void Tick()
        {
            if (!_isActive) return;
            
            _remainingTime -= Time.deltaTime;

            if (_remainingTime <= 0)
            {
                _eventBus.Invoke(new UpdateWeatherSignal());
                _remainingTime = _duration;
            }
        }

        private void Callback(string json)
        {
            var weather = JsonUtility.FromJson<WeatherJSON>(json);
            var count = weather.properties.periods.Count;

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
            _eventBus.Unsubscribe<OpenWeatherTabSignal>(OnOpenWeatherTabSignal);
            _eventBus.Unsubscribe<CloseWeatherTabSignal>(OnCloseWeatherTabSignal);
        }
    }
}