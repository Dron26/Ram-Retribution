using System;
using _Project_.CodeBase.Scripts.Interfaces;

namespace _Project_.CodeBase.Scripts.Leader
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
        
        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                throw new NotImplementedException(message: "Damage can`t be less than 0");
            
            ApplyArmorEffect(damage);
        }

        private int ApplyArmorEffect(int damage)
        {
            //эффект брони = armor / armor + baseArmorCoefficient 
            // damage -= эффект брони;

            return damage;
        }
    }
}