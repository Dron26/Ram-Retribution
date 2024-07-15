using System;
using System.Collections.Generic;
using System.Threading;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Visitors;
using CompanyName.RamRetribution.Scripts.UI;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using Cysharp.Threading.Tasks;
using UnityEngine;

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

        public Game(ModulesContainer container)
            => _modulesContainer = container;

        private bool RamsAlive => _rams.Count > 0;
        private bool HasEnemies => _enemies.Count > 0;

        public void Start()
        {
            _levelBuilder = _modulesContainer.Get<LevelBuilder>();
            _levelBuilder.Build();
            _levelBuilder.GateDestroyed += OnGateDestroyed;

            _unitSpawner = _modulesContainer.Get<UnitSpawner>();
            _unitSpawner.RamsCreated += OnRamsCreated;
            _unitSpawner.EnemiesCreated += OnEnemiesCreated;

            _tokenSource = new CancellationTokenSource();

            _unitSpawner.CreateRams();
        }

        private async UniTaskVoid HandleBattle()
        {
            _unitSpawner.SetEnemiesSpawnPoints(_levelBuilder.EnemySpots);
            NotifyRamsAttackGate();

            await UniTask.WaitUntil(() => _levelBuilder.IsCurrentGateAttackedFirst);

            while (RamsAlive)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(DelayForSpawn), DelayType.Realtime)
                    .WithCancellation(_tokenSource.Token);

                await SpawnEnemies();

                await UniTask.WaitUntil(() => !HasEnemies).WithCancellation(_tokenSource.Token);
                
                NotifyRamsAttackGate();
            }
        }

        private async UniTask MoveRamsToStartPosition(Vector3 destination)
        {
            var ramsPlacementVisitor =
                new UnitsPlacementVisitor(destination, new RamsPlacementStrategy());

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
                ram.Attack(_levelBuilder.CurrentGate).Forget();
        }
        
        private async void OnRamsCreated(IReadOnlyList<Unit> rams)
        {
            _unitSpawner.RamsCreated -= OnRamsCreated;

            foreach (var ram in rams)
                ram.Fleeing += OnRamFleeing;

            _rams = rams as List<Unit>;

            await MoveRamsToStartPosition(_levelBuilder.RamsStartPosition);

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
        }

        private async void OnGateDestroyed()
        {
            _levelBuilder.GateDestroyed -= OnGateDestroyed;
            CancelToken();
            
            await MoveRamsToStartPosition(_levelBuilder.RamsStartPosition);
        }
        
        private void CancelToken()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = new CancellationTokenSource();
        }
    }
}