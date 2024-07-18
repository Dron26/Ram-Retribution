using System;
using System.Collections.Generic;
using System.Threading;
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
        [SerializeField] private LevelConfigurator _nextLevelConfigurator;
        [SerializeField] private LevelConfigurator _currentLevelConfigurator;
        [SerializeField] private NavMeshSurface _surface;

        public Vector3 RamsStartPosition => _currentLevelConfigurator.RamsStartPosition;
        public Gate CurrentGate { get; private set; }
        public bool IsBuild { get; private set; }
        public bool IsCurrentGateAttackedFirst { get; private set; }
        public bool IsCurrentGateDestroyed { get; private set; }
        public List<Transform> EnemySpots => _currentLevelConfigurator.EnemySpawnPoint;
        public event Action GateDestroyed;

        private LevelConfigurator _configuration;
        private Gate _currentGate;
        private UniTask _buildTask;
        private bool _isStartedLocationsSetted;
        private CancellationTokenSource _tokenSource;

        public void Init()
        {
            Reset();
            
            var gridType = GridType.Forest; // = Config.GetGridType
            var gridId = 0;
            
            _currentLevelConfigurator.SetData(gridType, gridId, false);
            _nextLevelConfigurator.SetData(gridType, gridId++, true);
        }

        public async UniTask Build(CancellationTokenSource tokenSource)
        {
            _tokenSource = tokenSource;
            
            if (!_isStartedLocationsSetted)
            {
                await BuildLevel(_currentLevelConfigurator).WithCancellation(_tokenSource.Token);
                
                SetGateParameters(_currentLevelConfigurator.Gate);
                _isStartedLocationsSetted = true;
            }
            else
            {
                TransitionToNextLevel();
            }
            
            _buildTask = BuildLevel(_nextLevelConfigurator);
            
            await _buildTask.WithCancellation(_tokenSource.Token);
        }
        
        private void BuildNavMesh()
        {
            _surface.RemoveData();
            _surface.BuildNavMesh();
        }

        private async UniTask BuildLevel(LevelConfigurator configurator)
        {
            await configurator.GenerateGridAsync(_tokenSource);
            
            await UniTask.Delay(TimeSpan.FromSeconds(1f), DelayType.Realtime)
                .WithCancellation(_tokenSource.Token);
            
            BuildNavMesh();
            
            IsCurrentGateDestroyed = false;
            IsBuild = true;
        }

        private void SetGateParameters(Gate gate)
        {
            CurrentGate = gate;
            CurrentGate.Init(new GateHealth(1000));
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
            IsCurrentGateAttackedFirst = true;
        }

        private void TransitionToNextLevel()
        {
            (_currentLevelConfigurator, _nextLevelConfigurator) = (_nextLevelConfigurator, _currentLevelConfigurator);
            SetGateParameters(_currentLevelConfigurator.Gate);
            _currentLevelConfigurator.ShowTiles();
            var nextGridId = _currentLevelConfigurator.GridId + 1;
            _nextLevelConfigurator.SetData(GridType.Forest, nextGridId, true);
        }

        private void Reset()
        {
            IsCurrentGateDestroyed = false;
            IsBuild = false;
        }
    }
}