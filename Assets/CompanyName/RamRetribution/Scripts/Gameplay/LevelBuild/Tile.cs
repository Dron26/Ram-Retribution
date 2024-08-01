using System;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Common;
using Generator.Scripts.Common.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TilesDictionary _tilesDictionary;
        
        public TileType Type { get; private set; }
        
        private void Awake()
        {
            foreach (var tileCollections in _tilesDictionary.Values)
                foreach (var tile in tileCollections)
                    tile.gameObject.SetActive(false);
        }

        public void SetType(TileType type, int side = GridConstants.LeftSide)
        {
            if (!_tilesDictionary.TryGetValue(type, out var value))
                throw new InvalidOperationException($"No setter defined for TileType {type}");
            
            var index = Random.Range(0, value.Count);

            if (side == GridConstants.RightSide)
            {
                switch (type)
                {
                    case TileType.WoodGate or TileType.RockGate:
                        _tilesDictionary[type][index].transform.rotation *= Quaternion.Euler(0, 180, 0);
                        break;
                    case TileType.RoadTurn:
                        _tilesDictionary[type][index].transform.rotation *= Quaternion.Euler(0,-90,0);
                        break;
                    case TileType.RoadTCross:
                        _tilesDictionary[type][index].transform.rotation *= Quaternion.Euler(0, 180, 0);
                        break;
                }
            }

            Type = type;
            _tilesDictionary[type][index].gameObject.SetActive(true);
        }
    }
}