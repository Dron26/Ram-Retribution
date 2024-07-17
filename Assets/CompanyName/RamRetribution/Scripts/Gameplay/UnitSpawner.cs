using System;
using System.Collections.Generic;
using System.Threading;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _ramsSpawnPoint;
        [SerializeField] private Transform _ramsContainer;
        [SerializeField] private Transform _enemiesContainer;

        private const int MaxRamUnits = 6;
        private const int MaxEnemyUnits = 9;

        private LeaderDataState _leaderData;
        private List<Unit> _rams;
        private List<ConfigId> _selectedRamsId;
        private List<Transform> _enemySpots;
        private IUnitFactory _factory;
        
        public event Action<IReadOnlyList<Unit>> RamsCreated;
        public event Action<IReadOnlyList<Unit>> EnemiesCreated;

        public void Init(
            IUnitFactory factory,
            LeaderDataState leaderDataState,
            List<ConfigId> selectedRamsId)
        {
            _factory = factory;
            _leaderData = leaderDataState;
            _selectedRamsId = selectedRamsId;
        }

        public void SetEnemiesSpawnPoints(List<Transform> enemySpots)
        {
            _enemySpots = enemySpots;
        }

        public async UniTaskVoid SpawnEnemies(List<ConfigId> configsId, CancellationTokenSource tokenSource)
        {
            var currentSpawn = _enemySpots[Random.Range(0, _enemySpots.Count)];

            await SpawnWithDelay(configsId, currentSpawn, tokenSource);
        }

        public void CreateRams()
        {
            if (_rams != null)
                throw new InvalidOperationException(
                    $"Recreating the {nameof(_rams)} list is not allowed as it has already been initialized.");
            
            _rams = new List<Unit>();
            var leader = SpawnLeader();

            _rams.Add(leader);

            if (_selectedRamsId.Count <= 0)
            {
                RamsCreated?.Invoke(_rams);
                return;
            }

            foreach (var id in _selectedRamsId)
            {
                var ram = _factory.Create(id, _ramsSpawnPoint.position);
                ram.transform.SetParent(_ramsContainer);
                _rams.Add(ram);
            }

            RamsCreated?.Invoke(_rams);
        }

        private Unit SpawnLeader()
        {
            var leader = _factory.CreateLeader(_leaderData, _ramsSpawnPoint.position);
            leader.transform.SetParent(_ramsContainer);

            return leader;
        }

        private async UniTask SpawnWithDelay(
            List<ConfigId> configsId, 
            Transform currentSpawn, 
            CancellationTokenSource tokenSource, 
            float delay = 0.5f)
        {
            IPlacementStrategy placementStrategy = new CirclePlacementStrategy(2f, 3.5f);
            List<Unit> enemiesToAttack = new List<Unit>();

            foreach (var config in configsId)
            {
                var enemy = _factory.Create(config, currentSpawn.position);
                enemy.transform.SetParent(_enemiesContainer);

                var atPosition = Vector3.zero.With(
                    x: currentSpawn.position.x, 
                    z: currentSpawn.position.z - 2f);
                
                enemy.MoveToPoint(placementStrategy.SetPosition(atPosition, enemy), 
                    enemy.ActivateAgent);
                
                enemiesToAttack.Add(enemy);

                await UniTask.Delay(
                    TimeSpan.FromSeconds(delay),
                    DelayType.DeltaTime,
                    PlayerLoopTiming.Update,
                    tokenSource.Token);
            }

            await UniTask.WaitUntil(() =>
            {
                foreach (var unit in enemiesToAttack)
                {
                    if (!unit.IsActive)
                        return false;
                }

                return true;
            });
            
            EnemiesCreated?.Invoke(enemiesToAttack);
        }
    }
}