using Cinemachine;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Gameplay;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Common;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Enemies;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [SerializeField] private Transform _ramsSpawnPoint;
    [SerializeField] private Transform _ramsContainer;
    [SerializeField] private Transform _enemiesContainer;

    public override void InstallBindings()
    {
        BindLvlCombinator();
        BindGame();
        BindUnitSpawner();
        //BindBattleMediator();
        BindLevelBuilder();
        BindCamera();

        Container.BindInterfacesTo<BattleMediator>().AsSingle();
    }

    public override void Start()
    {
        var lvlNumber = Container.Resolve<GameData>().GetLastPassedLevelIndex();

        Container.Resolve<Game>().StartAsync(lvlNumber).Forget();
    }

    private void BindGame()
    {
        Container
            .Bind<Game>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }

    private void BindUnitSpawner()
    {
        IUnitFactory<Enemy> forestEnemiesFactory = Container.Instantiate<ForestEnemiesFactory>();

        Container
            .Bind<UnitSpawner>()
            .FromNew()
            .AsSingle()
            .WithArguments(
                new EnemyFactoriesContainer(forestEnemiesFactory), 
                _ramsSpawnPoint, 
                _ramsContainer,
                _enemiesContainer)
            .Lazy();
    }

    private void BindLevelBuilder()
    {
        ITileFactory<Tile> forestTileFactory = Container.Instantiate<ForestTileFactory>();
        ITileFactory<Tile> sandTileFactory = Container.Instantiate<SandTileFactory>();
        ITileFactory<Tile> iceTileFactory = Container.Instantiate<IceTileFactory>();

        Container
            .Bind<LevelBuilder>()
            .FromNew()
            .AsSingle()
            .WithArguments(new TileFactoriesContainer(forestTileFactory, sandTileFactory, iceTileFactory))
            .Lazy();
    }

    private void BindLvlCombinator()
    {
        Container
            .Bind<LvlCombinator>()
            .FromNew()
            .AsCached()
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
}