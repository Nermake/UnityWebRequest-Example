using EventSystem;
using MVP.Models;
using MVP.Presenters;
using MVP.Views;
using UnityEngine;
using Zenject;

namespace Installer
{
    public sealed class UIInstaller : MonoInstaller
    {
        [Header("Tabs")]
        [SerializeField] private WeatherTab _weatherTab;
        [SerializeField] private DogBreedTab _dogBreedTab;
        
        [Space, Header("ListViews")]
        [SerializeField] private WeatherListView _weatherListView;
        [SerializeField] private DogBreedListView _dogBreedListView;
        [SerializeField] private PopupView _popupView;

        public override void InstallBindings()
        {
            Container
                .Bind<WeatherTab>()
                .FromInstance(_weatherTab)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<DogBreedTab>()
                .FromInstance(_dogBreedTab)
                .AsSingle()
                .NonLazy();
            
            Container
                .BindFactory<WeatherModel, WeatherView, WeatherPresenter, WeatherPresenter.Factory>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindFactory<DogBreedModel, DogBreedView, EventBus, DogBreedPresenter, DogBreedPresenter.Factory>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<WeatherTabPresenter>()
                .AsSingle()
                .WithArguments(_weatherListView)
                .NonLazy();
            
            Container
                .BindInterfacesTo<DogBreedTabPresenter>()
                .AsSingle()
                .WithArguments(_dogBreedListView)
                .NonLazy();

            Container
                .BindInterfacesTo<PopupPresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<PopupView>()
                .FromInstance(_popupView)
                .AsSingle()
                .NonLazy();
        }
    }
}