using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Buildings
{
    public abstract class Gate : MonoBehaviour
    {
        private bool _isFirstAttack;
        
        public event Action FirstAttacked;
        public IDamageable Damageable { get; private set; }
        public abstract GateTypes Type { get; }

        private void OnDestroy()
        {
            Damageable.ValueChanged -= OnValueChanged;
        }

        public void Init(IDamageable damageable)
        {
            Damageable = damageable;
            Damageable.ValueChanged += OnValueChanged;
        }
        
        private void OnValueChanged(int value)
        {
            if (!_isFirstAttack)
            {
                FirstAttacked?.Invoke();
                _isFirstAttack = true;
            }
        }
    }
}