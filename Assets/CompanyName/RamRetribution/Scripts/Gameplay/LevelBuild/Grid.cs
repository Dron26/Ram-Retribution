using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factories;
using CompanyName.RamRetribution.Scripts.Factories.Interfaces;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Tiles;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class Grid
    {
        private readonly LevelCombinator _levelCombinator;
        private readonly List<Vector3> _enemySpots = new List<Vector3>();
        private readonly List<Vector3> _entryTiles = new List<Vector3>();

        public Grid(LevelCombinator levelCombinator)
        {
            _levelCombinator = levelCombinator;
        }
        
        public Gate LeftGate { get; private set; }
        public Gate RightGate { get; private set; }
        public IReadOnlyList<Vector3> EnemySpots => _enemySpots;
        public IReadOnlyList<Vector3> EntryTiles => _entryTiles;
        
        public void ParseTile(Tile tile)
        {
            switch (tile.Type)
            {
                case TileType.Entry:
                    _entryTiles.Add(tile.transform.position);
                    break;
                case TileType.WoodGate or TileType.RockGate:
                    ConfigureGate(tile);
                    break;
                case TileType.EnemiesSpawnPoint:
                    _enemySpots.Add(tile.transform.position);
                    break;
            }
        }
        
        private void ConfigureGate(Tile tile)
        {
            IGateInitializer gateInitializer = tile.Type switch
            {
                TileType.WoodGate => new WoodGateInitializer(_levelCombinator),
                TileType.RockGate => new RockGateInitializer(_levelCombinator),
                _ => throw new ArgumentException()
            };
            
            var instance = tile.GetComponentInChildren<Gate>();
            gateInitializer.Init(instance);
            
            if (RightGate == null)
                RightGate = instance;
            else
                LeftGate = instance;
        }
    }
}