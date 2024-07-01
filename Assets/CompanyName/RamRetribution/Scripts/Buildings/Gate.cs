using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Buildings
{
    public abstract class Gate : MonoBehaviour, IAttackable
    {
        private bool _isFirstAttack;
        
        public event Action FirstAttacked;
        public IDamageable Damageable { get; private set; }
        public Transform SelfTransform { get; private set; }
        public bool IsActive { get; private set; }
        public abstract GateTypes Type { get; }

        private void OnDestroy()
        {
            Damageable.ValueChanged -= OnValueChanged;
            Damageable.HealthEnded -= OnHealthEnded;
        }

        public void Init(IDamageable damageable)
        {
            Damageable = damageable;
            Damageable.ValueChanged += OnValueChanged;
            Damageable.HealthEnded += OnHealthEnded;

            IsActive = true;
            SelfTransform = transform;
            
            gameObject.SetActive(IsActive);
        }
        
        private void OnValueChanged(int value)
        {
            if (!_isFirstAttack)
            {
                FirstAttacked?.Invoke();
                _isFirstAttack = true;
            }
        }

        private void OnHealthEnded()
        {
            IsActive = false;
            gameObject.SetActive(IsActive);
        }
    }
}