using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using Cysharp.Threading.Tasks;
using Generator.Scripts.Common.Enum;
using Generator.Scripts.Level;
using Unity.AI.Navigation;
using UnityEngine;

namespace Generator.Scripts
{
    public class LevelBuilder : MonoBehaviour
    {
        public Vector3 RamsStartPosition => _currentLevelConfigurator.RamsStartPosition;
        public List<Transform> EnemySpawnPoint => _currentLevelConfigurator.EnemySpawnPoint;
        public bool IsCurrentGateAttackedFirst=>_isCurrentGateAttackedFirst;
        public bool _isCurrentGateAttackedFirst;

        public Gate CurrentGate { get; private set; }
        public bool IsBuild { get; private set; }
        public bool IsCurrentGateDestroyed { get; private set; }
        public List<Transform> EnemySpots => _enemySpots;
        public event Action OnLevelBuildComplete;
        public event Action GateDestroyed;
        
        [SerializeField] private List<Transform> _enemySpots;
        [SerializeField] private LevelConfigurator _nextLevelConfigurator;
        [SerializeField] private LevelConfigurator _currentLevelConfigurator;
        [SerializeField] private NavMeshSurface _surface;

        private LevelConfigurator _configuration;
        private Gate _currentGate;
        private UniTask _buildTask;
        private bool _isStartedLocationsSet;
        
        public void Init()
        {
            IsCurrentGateDestroyed = false;
            IsBuild = false;

            GridType gridType = GridType.Forest;
            int gridId = 0;
            InitializeConfigurator(_currentLevelConfigurator, gridType, gridId, false);
            InitializeConfigurator(_nextLevelConfigurator, gridType, gridId + 1, true);
        }

        private void InitializeConfigurator(LevelConfigurator configurator, GridType gridType, int gridId, bool isHideTiles)
        {
            configurator.Init();
            configurator.SetData(gridType, gridId, isHideTiles);
        }
        
        public async UniTask Build()
        {
            if (!_isStartedLocationsSet)
            {
                await BuildLevel(_currentLevelConfigurator);
                _buildTask = BuildLevel(_nextLevelConfigurator);
                SetGateParameters(_currentLevelConfigurator.GetGate());
                _isStartedLocationsSet = true;
            }
            else
            {
                TransitionToNextLevel();
                _buildTask = BuildLevel(_nextLevelConfigurator);  
            }

            OnLevelBuildComplete?.Invoke();
            await _buildTask;
        }

        
        private void SetSurface()
        {
            _surface.RemoveData();
            _surface.BuildNavMesh();
        }

        private void CreateStartedLocations(GridType gridType, int gridId)
        {
            _currentLevelConfigurator.SetData(gridType, gridId, false);
            _nextLevelConfigurator.SetData(gridType, gridId + 1, true);
        }
        
        private async UniTask BuildLevel(LevelConfigurator configurator)
        {
            await configurator.GenerateGridAsync();
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            SetSurface();
            IsCurrentGateDestroyed = false;
            IsBuild = true;
        }

        private void SetGateParameters(Gate gate)
        {
            CurrentGate = gate;
            CurrentGate.Init(new GateHealth(100));
            CurrentGate.FirstAttacked += OnGateFirstAttacked;
            CurrentGate.Damageable.HealthEnded += OnGateDestroyed;
        }

        private void OnGateDestroyed()
        {
            Debug.Log($"Gate destroyed");
            IsCurrentGateDestroyed = true;
            IsBuild = false;
            GateDestroyed?.Invoke();
        }

        private void OnGateFirstAttacked()
        {
            CurrentGate.FirstAttacked -= OnGateFirstAttacked;
            _isCurrentGateAttackedFirst=true;
        }
        
        private void TransitionToNextLevel()
        {
            (_currentLevelConfigurator, _nextLevelConfigurator) = (_nextLevelConfigurator, _currentLevelConfigurator);
            SetGateParameters(_currentLevelConfigurator.Gate);
            _currentLevelConfigurator.ShowTiles();
            int nextGridId = _currentLevelConfigurator.GridId + 1;
            _nextLevelConfigurator.SetData(GridType.Forest, nextGridId, true);
        }

    }
}