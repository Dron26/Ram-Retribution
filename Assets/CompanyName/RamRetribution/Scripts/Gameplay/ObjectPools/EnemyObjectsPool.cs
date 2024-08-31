using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factories.Units;
using CompanyName.RamRetribution.Scripts.Units.Enemies;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class EnemyObjectsPool
    {
        private readonly Dictionary<ConfigId, Queue<Enemy>> _subPools;
        private readonly Dictionary<ConfigId, HashSet<Enemy>> _inUseObjects;
        private readonly Transform _container;
        private readonly EnemyFactory _factory;

        public EnemyObjectsPool(EnemyFactory factory, Transform container)
        {
            _factory = factory;
            _subPools = new Dictionary<ConfigId, Queue<Enemy>>();
            _inUseObjects = new Dictionary<ConfigId, HashSet<Enemy>>();
            _container = container;
        }

        public void Create(int membersCountForEachType)
        {
            ConfigId[] enemiesIds = { ConfigId.LightEnemy, ConfigId.MediumEnemy, ConfigId.HeavyEnemy };

            foreach (var id in enemiesIds)
            {
                _subPools[id] = new Queue<Enemy>();
                _inUseObjects[id] = new HashSet<Enemy>();

                for (var i = 0; i < membersCountForEachType; i++)
                {
                    var enemy = _factory.CreateByConfig(id);
                    _subPools[id].Enqueue(enemy);
                    enemy.transform.SetParent(_container);
                }
            }
        }

        public Enemy Get(ConfigId id)
        {
            if (!_subPools.TryGetValue(id, out var enemies))
                throw new Exception($"No pool exists for config ID: {id}");
            
            if (enemies.Count == 0)
            {
                var newObject = _factory.CreateByConfig(id);
                enemies.Enqueue(newObject);
            }

            var instance = enemies.Dequeue();
            _inUseObjects[id].Add(instance);
            return instance;
        }

        public void Return(Enemy enemy)
        {
        }
    }
}