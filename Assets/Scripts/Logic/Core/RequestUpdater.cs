using System;
using EventSystem;
using EventSystem.Signals;
using Game.Type.JSON;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Logic.Core
{
    public class RequestUpdater<TSignal> : IInitializable, IDisposable where TSignal : ISignal
    {
        private readonly RequestManager _requestManager;
        protected readonly EventBus _eventBus;

        protected string _url;

        protected RequestUpdater(RequestManager requestManager, EventBus eventBus)
        {
            _requestManager = requestManager;
            _eventBus = eventBus;
        }

        public virtual void Initialize()
        {
            _eventBus.Subscribe<TSignal>(OnUpdateWeather);
        }

        private void OnUpdateWeather(TSignal obj)
        {
            _eventBus.Invoke(new StartDataLoadingSignal());
            _requestManager.AddRequest(UnityWebRequest.Get(_url), Callback);
        }
        
        protected virtual void Callback(string json)
        {
            var weather = JsonUtility.FromJson<WeatherJSON>(json);
            
            _eventBus.Invoke(new UpdateWeatherPeriodsSignal(weather.properties.periods.ToArray()));
            _eventBus.Invoke(new FinishDataLoadingSignal());
        }
        
        public void Dispose()
        {
            _eventBus.Unsubscribe<TSignal>(OnUpdateWeather);
        }
    }
}