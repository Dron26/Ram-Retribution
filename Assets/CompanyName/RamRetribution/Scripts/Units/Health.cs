using System;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Ram
{
    public class Health : IDamageable
    {
        private int _value;
        private int _armor;
        private int _max;

        public Health(int max, int armor = 0)
        {
            _max = max;
            _armor = armor;
            _value = _max;
        }
        
        public event Action<int> ValueChanged;
        public int Value => _value;
        public int Armor => _armor;
        public int TotalArmor => _armor * 100;
        
        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                throw new NotImplementedException(message: "Damage can`t be less than 0");
            
            ApplyArmorEffect(damage);
            
            ValueChanged?.Invoke(_value);
        }

        private int ApplyArmorEffect(int damage)
        {
            //эффект брони = armor / armor + baseArmorCoefficient 
            // damage -= эффект брони;
            
            return damage;
        }
    }
}