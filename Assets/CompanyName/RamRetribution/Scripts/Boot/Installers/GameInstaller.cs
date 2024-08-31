using Cinemachine;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Factories.Units;
using CompanyName.RamRetribution.Scripts.Gameplay;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CompanyName.RamRetribution.Scripts.Boot.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        [SerializeField] private Transform _ramsSpawnPoint;
        [SerializeField] private Transform _ramsContainer;
        [SerializeField] private Transform _enemiesContainer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BattleMediator>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelCombinator>().AsSingle();

            BindGame();
            BindBuffsContainer();
            BindRamFactory();
            BindLeader();
            BindUnitSpawner();
            BindCamera();
        }

        public override void Start()
        {
            var lvlNumber = Container.Resolve<GameData>().GetLastPassedLevelIndex();
            var selectedRams = Container.Resolve<ShopDataState>().SelectedRams;

            Container.Resolve<Game>().StartAsync(selectedRams, lvlNumber).Forget();
        }

        private void BindGame()
        {
            Container
                .Bind<Game>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindRamFactory()
        {
            Container
                .Bind<RamFactory>()
                .FromNew()
                .AsSingle()
                .Lazy();
        }

        private void BindBuffsContainer()
        {
            Container
                .Bind<BuffsContainer>()
                .FromResource($"{AssetPaths.Configs}{nameof(BuffsContainer)}")
                .AsSingle()
                .Lazy();
        }
        
        private void BindLeader()
        {
            Container
                .Bind<Leader>()
                .FromMethod(CreateLeader)
                .AsSingle()
                .Lazy();
        }

        private void BindUnitSpawner()
        {
            Container
                .Bind<UnitSpawner>()
                .FromNew()
                .AsSingle()
                .WithArguments(_ramsSpawnPoint, _ramsContainer, _enemiesContainer)
                .Lazy();
        }

        private void BindCamera()
        {
            Container
                .Bind<CinemachineVirtualCamera>()
                .FromInstance(_virtualCamera)
                .AsCached()
                .NonLazy();
        }

        private Leader CreateLeader(InjectContext ctx)
        {
            return Container
                .Resolve<RamFactory>()
                .CreateLeader(Container.Resolve<LeaderDataState>(), _ramsSpawnPoint.position);
        }
    }
}