using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using Cysharp.Threading.Tasks;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Health
{
    public class Health : IDamageable
    {
        private readonly IArmor _armor;
        private readonly float _baseValue;
        
        private float _currentValue;
        private float _maxValue;

        private int _regeneration;
        
        public Health(float baseValue, IArmor armor)
        {
            _baseValue = baseValue;
            _currentValue = _baseValue;
            _maxValue = _baseValue;
            
            _armor = armor;
        }

        public event Action<float> ValueChanged;
        public event Action HealthEnded;
        
        public float CurrentValue => _currentValue;
        public ref float ArmorValue => ref _armor.Value;
        
        public void TakeDamage(AttackType type, float damage)
        {
            if (damage < 0)
                throw new ArgumentException("Damage can`t be less than 0");

            var reducedDamage = _armor.ReduceDamage(type, damage);

            if (reducedDamage == 0)
                reducedDamage = 1;
            
            _currentValue -= reducedDamage;
            
            ValueChanged?.Invoke(_currentValue);
            
            if(_currentValue <= 0)
                HealthEnded?.Invoke();
        }

        public void Heal(int amount)
        {
            
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