using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using Generator.Scripts.Common.Enums;
using Unity.AI.Navigation;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class LevelBuilder
    {
        private const int LevelNumberForSwitchTiles = 50;
        private const int GridsCount = 2;

        private readonly GridConfigurator _gridConfigurator = new GridConfigurator(GridsCount);
        private readonly ObjectsPool<Tile> _objectsPool;

        private Level _currentLevel;
        private Queue<Grid> _grids = new Queue<Grid>();
        private TileTypes _currentTilesType;
        private IFactory<Tile> _tileFactory;
        private NavMeshSurface _meshSurface;

        public LevelBuilder(IFactory<Tile> factory)
        {
            CreateSurface();
            _objectsPool = new ObjectsPool<Tile>(factory, parent: _meshSurface.transform);
            _objectsPool.Create(GridConstants.SizeX * GridConstants.SizeY);
        }

        public void SetFactory(TileFactory factory)
        {
            _tileFactory = factory;
        }

        public async UniTask<Level> EntryBuild(int levelNumber)
        {
            _currentLevel = new Level(levelNumber);

            _grids = _gridConfigurator.Get(SetGridType(levelNumber, out var gateTypes), gateTypes);

            await AnimateTilesFall(_grids.Dequeue(), Vector3.zero);
            
            return _currentLevel;
        }

        public async UniTask<Level> BuildNext(int levelNumber)
        {
            _currentLevel = new Level(levelNumber);

            if (_grids.Count < 1)
                _gridConfigurator.Get(SetGridType(levelNumber, out var gateTypes), gateTypes);

            await AnimateTilesFall(
                _grids.Dequeue(),
                Vector3.zero
                    .With(x: 0, z: GridConstants.SizeY * GridConstants.StepBetweenTiles));
            
            return _currentLevel;
        }
        
        private async UniTask AnimateTilesFall(Grid grid, Vector3 gridStart)
        {
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
                        y: Random.Range(3, 7),
                        z: targetPosition.z);

                    var gridSide = x < GridConstants.SizeX / 2
                        ? GridConstants.LeftSide
                        : GridConstants.RightSide;

                    await MoveTileToPosition(grid.Tiles[x, y], startPosition, targetPosition, gridSide);
                }
            }

            _meshSurface.RemoveData();
            _meshSurface.BuildNavMesh();
        }

        private async UniTask MoveTileToPosition(
            TileType tileType,
            Vector3 startPosition,
            Vector3 targetPosition,
            int gridSide)
        {
            var tile = _objectsPool.Get();
            tile.SetType(tileType, gridSide);
            tile.transform.position = startPosition;

            var duration = Random.Range(1.5f, 2f);
            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                tile.transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                await UniTask.Yield();
            }

            tile.transform.position = targetPosition;
            _currentLevel.Init(tile);
        }

        private GridTypes SetGridType(int levelNumber, out GateTypes gateTypes)
        {
            const int GroupCount = 3;
            const int WoodChance = 60;

            var levelIndexFromZero = levelNumber - 1;
            var groupNumber = levelIndexFromZero / LevelNumberForSwitchTiles;
            var gateTypeChance = Random.Range(0, 100);

            GridTypes gridTypes;

            switch (groupNumber % GroupCount)
            {
                case 0:
                    gridTypes = GridTypes.Forest;
                    break;
                case 1:
                    gridTypes = GridTypes.Sand;
                    break;
                case 2:
                    gridTypes = GridTypes.Snow;
                    break;
                default: throw new ArgumentException();
            }

            gateTypes = gateTypeChance <= WoodChance
                ? GateTypes.Wood
                : GateTypes.Rock;

            return gridTypes;
        }

        private void CreateSurface()
        {
            var surfacePrefab = Services
                .ResourceLoadService
                .Load<NavMeshSurface>($"{AssetPaths.GridData}{nameof(NavMeshSurface)}");

            _meshSurface = Object
                .Instantiate(surfacePrefab)
                .GetComponent<NavMeshSurface>();
        }
    }
}