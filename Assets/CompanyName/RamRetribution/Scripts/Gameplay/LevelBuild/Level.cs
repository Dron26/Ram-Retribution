using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class Level
    {
        private readonly List<Vector3> _enemySpots = new List<Vector3>();
        private readonly List<Vector3> _entryTilesPositions = new List<Vector3>();

        public Level(int number)
        {
            Number = number;
        }
        
        public event Action GateDestroyed;

        public int Number { get; }
        public bool IsGateAttackedFirst { get; private set; }
        public Gate CurrentGate { get; private set; }
        public IReadOnlyList<Vector3> EnemySpots => _enemySpots;
        public IReadOnlyList<Vector3> EntryTilesPositions => _entryTilesPositions;

        public void Init(Tile tile)
        {
            if(tile.Type == TileType.Entry)
                _entryTilesPositions.Add(tile.transform.position);

            if (CurrentGate == null && tile.Type is TileType.WoodGate or TileType.RockGate)
                ConfigureGate(tile);
            
            if(tile.Type == TileType.EnemiesSpawnPoint)
                _enemySpots.Add(tile.transform.position);
        }

        private void ConfigureGate(Tile tile)
        {
            switch (tile.Type)
            {
                case TileType.WoodGate:
                {
                    IDamageable woodGateHealth = new GateHealth(1000);
                    var component = tile.GetComponentInChildren<Gate>();
                    component.Init(woodGateHealth);
                    CurrentGate = component;
                    break;
                }
                case TileType.RockGate:
                {
                    IDamageable rockGateHealth = new GateHealth(2000);
                    var component = tile.GetComponentInChildren<Gate>();
                    component.Init(rockGateHealth);
                    CurrentGate = component;
                    break;
                }
                default:
                    throw new ArgumentException();
            }

            IsGateAttackedFirst = false;
            CurrentGate.Damageable.HealthEnded += OnGateDestroyed;
            CurrentGate.FirstAttacked += OnGateAttackedFirst;
        }

        private void OnGateAttackedFirst()
        {
            CurrentGate.FirstAttacked -= OnGateAttackedFirst;
            IsGateAttackedFirst = true;
        }

        private void OnGateDestroyed()
        {
            CurrentGate.Damageable.HealthEnded -= OnGateDestroyed;
            GateDestroyed?.Invoke();
        }

        public void DebugCheckCollections()
        {
            foreach (Vector3 entryTilesPosition in _entryTilesPositions)
            {
                Debug.Log($"entryTilesPosition {entryTilesPosition}");
            }
            
            foreach (Vector3 tilesPosition in _enemySpots)
            {
                Debug.Log($"enemySpots: {tilesPosition}");
            }
        }
    }
}