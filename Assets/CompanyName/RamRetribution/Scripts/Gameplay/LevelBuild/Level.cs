using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Buildings;
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

        public Level(int number) 
            => Number = number;

        public event Action GatesDestroyed;

        public int Number { get; }
        public bool IsGateAttackedFirst { get; private set; }
        public Gate LeftGate { get; private set; }
        public Gate RightGate { get; private set; }
        public IReadOnlyList<Vector3> EnemySpots => _enemySpots;
        public IReadOnlyList<Vector3> EntryTilesPositions => _entryTilesPositions;
        public IReadOnlyList<Transform> GateAttackPoints => LeftGate.PointsForAttack;

        public void Init(Tile tile)
        {
            if(tile.Type == TileType.Entry)
                _entryTilesPositions.Add(tile.transform.position);

            if (tile.Type is TileType.WoodGate or TileType.RockGate)
                ConfigureGate(tile);
            
            if(tile.Type == TileType.EnemiesSpawnPoint)
                _enemySpots.Add(tile.transform.position);
        }

        public Gate GetGate()
        {
            if (LeftGate.IsActive && RightGate.IsActive)
                return Random.Range(0, 1) == 0 ? LeftGate : RightGate;

            if (LeftGate.IsActive)
                return LeftGate;

            if (RightGate.IsActive)
                return RightGate;

            throw new InvalidOperationException("Both gate already destroyed.");
        }
        
        private void ConfigureGate(Tile tile)
        {
            IGateFactory gateFactory;
            
            switch (tile.Type)
            {
                case TileType.WoodGate:
                    gateFactory = new WoodGateFactory();
                    break;
                case TileType.RockGate:
                    gateFactory = new RockGateFactory();
                    break;
                default:
                    throw new ArgumentException();
            }

            var instance = tile.GetComponentInChildren<Gate>();
            
            if (LeftGate == null)
            {
                LeftGate = gateFactory.Create(instance, isLeft: true);
                LeftGate.Damageable.HealthEnded += OnGateDestroyed;
                LeftGate.FirstAttacked += OnGatesAttackedFirst;
            }
            else
            {
                RightGate = gateFactory.Create(instance, isLeft: false);
                RightGate.Damageable.HealthEnded += OnGateDestroyed;
                RightGate.FirstAttacked += OnGatesAttackedFirst;
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
            
            if(!LeftGate.IsActive && !RightGate.IsActive)
                GatesDestroyed?.Invoke();
        }
    }
}