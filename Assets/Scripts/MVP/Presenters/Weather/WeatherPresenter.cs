using MVP.Models;
using MVP.Views;
using Zenject;

namespace MVP.Presenters
{
    public class WeatherPresenter : Presenter<WeatherModel, WeatherView>
    {
        public WeatherPresenter(WeatherView view, WeatherModel model) : base(view, model) { }

        protected override void OnModelChanged()
        {
            _view.SetSprite(_model.Sprite);
            _view.SetWeekday(_model.Name);
            _view.SetTemperature($"{_model.Temperature} {_model.TemperatureUnit}");
        }
        
        public sealed class Factory : PlaceholderFactory<WeatherModel, WeatherView, WeatherPresenter>
        {
        }    
    }
}