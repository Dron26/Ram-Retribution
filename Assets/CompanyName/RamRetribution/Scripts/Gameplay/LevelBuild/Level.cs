using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units.Components.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class Level
    {
        private readonly Grid _grid;
        
        public Level(int number, Grid grid)
        {
            Number = number;
            _grid = grid;

            _grid.LeftGate.FirstAttacked += OnGatesAttackedFirst;
            _grid.RightGate.FirstAttacked += OnGatesAttackedFirst;

            _grid.LeftGate.Damageable.HealthEnded += OnGateDestroyed;
            _grid.RightGate.Damageable.HealthEnded += OnGateDestroyed;
        }

        public event Action<GateTypes> GatesDestroyed;

        public int Number { get; }
        public bool IsGateAttackedFirst { get; private set; }

        private Gate LeftGate => _grid.LeftGate;
        private Gate RightGate => _grid.RightGate;

        public Vector3 EnemiesSpawnPoint => _grid.EnemySpots[Random.Range(0, _grid.EnemySpots.Count)];
        public IReadOnlyList<Vector3> EntryTilesPositions => _grid.EntryTiles;

        public Gate GetGateToAttack()
        {
            switch (LeftGate.IsActive)
            {
                case true when RightGate.IsActive:
                    return Random.Range(0, 1) == 0 ? LeftGate : RightGate;
                case true:
                    return LeftGate;
            }

            if (RightGate.IsActive)
                return RightGate;

            throw new InvalidOperationException("Both gate already destroyed.");
        }

        public IReadOnlyList<ConfigId> GetEnemies()
        {
            return new List<ConfigId>
            {
               ConfigId.LightEnemy, 
               ConfigId.LightEnemy, 
               ConfigId.LightEnemy, 
            };
        }

        private void OnGatesAttackedFirst(Gate gate)
        {
            gate.FirstAttacked -= OnGatesAttackedFirst;
            
            if (IsGateAttackedFirst)
                return;
            
            IsGateAttackedFirst = true;
        }

        private void OnGateDestroyed(IDamageable damageable)
        {
            damageable.HealthEnded -= OnGateDestroyed;
            
            if(!LeftGate.IsActive && !RightGate.IsActive)
                GatesDestroyed?.Invoke(LeftGate.Type);
        }
    }
}