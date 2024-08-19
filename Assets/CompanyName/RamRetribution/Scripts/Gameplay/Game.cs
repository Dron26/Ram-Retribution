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
        private const float DelayForSpawn = 10f;
        private readonly ModulesContainer _modulesContainer;
        private LevelBuilder _levelBuilder;
        private UnitSpawner _unitSpawner;

        private CancellationTokenSource _tokenSource;

        private Squad _rams;
        private List<Unit> _enemies = new List<Unit>();
        private Level _currentLevel;

        public Game(ModulesContainer container)
            => _modulesContainer = container;

        public event Action<int> LevelStarting; 
        private bool RamsAlive => _rams.Units.Count > 0;
        private bool HasEnemies => _enemies.Count > 0;

        public async UniTask StartAsync(int levelNumber)
        {
            _tokenSource = new CancellationTokenSource();
            _levelBuilder = _modulesContainer.Get<LevelBuilder>();

            LevelStarting?.Invoke(levelNumber);
            
            _currentLevel = await _levelBuilder.EntryBuild(levelNumber);
            _currentLevel.GatesDestroyed += OnGatesDestroyedAsync;
            
            _unitSpawner = _modulesContainer.Get<UnitSpawner>();
            _unitSpawner.RamsCreated += OnRamsCreatedAsync;
            _unitSpawner.EnemiesCreated += OnEnemiesCreated;

            _unitSpawner.CreateRams();
        }

        private async UniTaskVoid HandleBattleAsync()
        {
            _unitSpawner.SetEnemiesSpawnPoints(_currentLevel.EnemySpots);
            NotifyRamsAttackGateAsync();

            await UniTask.WaitUntil(() => _currentLevel.IsGateAttackedFirst);

            while (RamsAlive)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(DelayForSpawn), DelayType.Realtime)
                    .WithCancellation(_tokenSource.Token);

                await SpawnEnemiesAsync()
                    .WithCancellation(_tokenSource.Token);

                await UniTask.WaitUntil(() => !HasEnemies)
                    .WithCancellation(_tokenSource.Token);

                NotifyRamsAttackGateAsync();
            }
        }

        private async UniTask MoveRamsToStartPositionAsync(IReadOnlyList<Vector3> destinations)
        {
            var ramsPlacementVisitor =
                new RamsPlacementVisitor(destinations, new RamsPlacementStrategy());

            foreach (var ram in _rams.Units)
            {
                ram.DeactivateAgent();
                ramsPlacementVisitor.Visit(ram);
            }

            await UniTask.WaitUntil(() =>
            {
                foreach (var ram in _rams.Units)
                    if (!ram.IsActive)
                        return false;

                return true;
            });
        }

        private async UniTask SpawnEnemiesAsync()
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

        private void NotifyRamsAttackGateAsync()
        {
            for (var i = 0; i < _rams.Units.Count; i++)
                _rams.Units[i].Attack(_currentLevel.GetGate(), _currentLevel.GateAttackPoints[i]).Forget();
        }

        private async void OnRamsCreatedAsync(Squad squad)
        {
            _unitSpawner.RamsCreated -= OnRamsCreatedAsync;

            foreach (var ram in squad.Units)
                ram.Fleeing += OnRamFleeing;

            _rams = squad;

            await MoveRamsToStartPositionAsync(_currentLevel.EntryTilesPositions);

            HandleBattleAsync().Forget();
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
            ram.Flee(_currentLevel.EntryTilesPositions[Random.Range(0,_currentLevel.EntryTilesPositions.Count)]);

            if (_rams.Units.Count == 0)
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

        private async void OnGatesDestroyedAsync()
        {
            _currentLevel.GatesDestroyed -= OnGatesDestroyedAsync;
            CancelToken();
            
            foreach (var unit in _enemies)
                unit.Flee(_currentLevel.EntryTilesPositions[Random.Range(0,_currentLevel.EntryTilesPositions.Count)]);
            
            var nextLevelNumber = _currentLevel.Number + 1;
            _currentLevel = await _levelBuilder.BuildNext(nextLevelNumber);
            
            _rams.OnLevelPassed(nextLevelNumber);
            await MoveRamsToStartPositionAsync(_currentLevel.EntryTilesPositions);
            
            HandleBattleAsync().Forget();
        }

        private void CancelToken()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = new CancellationTokenSource();
        }
    }
}