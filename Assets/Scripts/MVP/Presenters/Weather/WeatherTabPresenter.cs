using EventSystem;
using EventSystem.Signals;
using Logic.Weather;
using MVP.Models;
using MVP.Views;

namespace MVP.Presenters
{
    public class WeatherTabPresenter : TabPresenter<
        WeatherService,
        WeatherListView,
        WeatherModel,
        WeatherPresenter,
        WeatherTab,
        WeatherView>
    {
        private readonly WeatherPresenter.Factory _factory;
        
        public WeatherTabPresenter(
            WeatherService service,
            WeatherListView listView,
            WeatherPresenter.Factory factory,
            WeatherTab tab,
            EventBus eventBus)
            : base(service, listView, tab, eventBus)
        {
            _factory = factory;
        }

        protected override void SubscribeSignals()
        {
            _eventBus.Subscribe<OpenWeatherTabSignal>(OnOpenWeatherTab);
            _eventBus.Subscribe<CloseWeatherTabSignal>(OnCloseWeatherTab);
            _eventBus.Subscribe<StartDataLoadingSignal>(OnStartDataLoading);
            _eventBus.Subscribe<FinishDataLoadingSignal>(OnFinishDataLoading);
        }

        protected override void UnsubscribeSignals()
        {
            _eventBus.Unsubscribe<OpenWeatherTabSignal>(OnOpenWeatherTab);
            _eventBus.Unsubscribe<CloseWeatherTabSignal>(OnCloseWeatherTab);
            _eventBus.Unsubscribe<StartDataLoadingSignal>(OnStartDataLoading);
            _eventBus.Unsubscribe<FinishDataLoadingSignal>(OnFinishDataLoading);
        }

        protected override WeatherPresenter CreatePresenter(WeatherModel model, WeatherView view)
        {
            return _factory.Create(model, view);
        }

        private void OnOpenWeatherTab(OpenWeatherTabSignal obj)
        {
            _tab.Show();
        }

        private void OnCloseWeatherTab(CloseWeatherTabSignal obj)
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
    }
}