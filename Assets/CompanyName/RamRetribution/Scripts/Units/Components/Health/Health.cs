using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Health
{
    public class Health : IDamageable
    {
        private int _value;
        private readonly IArmor _armor;
        
        public Health(int value, IArmor armor)
        {
            _value = value;
            _armor = armor;
        }

        public event Action<int> ValueChanged;
        public event Action HealthEnded;
        
        public int Value => _value;
        public float ArmorValue => _armor.Value;
        
        public void TakeDamage(AttackType type, int damage)
        {
            if (damage < 0)
                throw new ArgumentException("Damage can`t be less than 0");

            var reducedDamage = _armor.ReduceDamage(type, damage);

            if (reducedDamage == 0)
                reducedDamage = 1;
            
            _value -= reducedDamage;
            
            ValueChanged?.Invoke(_value);
            
            if(_value <= 0)
                HealthEnded?.Invoke();
        }

        public void Restore(int amount)
        {
            
        }
    }
}