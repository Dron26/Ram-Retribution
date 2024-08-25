using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Factorys.Interfaces;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class Level
    {
        private readonly List<Vector3> _enemySpots = new List<Vector3>();
        private readonly List<Vector3> _entryTilesPositions = new List<Vector3>();

        private Gate _leftGate;
        private Gate _rightGate;
        
        public Level(int number) 
            => Number = number;

        public event Action GatesDestroyed;

        public int Number { get; }
        public bool IsGateAttackedFirst { get; private set; }

        public Vector3 EnemySpawnPoint => _enemySpots[Random.Range(0, _enemySpots.Count)];
        public IReadOnlyList<Vector3> EntryTilesPositions => _entryTilesPositions;
        
        //Rework
        public IReadOnlyList<Transform> GateAttackPoints => _leftGate.PointsForAttack;

        public void Init(Tile tile)
        {
            if(tile.Type == TileType.Entry)
                _entryTilesPositions.Add(tile.transform.position);

            if (tile.Type is TileType.WoodGate or TileType.RockGate)
                ConfigureGate(tile);
            
            if(tile.Type == TileType.EnemiesSpawnPoint)
                _enemySpots.Add(tile.transform.position);
        }

        public Gate GetGateToAttack()
        {
            if (_leftGate.IsActive && _rightGate.IsActive)
                return Random.Range(0, 1) == 0 ? _leftGate : _rightGate;

            if (_leftGate.IsActive)
                return _leftGate;

            if (_rightGate.IsActive)
                return _rightGate;

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
        
        private void ConfigureGate(Tile tile)
        {
            var instance = tile.GetComponentInChildren<Gate>();
            
            if (_rightGate == null)
            {
                _rightGate = instance;
                _rightGate.Damageable.HealthEnded += OnGateDestroyed;
                _rightGate.FirstAttacked += OnGatesAttackedFirst;
            }
            else
            {
                _leftGate = instance;
                _leftGate.Damageable.HealthEnded += OnGateDestroyed;
                _leftGate.FirstAttacked += OnGatesAttackedFirst;
            }
            
            IsGateAttackedFirst = false;
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
            
            if(!_leftGate.IsActive && !_rightGate.IsActive)
                GatesDestroyed?.Invoke();
        }
    }
}