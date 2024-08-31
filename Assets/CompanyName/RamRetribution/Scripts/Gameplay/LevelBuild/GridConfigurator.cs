using System;
using System.IO;
using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factories.Interfaces;
using CompanyName.RamRetribution.Scripts.Factories.Tiles;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Tiles;
using Cysharp.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.ReworkLevelBuild_GridConfigurator
{
    public class GridConfigurator
    {
        private readonly IResourceLoadService _loadService;
        private readonly LevelCombinator _levelCombinator;

        private LocationTypes _locationType = LocationTypes.None;
        private GateTypes _gateType;
        private TileObjectsPool<Tile> _tileObjectsPool;
        private NavMeshSurface _meshSurface;

        public GridConfigurator(IResourceLoadService loadService, LevelCombinator levelCombinator)
        {
            _loadService = loadService;
            _levelCombinator = levelCombinator;
            CreateSurface(_loadService);
        }

        public async UniTask<Grid> CreateAsync(int levelNumber, Vector3 buildStartPosition)
        {
            SetTypes(levelNumber);
            
            var grid = new Grid(_levelCombinator);

            await BuildAsync(grid, buildStartPosition);

            return grid;
        }

        #region Common static operations

        private static TileType[,] ConvertTo2DArray(TileType[] tiles)
        {
            var tiles2D = new TileType[GridConstants.SizeX, GridConstants.SizeY];

            for (int x = 0; x < GridConstants.SizeX; x++)
            {
                for (int y = 0; y < GridConstants.SizeY; y++)
                {
                    tiles2D[x, y] = tiles[x * GridConstants.SizeY + y];
                }
            }

            return tiles2D;
        }

        private static int GetFolderItemsCount(string path)
        {
            var directoryInfo = new DirectoryInfo($"{AssetPaths.Resources}{path}");

            var objectsCount = directoryInfo.GetFiles(
                    "*.asset",
                    SearchOption.TopDirectoryOnly)
                .Length;

            return objectsCount;
        }

        #endregion

        #region BuildGrid logic

        private async UniTask BuildAsync(Grid grid, Vector3 gridStart)
        {
            var fallDelayTime = 0.5f;
            var gridData = LoadData();
            var tiles2D = ConvertTo2DArray(gridData.Tiles);
            
            for (var y = 0; y < GridConstants.SizeY; y++)
            {
                var isOdd = y % 2 == 0;
                var startX = isOdd ? 0 : GridConstants.SizeX - 1;
                var endX = isOdd ? GridConstants.SizeX : -1;
                var step = isOdd ? 1 : -1;

                for (var x = startX; x != endX; x += step)
                {
                    var targetPosition = gridStart + Vector3.zero.With(
                        x: x * GridConstants.StepBetweenTiles,
                        z: y * GridConstants.StepBetweenTiles);

                    var startPosition = gridStart + Vector3.zero.With(
                        x: targetPosition.x,
                        y: 3,
                        z: targetPosition.z);

                    var gridSide = x < GridConstants.SizeX / 2
                        ? GridConstants.LeftSide
                        : GridConstants.RightSide;

                    var tile = _tileObjectsPool.Get();
                    tile.SetType(tiles2D[x, y], gridSide);
                    
                    AnimateMovementAsync(tile, grid, startPosition, targetPosition).Forget();

                    await UniTask.Delay(TimeSpan.FromSeconds(fallDelayTime));
                }
            }

            _meshSurface.RemoveData();
            _meshSurface.BuildNavMesh();
        }

        private async UniTaskVoid AnimateMovementAsync(
            Tile tile,
            Grid grid,
            Vector3 startPosition,
            Vector3 targetPosition
        )
        {
            var duration = 1.2f;
            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                tile.transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                await UniTask.Yield();
            }

            tile.transform.position = targetPosition;
            grid.ParseTile(tile);
        }

        #endregion

        #region Operations with types & surface

        private void SetTypes(int levelNumber)
        {
            const int WoodChance = 60;
            var gateTypeChance = Random.Range(0, 100);

            _gateType = gateTypeChance <= WoodChance
                ? GateTypes.Wood
                : GateTypes.Rock;
            
            var newLocationType = levelNumber switch
            {
                >= 1 and <= GameConstants.ForestLocationEndLevel
                    => LocationTypes.Forest,

                > GameConstants.ForestLocationEndLevel and <= GameConstants.SandLocationEndLevel
                    => LocationTypes.Sand,

                > GameConstants.SandLocationEndLevel and <= GameConstants.MaxLevels
                    => LocationTypes.Ice,

                _ => throw new ArgumentException()
            };

            if (newLocationType == _locationType) 
                return;
            
            _locationType = newLocationType;
            SetTileFactory();
        }

        private void SetTileFactory()
        {
            ITileFactory<Tile> tileFactory = _locationType switch
            {
                LocationTypes.Forest => new ForestTileFactory(_loadService),
                LocationTypes.Sand => new SandTileFactory(_loadService),
                LocationTypes.Ice => new IceTileFactory(_loadService),
                LocationTypes.None => throw new ArgumentException(
                    $"You must set locationType before creating Tiles pool and factories"),
                _ => throw new ArgumentOutOfRangeException()
            };

            _tileObjectsPool = new TileObjectsPool<Tile>(tileFactory ,parent: _meshSurface.transform);
            _tileObjectsPool.Create(GridConstants.SizeX * GridConstants.SizeY);
        }

        private void CreateSurface(IResourceLoadService loadService)
        {
            var surfacePrefab = loadService
                .Load<NavMeshSurface>($"{AssetPaths.GridData}{nameof(NavMeshSurface)}");

            _meshSurface = Object
                .Instantiate(surfacePrefab)
                .GetComponent<NavMeshSurface>();
        }

        private GridData LoadData()
        {
            int randomIndex;
            string path;

            switch (_locationType)
            {
                case LocationTypes.Forest:
                    randomIndex = Random.Range(0, GetFolderItemsCount($"{AssetPaths.ForestGridData}{_gateType}"));
                    path = $"{AssetPaths.ForestGridData + _gateType + "/" + randomIndex}";
                    return _loadService.Load<GridData>(path);

                case LocationTypes.Sand:
                    randomIndex = Random.Range(0, GetFolderItemsCount($"{AssetPaths.SandGridData}{_gateType}"));
                    path = $"{AssetPaths.SandGridData + _gateType + "/" + randomIndex}";
                    return _loadService.Load<GridData>(path);

                case LocationTypes.Ice:
                    randomIndex = Random.Range(0, GetFolderItemsCount($"{AssetPaths.IceGridData}{_gateType}"));
                    path = $"{AssetPaths.IceGridData + _gateType + "/" + randomIndex}";
                    return _loadService.Load<GridData>(path);

                default:
                    throw new ArgumentOutOfRangeException(nameof(_locationType), _locationType, null);
            }
        }

        #endregion
    }
}