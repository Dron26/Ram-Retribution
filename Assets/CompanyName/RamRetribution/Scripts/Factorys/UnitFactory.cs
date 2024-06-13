using System;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using Object = UnityEngine.Object;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class UnitFactory : IUnitFactory
    {
        public Unit Create(UnitConfig config) 
        {
            var instance = Object.Instantiate(config.Prefab);
            var unitComponent = instance.GetComponent<Unit>();

            var healthComponent = GetHealth(config);
            var attackComponent = GetAttack(config);
            
            unitComponent.Init(healthComponent, attackComponent);
            
            return unitComponent;
        }
        
        private IDamageable GetHealth(UnitConfig config)
        {
            IArmor armor = null;

            switch (config.ArmorType)
            {
                case ArmorTypes.Light:
                    armor = new LightArmor(config.ArmorValue, config.ArmorReduceCoefficient);
                    break;
                case ArmorTypes.Medium:
                    armor = new MediumArmor(config.ArmorValue, config.ArmorReduceCoefficient);
                    break;
                case ArmorTypes.Heavy:
                    armor = new HeavyArmor(config.ArmorValue, config.ArmorReduceCoefficient);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(config.ArmorType), config.ArmorType, null);
            }

            return new AIHealth(config.HealthValue, armor);
        }
        
        private IAttackComponent GetAttack(UnitConfig config)
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