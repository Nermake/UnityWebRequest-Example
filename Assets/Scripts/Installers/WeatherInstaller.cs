using EventSystem;
using Logic.Weather;
using MVP.Models;
using Zenject;

namespace Installer
{
    public class WeatherInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<WeatherService>()
                .AsCached()
                .NonLazy();

            Container
                .BindInterfacesTo<WeatherTabModel>()
                .AsSingle()
                .NonLazy();
            
            Container.BindFactory<EventBus, int, WeatherModel, WeatherModel.Factory>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<WeatherUpdater>()
                .AsSingle()
                .NonLazy();
        }
    }
}