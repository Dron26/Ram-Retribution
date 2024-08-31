using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Visitors.Variants;
using CompanyName.RamRetribution.Scripts.Factories.Units;
using CompanyName.RamRetribution.Scripts.Factories.Units.Variant;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
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
        private readonly ConfigsContainer _configsContainer;
        private readonly IResourceLoadService _loadService;

        private readonly Transform _ramsSpawnPoint;
        private readonly Transform _ramsContainer;
        private readonly Transform _enemiesContainer;

        private readonly RamFactory _ramFactory;
        private readonly Squad<Ram> _ramSquad;

        private EnemyObjectsPool _enemyObjectsPool;
        private LocationTypes _locationType = LocationTypes.None;

        public UnitSpawner(
            Leader leader,
            RamFactory ramFactory,
            Transform ramsSpawnPoint,
            Transform ramsContainer,
            Transform enemiesContainer)
        {
            _ramFactory = ramFactory;
            _ramsSpawnPoint = ramsSpawnPoint;
            _ramsContainer = ramsContainer;
            _enemiesContainer = enemiesContainer;

            var placementStrategy = new RamsPlacementStrategy(new RamsPlacementVisitor(spaceBetweenMembers: 1.5f));
            _ramSquad = new Squad<Ram>(GameConstants.MaxRamsWithoutLeader).SetPlacementStrategy(placementStrategy);
            
            leader.transform.SetParent(_ramsContainer);
            _ramSquad.Add(leader);
        }

        public event Action<Squad<Ram>> RamsCreated;
        public event Action<Squad<Enemy>> EnemiesCreated;

        public async UniTask<bool> SpawnEnemies(int levelNumber, IReadOnlyList<ConfigId> configsId, Vector3 at,
            CancellationToken token)
        {
            TryChangeType(levelNumber);

            var squad = new Squad<Enemy>(configsId.Count).SetPlacementStrategy(new CirclePlacementStrategy(1f,1.5f));

            foreach (var id in configsId)
            {
                var enemy = _enemyObjectsPool.Get(id);
                squad.Add(enemy);
            }

            var atPosition = Vector3.zero.With(
                x: at.x,
                z: at.z - 1f);
            
            var task = await squad.MoveTo(atPosition, token);

            if (!task)
                return false;

            EnemiesCreated?.Invoke(squad);
            return true;
        }

        public void SpawnRams(IReadOnlyList<ConfigId> selectedRams, int levelNumber)
        {
            if (selectedRams.Count <= 0)
            {
                RamsCreated?.Invoke(_ramSquad);
                return;
            }

            foreach (var ram in selectedRams.Select(id => _ramFactory.CreateByConfig(id, _ramsSpawnPoint.position)))
            {
                ram.transform.SetParent(_ramsContainer);
                _ramSquad.Add(ram);
            }

            _ramSquad.OnComplete(levelNumber);
            RamsCreated?.Invoke(_ramSquad);
        }

        private void TryChangeType(int levelNumber)
        {
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
            SetFactory();
        }

        private void SetFactory()
        {
            EnemyFactory enemyFactory = _locationType switch
            {
                LocationTypes.Forest => new ForestEnemiesFactory(_configsContainer, _loadService),
                LocationTypes.Sand => new SandEnemiesFactory(_configsContainer, _loadService),
                LocationTypes.Ice => new IceEnemiesFactory(_configsContainer, _loadService),
                LocationTypes.None => throw new ArgumentException(
                    $"You must set locationType before creating Tiles pool and factories"),
                _ => throw new ArgumentOutOfRangeException()
            };

            var enemiesForEachType = GameConstants.MaxEnemiesInSquad;
            _enemyObjectsPool = new EnemyObjectsPool(enemyFactory, container: _enemiesContainer);
            _enemyObjectsPool.Create(enemiesForEachType);
        }
    }
}