using System;
using System.Collections;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Gameplay.Level;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private Vector3 _ramsStartPosition;
        [SerializeField] private List<Transform> _enemySpots;

        private Gate _gate;
        private LevelConfigurator _configuration;
        private Gate _currentGate;
        private Coroutine _buildCoroutine;

        public event Action GateDestroyed;

        public Gate CurrentGate { get; private set; }
        public bool IsBuild { get; private set; }
        public bool IsCurrentGateAttackedFirst { get; private set; }
        public Vector3 RamsStartPosition => _ramsStartPosition;
        public List<Transform> EnemySpots => _enemySpots;

        public void Init()
        {
            _gate = FindObjectOfType<Gate>();
            IsBuild = false;
        }

        public void SetLevelConfiguration()
        {
            //_unitSpawner.SetEnemiesPoints(configuration.GetEnemyPoints());
        }

        public void Build()
        {
            if (_buildCoroutine != null)
                StopCoroutine(_buildCoroutine);

            _buildCoroutine = StartCoroutine(Construct());
        }

        private IEnumerator Construct()
        {
            CurrentGate = _gate;
            CurrentGate.Init(new GateHealth(10000));

            CurrentGate.FirstAttacked += OnGateFirstAttacked;
            CurrentGate.Damageable.HealthEnded += OnGateDestroyed;
            yield return new WaitForSecondsRealtime(3f);

            IsCurrentGateAttackedFirst = false;
            IsBuild = true;
        }

        private void OnGateDestroyed()
        {
            IsBuild = false;
            GateDestroyed?.Invoke();
        }

        private void OnGateFirstAttacked()
        {
            CurrentGate.FirstAttacked -= OnGateFirstAttacked;
            IsCurrentGateAttackedFirst = true;
        }
    }
}