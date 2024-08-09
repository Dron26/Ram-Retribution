using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using Cysharp.Threading.Tasks;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Health
{
    public class Health : IDamageable
    {
        private readonly IArmor _armor;
        private readonly int _baseValue;
        
        private int _currentValue;
        private int _maxValue;

        private int _regeneration;
        
        public Health(int baseValue, IArmor armor)
        {
            _baseValue = baseValue;
            _currentValue = _baseValue;
            _maxValue = _baseValue;
            
            _armor = armor;
        }

        public event Action<int> ValueChanged;
        public event Action HealthEnded;
        
        public int CurrentValue => _currentValue;
        public float ArmorValue => _armor.Value;
        
        public void TakeDamage(AttackType type, int damage)
        {
            if (damage < 0)
                throw new ArgumentException("Damage can`t be less than 0");

            var reducedDamage = _armor.ReduceDamage(type, damage);

            if (reducedDamage == 0)
                reducedDamage = 1;
            
            _currentValue -= reducedDamage;
            
            ValueChanged?.Invoke(_baseValue);
            
            if(_baseValue <= 0)
                HealthEnded?.Invoke();
        }

        public void Heal(int amount)
        {
            
        }

        public void Improve(int bonusValue)
        {
            _regeneration += bonusValue;
        }

        private async UniTaskVoid AutoHeal()
        {
            while (_currentValue < _maxValue)
            {
                _currentValue += _regeneration;

                if (_currentValue > _maxValue)
                    _currentValue = _maxValue;

                await UniTask.Delay(
                    TimeSpan.FromSeconds(2f),
                    DelayType.Realtime);
            }
        }
    }
}