using CompanyName.RamRetribution.Scripts.Factories;
using Zenject;

namespace CompanyName.RamRetribution.Scripts.Boot.Installers
{
    public class LobbyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCanvasFactory();
        }

        public override void Start()
        {
            Container.Resolve<CanvasFactory>().CreateLobbyView();
        }

        private void BindCanvasFactory()
        {
            Container
                .Bind<CanvasFactory>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}