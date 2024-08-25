using System;
using System.Collections.Generic;
using System.Threading;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using CompanyName.RamRetribution.Scripts.Units.Enemies;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class UnitSpawner
    {
        private readonly LeaderDataState _leaderData;
        private readonly EnemyFactoriesContainer _factories;
        private readonly List<ConfigId> _selectedRamsId;
        
        private readonly Transform _ramsSpawnPoint;
        private readonly Transform _ramsContainer;
        private readonly Transform _enemiesContainer;

        public UnitSpawner(
            LeaderDataState leaderDataState,
            ShopDataState shopData,
            EnemyFactoriesContainer factories,
            Transform ramsSpawnPoint,
            Transform ramsContainer,
            Transform enemiesContainer)
        {
            _leaderData = leaderDataState;
            _factories = factories;
            _selectedRamsId = shopData.SelectedRams;

            _ramsSpawnPoint = ramsSpawnPoint;
            _ramsContainer = ramsContainer;
            _enemiesContainer = enemiesContainer;
        }

        public event Action<Squad<Ram>> RamsCreated;
        public event Action<Squad<Enemy>> EnemiesCreated;

        public async UniTaskVoid SpawnEnemies(int levelNumber, IReadOnlyList<ConfigId> configsId, Vector3 at,
            CancellationTokenSource tokenSource)
        {
            var squad = new Squad<Enemy>(configsId.Count, new CirclePlacementStrategy(1,2));
            var factory = _factories.Get(levelNumber);

            foreach (var config in configsId)
            {
                var enemy = factory.Create(config, at);
                enemy.transform.SetParent(_enemiesContainer);

                squad.Add(enemy);
            }

            var atPosition = Vector3.zero.With(
                x: at.x,
                z: at.z - 2f);

            //var task = await GoToPositionAsync(squad, atPosition, tokenSource);
            var task = await squad.MoveTo(atPosition, tokenSource.Token);
            
            if (task)
                EnemiesCreated?.Invoke(squad);
        }

        public void SpawnRams(int levelNumber)
        {
            var factory = new RamsFactory();
            var squad = new Squad<Ram>(GameConstants.MaxRams, new RamsPlacementStrategy());
            //var leader = SpawnLeader(factory);

            //squad.Add(leader);

            if (_selectedRamsId.Count <= 0)
            {
                RamsCreated?.Invoke(squad);
                return;
            }

            foreach (var id in _selectedRamsId)
            {
                var ram = factory.Create(id, _ramsSpawnPoint.position);
                ram.transform.SetParent(_ramsContainer);
                squad.Add(ram);
            }

            squad.OnComplete(levelNumber);
            RamsCreated?.Invoke(squad);
        }

        // private Ram SpawnLeader(IUnitFactory<Unit> factory)
        // {
        //     var leader = factory.CreateLeader(_leaderData, _ramsSpawnPoint.position);
        //     leader.transform.SetParent(_ramsContainer);
        //
        //     return leader as Ram;
        // }
    }
}