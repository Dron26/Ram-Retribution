using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factories.ComponentsFactory;
using CompanyName.RamRetribution.Scripts.Units.Enemies;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CompanyName.RamRetribution.Scripts.Factories.Units
{
    public abstract class EnemyFactory
    {
        private readonly ConfigsContainer _configsContainer;
        private readonly UnitComponentsFactory _componentsFactory;
        private readonly List<Enemy> _prefabs;
        private readonly Dictionary<ConfigId, Func<ConfigId, Enemy>> _creators;

        protected EnemyFactory(ConfigsContainer configsContainer, IResourceLoadService loadService,
            string assetFolderPath)
        {
            _configsContainer = configsContainer;
            _componentsFactory = new UnitComponentsFactory();
            _prefabs = loadService.LoadAll<Enemy>(assetFolderPath);

            _creators = new Dictionary<ConfigId, Func<ConfigId, Enemy>>()
            {
                { ConfigId.LightEnemy, Create<LightEnemy> },
                { ConfigId.MediumEnemy, Create<MediumEnemy> },
                { ConfigId.HeavyEnemy, Create<HeavyEnemy> }
            };
        }

        public Enemy CreateByConfig(ConfigId id)
        {
            if (_creators.TryGetValue(id, out Func<ConfigId, Enemy> creator))
                return creator(id);

            throw new ArgumentException($"No creator found for ConfigId: {id}");
        }
        
        private T Create<T>(ConfigId id)
            where T : Enemy
        {
            var prefab = GetPrefab<T>();
            var instance = Object.Instantiate(prefab).GetComponent<T>();

            var config = _configsContainer.Get(id);
            var healthComponent = _componentsFactory.CreateHealth(config);
            var attackComponent = _componentsFactory.CreateAttack(config);

            instance.Init(healthComponent, attackComponent, config.Priority);

            return instance;
        }

        private Enemy GetPrefab<T>()
            where T : Enemy
        {
            foreach (var enemy in _prefabs.Where(enemy => enemy.GetType() == typeof(T)))
                return enemy;

            throw new ArgumentOutOfRangeException(
                $"Prefab of type {typeof(T).Name} not found in the available prefabs.");
        }
    }
}