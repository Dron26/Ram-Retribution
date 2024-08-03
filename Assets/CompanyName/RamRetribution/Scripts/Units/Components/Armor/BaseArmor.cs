using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Armor
{
    public abstract class BaseArmor : IArmor
    {
        private readonly float _value;

        protected BaseArmor(int value)
        {
            _value = value;
        }

        public float Value => _value;
        
        public virtual int ReduceDamage(AttackType type, float damage)
        {
            damage -= _value;

            return damage <= 0 ? 0 : Mathf.FloorToInt(damage);
        }
    }
}