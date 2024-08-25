using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Buildings
{
    public class Gate : MonoBehaviour, IAttackble
    {
        [SerializeField] private Transform _doorTransform;
        [SerializeField] private Transform _pointsForAttackContainer;

        private Animator _animator;
        private bool _isFirstAttack;

        public event Action<Gate> FirstAttacked;
        public IDamageable Damageable { get; private set; }
        public Transform SelfTransform { get; private set; }
        public List<Transform> PointsForAttack { get; } = new List<Transform>();
        public bool IsActive { get; private set; }

        private void Awake()
            => _animator = GetComponent<Animator>();

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

            var pointsCount = _pointsForAttackContainer.childCount;

            for (var i = 0; i < pointsCount; i++)
                PointsForAttack.Add(_pointsForAttackContainer.GetChild(i));
        }

        private void OnValueChanged(float value)
        {
            if (_isFirstAttack)
                return;

            FirstAttacked?.Invoke(this);
            _isFirstAttack = true;
        }

        private void OnHealthEnded(IDamageable damageable)
        {
            damageable.HealthEnded -= OnHealthEnded;
            IsActive = false;

            var rotateVector = Vector3.zero.With(x: -90, z: 90);
            _doorTransform.DORotate(rotateVector, 1.5f);
        }
    }
}