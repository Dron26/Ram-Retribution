using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Gameplay.Level;
using Generator.Scripts.Level;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(menuName = "LevelData")]
    public class LevelDTO : ScriptableObject
    {
        [SerializeField] private List<Tile> _forestTiles;
        [SerializeField] private List<Tile> _sandTiles;
        [SerializeField] private List<Tile> _snowTiles;
        [SerializeField] private List<Gate> Gates;
        
        public IReadOnlyList<Tile> GetTiles(TileTypes type)
        {
            return type switch
            {
                TileTypes.Forest => _forestTiles,
                TileTypes.Sand => _sandTiles,
                TileTypes.Snow => _snowTiles,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public Gate GetGate(GateTypes type)
        {
            foreach (var gate in Gates)
                if (gate.Type == type)
                    return gate;

            throw new ArgumentException();
        }
    }
}