using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components.Attack;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Armor
{
    public class MediumArmor : BaseArmor
    {
        private const float ReduceRangeAttack = 0.75f;
        private const float ReduceMeleeAttack = 0.65f;
        
        public MediumArmor(int value) : base(value)
        {
        }

        public override int ReduceDamage(IAttackComponent attackComponent, float damage)
        {
            return attackComponent switch
            {
                MeleeAttack => base.ReduceDamage(attackComponent, damage * ReduceMeleeAttack),
                RangeAttack => base.ReduceDamage(attackComponent, damage * ReduceRangeAttack),
                _ => throw new ArgumentOutOfRangeException(nameof(attackComponent), attackComponent, null)
            };
        }
    }
}