using System;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units.Components.Armor;
using CompanyName.RamRetribution.Scripts.Units.Components.Attack;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using CompanyName.RamRetribution.Scripts.Units.Components.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Factories.ComponentsFactory
{
    public class UnitComponentsFactory
    {
        public IDamageable CreateHealth(UnitConfig config)
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

        public IAttackComponent CreateAttack(UnitConfig config)
        {
            return config.AttackType switch
            {
                AttackType.Melee => new MeleeAttack(config.Damage, config.AttackSpeed),
                AttackType.Range => new RangeAttack(config.Damage, config.AttackSpeed, config.AttackDistance),
                _ => throw new ArgumentOutOfRangeException(nameof(config.AttackType), config.AttackType, null)
            };
        }
    }
}