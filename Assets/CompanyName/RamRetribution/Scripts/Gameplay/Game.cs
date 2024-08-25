using System;
using System.Collections.Generic;
using System.Threading;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
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
        private readonly LvlCombinator _lvlCombinator;
        private readonly Wallet _wallet;

        private Squad<Ram> _rams;
        private Squad<Enemy> _enemies;
        private Level _currentLevel;

        private CancellationTokenSource _tokenSource;

        public Game(Wallet wallet, UnitSpawner spawner, LevelBuilder levelBuilder, LvlCombinator lvlCombinator)
        {
            _wallet = wallet;
            _unitSpawner = spawner;
            _levelBuilder = levelBuilder;
            _lvlCombinator = lvlCombinator;
        }

        public event Action<int> LevelStarting;

        public async UniTask StartAsync(int levelNumber)
        {
            _tokenSource = new CancellationTokenSource();

            LevelStarting?.Invoke(levelNumber);
            _lvlCombinator.OnLevelStarting(levelNumber);

            _currentLevel = await _levelBuilder.EntryBuild(levelNumber);
            _currentLevel.GatesDestroyed += OnGatesDestroyedAsync;

            _unitSpawner.RamsCreated += OnRamsCreatedAsync;
            _unitSpawner.EnemiesCreated += OnEnemiesCreated;

            _unitSpawner.SpawnRams(levelNumber);
        }

        private async UniTaskVoid HandleBattleAsync()
        {
            NotifyRamsAttackGate();

            await UniTask.WaitUntil(() => _currentLevel.IsGateAttackedFirst);

            while (_rams.IsAlive)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(DelayForSpawn), DelayType.Realtime)
                    .WithCancellation(_tokenSource.Token);

                await SpawnEnemiesAsync()
                    .WithCancellation(_tokenSource.Token);

                await UniTask.WaitUntil(() => !_enemies.IsAlive)
                    .WithCancellation(_tokenSource.Token);

                NotifyRamsAttackGate();
            }
        }

        private async UniTask MoveRamsToStartPositionAsync(IReadOnlyList<Vector3> destinations)
        {
            //var tasks = new UniTask<bool>[_rams.Units.Count];

            await _rams.MoveTo(destinations[Random.Range(0, destinations.Count)]);
            
            // for (var i = 0; i < _rams.Units.Count; i++)
            // {
            //     tasks[i] = _rams.Units[i].MoveToPoint(destinations[Random.Range(0, destinations.Count)],
            //         callback: _rams.Units[i].ActivateAgent);
            // }
            //
            // await UniTask.WhenAll(tasks);
        }

        private async UniTask SpawnEnemiesAsync()
        {
            _unitSpawner.SpawnEnemies(_currentLevel.Number,
                _currentLevel.GetEnemies(),
                _currentLevel.EnemySpawnPoint,
                _tokenSource).Forget();

            await UniTask.WaitUntil(() => _enemies.IsAlive);
        }

        private void NotifyRamsAttackGate()
        {
            for (var i = 0; i < _rams.Units.Count; i++)
                _rams.Units[i].Attack(_currentLevel.GetGateToAttack(), _currentLevel.GateAttackPoints[i]).Forget();
        }

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

            _wallet.Add(CurrencyTypes.Money, _lvlCombinator.GetGoldForUnit());
        }

        private async void OnGatesDestroyedAsync()
        {
            _currentLevel.GatesDestroyed -= OnGatesDestroyedAsync;

            CancelToken();
            PutAwayEnemies();
            _wallet.Add(CurrencyTypes.Money, _lvlCombinator.GetGoldForGate());

            var nextLevelNumber = _currentLevel.Number + 1;
            _currentLevel = await _levelBuilder.BuildNext(nextLevelNumber);

            _rams.OnLevelPassed(nextLevelNumber);
            LevelStarting?.Invoke(nextLevelNumber);
            _lvlCombinator.OnLevelStarting(nextLevelNumber);

            await MoveRamsToStartPositionAsync(_currentLevel.EntryTilesPositions);

            HandleBattleAsync().Forget();
        }

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
    }
}