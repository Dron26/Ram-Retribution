using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components
{
    public abstract class BaseArmor : IArmor
    {
        private readonly float _value;
        private readonly int _reduceCoefficient;

        protected BaseArmor(int value, int reduceCoefficient)
        {
            _value = value;
            _reduceCoefficient = reduceCoefficient;
        }

        public virtual float ReduceDamage(AttackType type, float damage)
        {
            float reduceValue = _value / _value + _reduceCoefficient;
            
            damage -= reduceValue;

            if (damage <= 0)
                return 0;

            return damage;
        }
    }
}