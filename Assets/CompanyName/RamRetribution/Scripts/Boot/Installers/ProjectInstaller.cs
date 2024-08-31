using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
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
            BindData();
            BindPauseControl();
            BindWallet();
            BindConfigs();
            BindGameDataBase();
        }

        private void BindDataService()
        {
            ISerializer serializer = new JsonSerializer();

            Container
                .Bind<ISaveLoadDataService>()
                .To<PrefsSaveLoadDataService>()
                .FromNew()
                .AsSingle()
                .WithArguments(serializer)
                .NonLazy();
        }

        private void BindData()
        {
            Container
                .Bind<GameData>()
                .FromMethod(_ => Container
                    .Resolve<ISaveLoadDataService>()
                    .Load<GameData>(DataNames.GameData))
                .AsCached()
                .NonLazy();

            Container
                .Bind<LeaderDataState>()
                .FromMethod(_ => Container
                    .Resolve<ISaveLoadDataService>()
                    .Load<LeaderDataState>(DataNames.LeaderDataState))
                .AsCached()
                .Lazy();

            Container
                .Bind<ShopDataState>()
                .FromMethod(_ => Container
                    .Resolve<ISaveLoadDataService>()
                    .Load<ShopDataState>(DataNames.ShopDataState))
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