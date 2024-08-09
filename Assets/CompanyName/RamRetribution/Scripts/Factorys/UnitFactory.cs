using System;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components.Armor;
using CompanyName.RamRetribution.Scripts.Units.Components.Attack;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class UnitFactory : IUnitFactory
    {
        private readonly ConfigsContainer _configsContainer;
        
        public UnitFactory(ConfigsContainer configsContainer) 
            => _configsContainer = configsContainer;

        public Unit CreateLeader(LeaderDataState leaderData, Vector3 at)
        {
            var prefab = _configsContainer.Get(ConfigId.Leader).Prefab;
            var leader = Object.Instantiate(prefab, at, Quaternion.identity);

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
        
        public Unit Create(ConfigId configId, Vector3 at)
        {
            var config = _configsContainer.Get(configId);
            var instance = Object.Instantiate(config.Prefab, at, Quaternion.identity);

            var healthComponent = GetHealth(config);
            var attackComponent = GetAttack(config);
            
            instance.Init(healthComponent, attackComponent, config.Priority);
            
            return instance;
        }
        
        private static IDamageable GetHealth(UnitConfig config)
        {
            IArmor armor = config.ArmorType switch
            {
                ArmorTypes.Light => new LightArmor(config.ArmorValue),
                ArmorTypes.Medium => new MediumArmor(config.ArmorValue),
                ArmorTypes.Heavy => new HeavyArmor(config.ArmorValue),
                _ => throw new ArgumentOutOfRangeException(nameof(config.ArmorType), config.ArmorType, null)
            };

            return new Health(config.HealthValue, armor);
        }
        
        private static IAttackComponent GetAttack(UnitConfig config)
        {
            return config.AttackType switch
            {
                AttackType.Melee => new MeleeAttack(config.Damage, config.AttackSpeed),
                AttackType.Range => new RangeAttack(config.Damage,config.AttackSpeed,config.AttackDistance),
                _ => throw new ArgumentOutOfRangeException(nameof(config.AttackType), config.AttackType, null)
            };
        }
    }
}