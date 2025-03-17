using System;
using System.Threading.Tasks;
using EventSystem.Signals;
using Extensions;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;
using EventBus = EventSystem.EventBus;

namespace MVP.Models
{
    public class WeatherModel : IModel
    {
        public event Action ModelChanged;

        public int ID { get; private set; }
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }
        public int Temperature { get; private set; }
        public string TemperatureUnit { get; private set; }

        private readonly EventBus _eventBus;

        public WeatherModel(EventBus eventBus, int id)
        {
            _eventBus = eventBus;
            ID = id;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<UpdateWeatherPeriodsSignal>(OnUpdateWeatherModel);
        }

        private async void OnUpdateWeatherModel(UpdateWeatherPeriodsSignal signal)
        {
            var period = signal.Periods[ID];
            
            Name = period.name;
            Temperature = period.temperature;
            TemperatureUnit = period.temperatureUnit;
            Sprite = await LoadSpriteFromUrl(period.icon);
            
            ModelChanged?.Invoke();
        }
        
        private static async Task<Sprite> LoadSpriteFromUrl(string iconUrl)
        {
            var request = UnityWebRequestTexture.GetTexture(iconUrl);
            await request.SendWebRequestAsync();
            
            var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            
            return sprite;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<UpdateWeatherPeriodsSignal>(OnUpdateWeatherModel);
        }
        
        public sealed class Factory : PlaceholderFactory<EventBus, int, WeatherModel>
        {
        }
    }
}