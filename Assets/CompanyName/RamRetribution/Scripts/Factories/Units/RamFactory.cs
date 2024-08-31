using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factories.ComponentsFactory;
using CompanyName.RamRetribution.Scripts.Units.Components.Armor;
using CompanyName.RamRetribution.Scripts.Units.Components.Attack;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using CompanyName.RamRetribution.Scripts.Units.Components.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CompanyName.RamRetribution.Scripts.Factories.Units
{
    public class RamFactory
    {
        private readonly ConfigsContainer _configsContainer;
        private readonly BuffsContainer _buffsContainer;
        private readonly UnitComponentsFactory _componentsFactory;
        private readonly Dictionary<ConfigId, Func<ConfigId, Vector3, Ram>> _creators;

        public RamFactory(ConfigsContainer configsContainer, BuffsContainer buffsContainer)
        {
            _configsContainer = configsContainer;
            _buffsContainer = buffsContainer;
            _componentsFactory = new UnitComponentsFactory();

            _creators = new Dictionary<ConfigId, Func<ConfigId, Vector3, Ram>>()
            {
                { ConfigId.Attacan, Create<Attacker> },
                { ConfigId.Baron, Create<Attacker> },
                { ConfigId.Demol, Create<Demolisher> },
                { ConfigId.Fogos, Create<Demolisher> },
                { ConfigId.Largos, Create<Support> },
                { ConfigId.Suppy, Create<Support> },
                { ConfigId.Tanky, Create<Tank> },
            };
        }

        public Leader CreateLeader(LeaderDataState leaderData, Vector3 at)
        {
            var prefab = _configsContainer.Get(ConfigId.Leader).Prefab;
            Object.Instantiate(prefab, at, Quaternion.identity).TryGetComponent(out Leader leader);

            IAttackComponent attackComponent = new MeleeAttack(leaderData.Damage, leaderData.AttackSpeed);

            IDamageable healthComponent = leaderData.ArmorType switch
            {
                ArmorTypes.Light => new Health(leaderData.HealthValue, new LightArmor(leaderData.ArmorValue)),
                ArmorTypes.Medium => new Health(leaderData.HealthValue, new MediumArmor(leaderData.ArmorValue)),
                ArmorTypes.Heavy => new Health(leaderData.HealthValue, new HeavyArmor(leaderData.ArmorValue)),
                _ => throw new ArgumentOutOfRangeException()
            };

            leader.Init(healthComponent, attackComponent, PriorityTypes.Leader);

            return leader;
        }

        public Ram CreateByConfig(ConfigId id, Vector3 at)
        {
            if (_creators.TryGetValue(id, out Func<ConfigId, Vector3, Ram> creator))
                return creator(id, at);

            throw new ArgumentException($"No creator found for ConfigId: {id}");
        }

        private Ram Create<T>(ConfigId id, Vector3 at)
            where T : Ram
        {
            var config = _configsContainer.Get(id);
            var prefab = config.Prefab;
            var instance = Object.Instantiate(prefab, at, Quaternion.identity).GetComponent<T>();

            var healthComponent = _componentsFactory.CreateHealth(config);
            var attackComponent = _componentsFactory.CreateAttack(config);

            instance.Init(healthComponent, attackComponent, config.Priority);
            instance.AddBuff(_buffsContainer.Get(id));

            return instance;
        }
    }
}