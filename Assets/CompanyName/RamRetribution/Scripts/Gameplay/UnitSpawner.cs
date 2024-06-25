using System.Collections;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common.Services;
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
        private List<string> _selectedRamsId;
        private List<Transform> _enemySpots;
        private IUnitFactory _factory;

        private WaitForSeconds _waitFor;
        
        public SquadsHolder Squads { get; private set; }

        public void Init(IUnitFactory factory, LeaderDataState leaderDataState,List<string> selectedRamsId,int enemySquadsCount = 2)
        {
            _factory = factory;
            _leaderData = leaderDataState;
            _selectedRamsId = selectedRamsId;
            Squads = new SquadsHolder(MaxRamUnits, MaxEnemyUnits, enemySquadsCount);
        }

        public void SetSpawnPoints(List<Transform> enemySpots)
        {
            _enemySpots = enemySpots;
        }

        public void Spawn(List<string> configsId)
        {
            var currentSpawn = _enemySpots[Random.Range(0, _enemySpots.Count)];

            StartCoroutine(SpawnWithDelay(configsId, currentSpawn));
        }
        
        public void CreateRamsSquad()
        {
            SpawnLeader();

            if (_selectedRamsId.Count <= 0) return;
            
            foreach (var t in _selectedRamsId)
            {
                var ram = _factory.Create(t, _ramsSpawnPoint.position);
                ram.transform.SetParent(_ramsContainer);
                Squads.Add(ram);
            }
        }
        
        private void SpawnLeader()
        {
            var leader = _factory.CreateLeader(_leaderData, _ramsSpawnPoint.position);
            leader.transform.SetParent(_ramsContainer);
            
            Squads.Add(leader);
        }

        private IEnumerator SpawnWithDelay(List<string> configsId, Transform currentSpawn, float delay = 0.5f)
        {
            _waitFor ??= new WaitForSeconds(delay);
            
            foreach (var config in configsId)
            {
                var unit = _factory.Create(config, currentSpawn.position);
                unit.transform.SetParent(_enemiesContainer);
                Squads.Add(unit);
                
                yield return _waitFor;
            }
        }
    }
}