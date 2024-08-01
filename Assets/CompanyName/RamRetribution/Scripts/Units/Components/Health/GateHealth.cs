using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Health
{
    public class GateHealth : IDamageable
    {
        private const float MeleeReduceCoefficient = 0.75f;
        private const float RangeReduceCoefficient = 0.45f;
        
        private int _value;
        
        public GateHealth(int value)
        {
            _value = value;
        }

        public event Action<int> ValueChanged;
        public event Action HealthEnded;
        public int Health => _value;
        
        public void TakeDamage(AttackType type, int damage)
        {
            _value -= type switch
            {
                AttackType.Melee => Mathf.FloorToInt(damage * MeleeReduceCoefficient),
                AttackType.Range => Mathf.FloorToInt(damage * RangeReduceCoefficient),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            ValueChanged?.Invoke(_value);

            if (_value <= 0)
            {
                HealthEnded?.Invoke();
                Debug.Log($"Gate health ended");
            }
            
            // Debug.Log($"Gate health {_value}");
        }

        public void Restore(int amount)
        {
            throw new System.NotImplementedException();
        }
    }
}