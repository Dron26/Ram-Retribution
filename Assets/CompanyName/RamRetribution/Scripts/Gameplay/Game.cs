using System;
using System.Collections.Generic;
using System.Threading;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Visitors;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.UI;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class Game
    {
        private const double DelayForSpawn = 10f;
        private readonly ModulesContainer _modulesContainer;
        private LevelBuilder _levelBuilder;
        private UnitSpawner _unitSpawner;

        private CancellationTokenSource _tokenSource;

        private List<Unit> _rams;
        private List<Unit> _enemies = new List<Unit>();
        private Level _currentLevel;

        public Game(ModulesContainer container)
            => _modulesContainer = container;

        private bool RamsAlive => _rams.Count > 0;
        private bool HasEnemies => _enemies.Count > 0;

        public async UniTask Start(int levelNumber)
        {
            _tokenSource = new CancellationTokenSource();
            _levelBuilder = _modulesContainer.Get<LevelBuilder>();

            _currentLevel = await _levelBuilder.EntryBuild(levelNumber);
            _currentLevel.GateDestroyed += OnGateDestroyed;
            
            _unitSpawner = _modulesContainer.Get<UnitSpawner>();
            _unitSpawner.RamsCreated += OnRamsCreated;
            _unitSpawner.EnemiesCreated += OnEnemiesCreated;

            _unitSpawner.CreateRams();
        }

        private async UniTaskVoid HandleBattle()
        {
            _unitSpawner.SetEnemiesSpawnPoints(_currentLevel.EnemySpots);
            NotifyRamsAttackGate();

            await UniTask.WaitUntil(() => _currentLevel.IsGateAttackedFirst);

            while (RamsAlive)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(DelayForSpawn), DelayType.Realtime)
                    .WithCancellation(_tokenSource.Token);

                await SpawnEnemies()
                    .WithCancellation(_tokenSource.Token);

                await UniTask.WaitUntil(() => !HasEnemies)
                    .WithCancellation(_tokenSource.Token);

                NotifyRamsAttackGate();
            }
        }

        private async UniTask MoveRamsToStartPosition(IReadOnlyList<Vector3> destinations)
        {
            var ramsPlacementVisitor =
                new UnitsPlacementVisitor(destinations[0], new RamsPlacementStrategy());

            foreach (var ram in _rams)
            {
                ram.DeactivateAgent();
                ramsPlacementVisitor.Visit(ram);
            }

            await UniTask.WaitUntil(() =>
            {
                foreach (var ram in _rams)
                    if (!ram.IsActive)
                        return false;

                return true;
            });
        }

        private async UniTask SpawnEnemies()
        {
            _unitSpawner.SpawnEnemies(new List<ConfigId>()
                {
                    ConfigId.LightEnemy,
                    ConfigId.LightEnemy,
                    ConfigId.LightEnemy
                },
                _tokenSource).Forget();

            await UniTask.WaitUntil(() => HasEnemies);
        }

        private void NotifyRamsAttackGate()
        {
            foreach (var ram in _rams)
                ram.Attack(_currentLevel.CurrentGate).Forget();
        }

        private async void OnRamsCreated(IReadOnlyList<Unit> rams)
        {
            _unitSpawner.RamsCreated -= OnRamsCreated;

            foreach (var ram in rams)
                ram.Fleeing += OnRamFleeing;

            _rams = rams as List<Unit>;

            await MoveRamsToStartPosition(_currentLevel.EntryTilesPositions);

            HandleBattle().Forget();
        }

        private void OnEnemiesCreated(IReadOnlyList<Unit> enemies)
        {
            _enemies = enemies as List<Unit>;

            foreach (var enemy in _enemies)
                enemy.Fleeing += OnEnemyFleeing;
        }

        private void OnRamFleeing(Unit ram)
        {
            ram.Fleeing -= OnRamFleeing;
            _rams.Remove(ram);
            ram.Flee(_currentLevel.EntryTilesPositions[Random.Range(0,_currentLevel.EntryTilesPositions.Count)]);

            if (_rams.Count == 0)
            {
                CancelToken();
                _modulesContainer.Get<GameUI>().ShowLoseScreen();
            }
        }

        private void OnEnemyFleeing(Unit enemy)
        {
            enemy.Fleeing -= OnEnemyFleeing;
            _enemies.Remove(enemy);
            enemy.Flee(_currentLevel.EntryTilesPositions[Random.Range(0,_currentLevel.EntryTilesPositions.Count)]);
        }

        private async void OnGateDestroyed()
        {
            _currentLevel.GateDestroyed -= OnGateDestroyed;
            CancelToken();
            
            foreach (var unit in _enemies)
                unit.Flee(_currentLevel.EntryTilesPositions[Random.Range(0,_currentLevel.EntryTilesPositions.Count)]);
            
            var nextLevelNumber = _currentLevel.Number + 1;
            
            _currentLevel = await _levelBuilder.BuildNext(nextLevelNumber);
            await MoveRamsToStartPosition(_currentLevel.EntryTilesPositions);
            
            HandleBattle().Forget();
        }

        private void CancelToken()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = new CancellationTokenSource();
        }
    }
}