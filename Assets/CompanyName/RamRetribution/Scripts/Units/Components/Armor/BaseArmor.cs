using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Components
{
    public abstract class BaseArmor : IArmor
    {
        private readonly float _value;

        protected BaseArmor(int value)
        {
            _value = value;
        }

        public virtual int ReduceDamage(AttackType type, float damage)
        {
            damage -= _value;

            return damage <= 0 ? 0 : Mathf.FloorToInt(damage);
        }
    }
}