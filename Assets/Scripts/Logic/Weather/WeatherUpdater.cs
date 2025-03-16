using EventSystem;
using EventSystem.Signals;
using Game.Const;
using Game.Type.JSON;
using Logic.Core;
using UnityEngine;

namespace Logic.Weather
{
    public class WeatherUpdater : RequestUpdater<UpdateWeatherSignal>
    {
        public WeatherUpdater(RequestManager requestManager, EventBus eventBus) : base(requestManager, eventBus) { }

        public override void Initialize()
        {
            base.Initialize();

            _url = StringConst.WEATHER_URL;
        }

        protected override void Callback(string json)
        {
            var weather = JsonUtility.FromJson<WeatherJSON>(json);
            
            _eventBus.Invoke(new UpdateWeatherPeriodsSignal(weather.properties.periods.ToArray()));
            _eventBus.Invoke(new FinishDataLoadingSignal());
        }
    }
}