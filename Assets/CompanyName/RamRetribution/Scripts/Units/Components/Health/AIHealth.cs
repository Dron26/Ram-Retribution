using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Health
{
    public class AIHealth : IDamageable
    {
        private float _healthValue;
        private readonly IArmor _armor;
        
        public AIHealth(int healthValue, IArmor armor)
        {
            _healthValue = healthValue;
            _armor = armor;
        }

        public float Value => _healthValue;
        public event Action Died;
        
        public void TakeDamage(AttackType type ,int damage)
        {
            if (damage < 0)
                throw new ArgumentException("Damage can`t be less than 0");

            float reducedDamage = _armor.ReduceDamage(type, damage);

            if(reducedDamage == 0)
                return;
            
            _healthValue -= reducedDamage;
            
            if(_healthValue <= 0)
                Died?.Invoke();
        }

        public void Heal(int amount)
        {
            
        }
    }
}