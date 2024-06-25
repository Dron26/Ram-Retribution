using System;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
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
            var prefab = _configsContainer.GetConfig("Leader").Prefab;
            var leader = Object.Instantiate(prefab, at, Quaternion.identity);
            
            IDamageable healthComponent;
            IAttackComponent attackComponent = new MeleeAttack(leaderData.Damage, leaderData.AttackSpeed);
            
            switch (leaderData.ArmorType)
            {
                case ArmorTypes.Light:
                    healthComponent = new AIHealth(leaderData.HealthValue, new LightArmor(leaderData.ArmorValue));
                    break;
                case ArmorTypes.Medium:
                    healthComponent = new AIHealth(leaderData.HealthValue, new MediumArmor(leaderData.ArmorValue));
                    break;
                case ArmorTypes.Heavy:
                    healthComponent = new AIHealth(leaderData.HealthValue, new HeavyArmor(leaderData.ArmorValue));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            leader.Init(healthComponent, attackComponent, 0);
            return leader;
        }
        
        public Unit Create(string configId, Vector3 at)
        {
            var config = _configsContainer.GetConfig(configId);
            var instance = Object.Instantiate(config.Prefab, at, Quaternion.identity);
            var unitComponent = instance.GetComponent<Unit>();

            var healthComponent = GetHealth(config);
            var attackComponent = GetAttack(config);
            
            unitComponent.Init(healthComponent, attackComponent, (int)config.Priority);
            
            return unitComponent;
        }
        
        private static IDamageable GetHealth(UnitConfig config)
        {
            IArmor armor = null;

            switch (config.ArmorType)
            {
                case ArmorTypes.Light:
                    armor = new LightArmor(config.ArmorValue);
                    break;
                case ArmorTypes.Medium:
                    armor = new MediumArmor(config.ArmorValue);
                    break;
                case ArmorTypes.Heavy:
                    armor = new HeavyArmor(config.ArmorValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(config.ArmorType), config.ArmorType, null);
            }

            return new AIHealth(config.HealthValue, armor);
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