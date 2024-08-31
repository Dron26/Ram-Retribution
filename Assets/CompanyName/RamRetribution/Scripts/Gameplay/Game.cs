using System;
using System.Collections.Generic;
using System.Threading;
using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.ReworkLevelBuild_GridConfigurator;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Enemies;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class Game
    {
        private const float DelayForSpawn = 10f;

        private readonly LevelBuilder _levelBuilder;
        private readonly UnitSpawner _unitSpawner;

        private Squad<Ram> _rams;
        private Squad<Enemy> _enemies;
        private Level _currentLevel;

        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public Game(UnitSpawner spawner, IResourceLoadService loadService, LevelCombinator levelCombinator)
        {
            _unitSpawner = spawner;
            levelCombinator.SubscribeOnGameEvents(this);
            
            _levelBuilder = new LevelBuilder(new GridConfigurator(loadService, levelCombinator));
        }

        public event Action<int> LevelStarting;
        public event Action<Enemy> EnemyDefeated;
        public event Action<GateTypes> GatesDestroyed;

        public async UniTaskVoid StartAsync(IReadOnlyList<ConfigId> selectedRams, int levelNumber)
        {
            LevelStarting?.Invoke(levelNumber);
            
            _currentLevel = await _levelBuilder.Build(levelNumber);
            _currentLevel.GatesDestroyed += OnGatesDestroyedAsync;

            _unitSpawner.RamsCreated += OnRamsCreatedAsync;
            _unitSpawner.EnemiesCreated += OnEnemiesCreated;

            _unitSpawner.SpawnRams(selectedRams, levelNumber);
        }

        private async UniTaskVoid HandleBattleAsync()
        {
            NotifyRamsAttackGate().Forget();

            await UniTask.WaitUntil(() => _currentLevel.IsGateAttackedFirst);

            while (_rams.IsAlive)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(DelayForSpawn), DelayType.Realtime)
                    .WithCancellation(_tokenSource.Token);

                SpawnEnemiesAsync().Forget();

                await UniTask.WaitUntil(() => !_enemies.IsAlive)
                    .WithCancellation(_tokenSource.Token);

                NotifyRamsAttackGate().Forget();
            }
        }

        private async UniTask MoveRamsToStartPositionAsync(IReadOnlyList<Vector3> destinations) 
            => await _rams.MoveTo(destinations[Random.Range(0, destinations.Count)]);

        private async UniTaskVoid SpawnEnemiesAsync()
        {
           var isSpawned = await _unitSpawner.SpawnEnemies(_currentLevel.Number,
                _currentLevel.GetEnemies(),
                _currentLevel.EnemiesSpawnPoint,
                _tokenSource.Token);

           if (!isSpawned)
               throw new Exception($"Something wrong with enemies spawn");
        }

        private async UniTaskVoid NotifyRamsAttackGate()
        {
            var tasks = new List<UniTask>();

            foreach (var ram in _rams.Units)
                tasks.Add(ram.MoveNavMeshAsync(_currentLevel.GetGateToAttack().transform));

            await UniTask.WhenAll(tasks);
        }

        #region EventHandlers

        private async void OnRamsCreatedAsync(Squad<Ram> squad)
        {
            _unitSpawner.RamsCreated -= OnRamsCreatedAsync;

            foreach (var ram in squad.Units)
                ram.Fleeing += OnRamFleeing;

            _rams = squad;

            await MoveRamsToStartPositionAsync(_currentLevel.EntryTilesPositions);

            HandleBattleAsync().Forget();
        }

        private void OnEnemiesCreated(Squad<Enemy> squad)
        {
            _enemies = squad;

            foreach (var enemy in _enemies.Units)
                enemy.Fleeing += OnEnemyFleeing;
        }

        private void OnRamFleeing(Ram ram)
        {
            ram.Fleeing -= OnRamFleeing;
            ram.FleeAsync(_currentLevel.EntryTilesPositions[Random.Range(0, _currentLevel.EntryTilesPositions.Count)])
                .Forget();

            if (_rams.Units.Count == 0)
            {
                CancelToken();
                //_modulesContainer.Get<GameUI>().ShowLoseScreen();
            }
        }

        private void OnEnemyFleeing(Enemy enemy)
        {
            enemy.Fleeing -= OnEnemyFleeing;
            enemy.FleeAsync(
                _currentLevel.EntryTilesPositions[Random.Range(0, _currentLevel.EntryTilesPositions.Count)]).Forget();

            EnemyDefeated?.Invoke(enemy);
        }

        private async void OnGatesDestroyedAsync(GateTypes gateType)
        {
            _currentLevel.GatesDestroyed -= OnGatesDestroyedAsync;

            CancelToken();
            PutAwayEnemies();
            GatesDestroyed?.Invoke(gateType);

            var nextLevelNumber = _currentLevel.Number + 1;
            LevelStarting?.Invoke(nextLevelNumber);
            _currentLevel = await _levelBuilder.Build(nextLevelNumber);

            _rams.OnLevelPassed(nextLevelNumber);

            await MoveRamsToStartPositionAsync(_currentLevel.EntryTilesPositions);

            HandleBattleAsync().Forget();
        }

        #endregion

        #region CommonActions

        private void PutAwayEnemies()
        {
            if (_enemies.Units.Count == 0)
                return;

            _enemies.MoveTo(_currentLevel.EntryTilesPositions[
                Random.Range(0, _currentLevel.EntryTilesPositions.Count)]).Forget();
        }

        private void CancelToken()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = new CancellationTokenSource();
        }

        #endregion
    }
}