using EventSystem;
using Logic.DogBreeds;
using MVP.Models;
using Zenject;

namespace Installer
{
    public sealed class DogBreedInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<DogBreedService>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<DogBreedTabModel>()
                .AsSingle()
                .NonLazy();
            
            Container.BindFactory<EventBus, int, DogBreedModel, DogBreedModel.Factory>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<DogBreedUpdater>()
                .AsSingle()
                .NonLazy();
        }
    }
}