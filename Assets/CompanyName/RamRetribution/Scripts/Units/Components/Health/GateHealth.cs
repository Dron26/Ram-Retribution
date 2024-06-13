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

        public int Value => _value;
        
        public void TakeDamage(AttackType type, int damage)
        {
            switch (type)
            {
                case AttackType.Melee:
                    _value -= Mathf.FloorToInt(damage * MeleeReduceCoefficient);
                    break;
                case AttackType.Range:
                    _value -= Mathf.FloorToInt(damage * RangeReduceCoefficient);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            
            Debug.Log($"Gate value: {Value}");
        }

        public void Heal(int amount)
        {
            throw new System.NotImplementedException();
        }
    }
}