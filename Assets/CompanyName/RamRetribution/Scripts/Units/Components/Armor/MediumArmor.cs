using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Armor
{
    public class MediumArmor : BaseArmor
    {
        private const float ReduceRangeAttack = 0.75f;
        private const float ReduceMeleeAttack = 0.65f;
        
        public MediumArmor(int value) : base(value)
        {
        }

        public override int ReduceDamage(AttackType type, float damage)
        {
            return type switch
            {
                AttackType.Melee => base.ReduceDamage(type, damage * ReduceMeleeAttack),
                AttackType.Range => base.ReduceDamage(type, damage * ReduceRangeAttack),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}