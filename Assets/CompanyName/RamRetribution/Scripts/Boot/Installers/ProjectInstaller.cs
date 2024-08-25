using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using Zenject;

namespace CompanyName.RamRetribution.Scripts.Boot.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStateMachine();
            BindDataService();
            BindResourcesLoaderService();
            BindPauseControl();
            BindWallet();
            BindConfigs();
            BindData();
            BindGameDataBase();
        }

        private void BindDataService()
        {
            ISerializer serializer = new JsonSerializer();

            Container
                .Bind<IDataService>()
                .To<PrefsDataService>()
                .FromNew()
                .AsSingle()
                .WithArguments(serializer)
                .NonLazy();
        }

        private void BindData()
        {
            Container
                .Bind<GameData>()
                .FromMethod(() => Container.Resolve<IDataService>().Load<GameData>(DataNames.GameData.ToString()))
                .AsCached()
                .NonLazy();

            Container
                .Bind<UnitConfig>()
                .WithId(nameof(Leader))
                .FromMethod(() => Container.Resolve<ConfigsContainer>().Get(ConfigId.Leader))
                .AsSingle()
                .Lazy();
            
            Container
                .Bind<LeaderDataState>()
                .FromNew()
                .AsCached()
                .Lazy();
            
            Container
                .Bind<ShopDataState>()
                .FromMethod(() =>
                    Container.Resolve<IDataService>().Load<ShopDataState>(DataNames.ShopDataState.ToString()))
                .AsCached()
                .Lazy();
        }

        private void BindResourcesLoaderService()
        {
            Container
                .Bind<IResourceLoadService>()
                .To<ResourceLoaderService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindPauseControl()
        {
            Container
                .Bind<PauseControl>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindStateMachine()
        {
            Container
                .Bind<StateMachine>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameDataBase()
        {
            Container
                .Bind<GameDataBase>()
                .FromResource($"{AssetPaths.GameDataBase}{nameof(GameDataBase)}")
                .AsSingle()
                .Lazy();
        }

        private void BindConfigs()
        {
            Container
                .Bind<ConfigsContainer>()
                .FromResource($"{AssetPaths.Configs}{nameof(ConfigsContainer)}")
                .AsSingle()
                .Lazy();
        }

        private void BindWallet()
        {
            Container
                .Bind<Wallet>()
                .FromNew()
                .AsSingle()
                .Lazy();
        }
    }
}