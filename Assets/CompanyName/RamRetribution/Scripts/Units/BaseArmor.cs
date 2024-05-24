using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Ram
{
    public class BaseArmor : IArmor
    {
        private readonly int _value;
        private readonly int _reduceCoefficient;

        public BaseArmor(int value, int reduceCoefficient)
        {
            _value = value;
            _reduceCoefficient = reduceCoefficient;
        }

        public int ReduceDamage(int damage)
        {
            int reduceValue = _value / _value + _reduceCoefficient;

            damage -= reduceValue;

            if (damage <= 0)
                return 0;

            return damage;
        }
    }
}