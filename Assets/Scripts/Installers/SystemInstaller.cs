using EventSystem;
using Logic.Core;
using Zenject;

namespace Installer
{
    public sealed class SystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<TabManager>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<EventBus>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<RequestManager>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<InputManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}