using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Buildings
{
    public class Gate : MonoBehaviour, IAttackble
    {
        [SerializeField] private Transform _pointsForAttackContainer;
        
        private bool _isFirstAttack;

        public event Action FirstAttacked;
        public IDamageable Damageable { get; private set; }
        public Transform SelfTransform { get; private set; }
        public List<Transform> PointsForAttack { get; } = new List<Transform>();
        public bool IsActive { get; private set; }

        private void OnDestroy()
        {
            if (SelfTransform == null)
                return;

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

            var pointsCount = _pointsForAttackContainer.childCount;

            for (var i = 0; i < pointsCount; i++)
                PointsForAttack.Add(_pointsForAttackContainer.GetChild(i));
            
            gameObject.SetActive(IsActive);
        }

        private void OnValueChanged(float value)
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