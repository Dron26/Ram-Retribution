using System;
using System.Collections;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Gameplay.Level;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using UnityEngine;
using UnityEngine.Serialization;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private List<Transform> _enemySpots;
        
        private Gate _gate;
        private LevelConfigurator _configuration;
        private Gate _currentGate;
        private Coroutine _buildCoroutine;

        public event Action GateAttackedFirst; 
        
        public Gate CurrentGate { get; private set; }
        public bool IsBuild { get; private set; }
        public bool IsCurrentGateDestroyed { get; private set; }
        public List<Transform> EnemySpots => _enemySpots;
        
        public void Init()
        {
            _gate = FindObjectOfType<Gate>();
            IsCurrentGateDestroyed = false;
            IsBuild = false;
        }

        public void SetLevelConfiguration(LevelConfigurator configuration)
        {
            _configuration = configuration;
            //_unitSpawner.SetEnemiesPoints(configuration.GetEnemyPoints());
        }
        
        public void Build()
        {
            if(_buildCoroutine != null)
                StopCoroutine(_buildCoroutine);

            _buildCoroutine = StartCoroutine(Construct());
        }

        private IEnumerator Construct()
        {
            CurrentGate = _gate;
            CurrentGate.Init(new GateHealth(500));
            
            CurrentGate.FirstAttacked += OnGateFirstAttacked;
            CurrentGate.Damageable.HealthEnded += OnGateDestroyed;
            yield return new WaitForSecondsRealtime(3f);
            
            IsCurrentGateDestroyed = false;
            IsBuild = true;
        }

        private void OnGateDestroyed()
        {
            Debug.Log($"Gate destroyed");
            IsCurrentGateDestroyed = true;
            IsBuild = false;
        }

        private void OnGateFirstAttacked()
        {
            CurrentGate.FirstAttacked -= OnGateFirstAttacked;
            GateAttackedFirst?.Invoke();
        }
    }
}