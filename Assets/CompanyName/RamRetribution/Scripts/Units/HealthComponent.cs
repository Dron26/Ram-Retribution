using System;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Ram
{
    public class HealthComponent : IDamageable
    {
        private int _healthValue;
        private readonly IArmor _armor;
        
        public HealthComponent(int healthValue, IArmor armor)
        {
            _healthValue = healthValue;
            _armor = armor;
        }

        public int Value => _healthValue;
        public event Action Died;
        
        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentException("Damage can`t be less than 0");

            int reducedDamage = _armor.ReduceDamage(damage);

            if(reducedDamage == 0)
                return;
            
            _healthValue -= reducedDamage;
            
            if(_healthValue <= 0)
                Died?.Invoke();
        }
    }
}