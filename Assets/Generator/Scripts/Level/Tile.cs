using System.Collections.Generic;
using System.Collections.ObjectModel;
using Generator.Scripts.Common.Enums;
using UnityEngine;

namespace Generator.Scripts.Level
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _groundTiles = new List<GameObject>();
        [SerializeField] private List<GameObject> _treeTiles = new List<GameObject>();
        [SerializeField] private List<GameObject> _bushTiles = new List<GameObject>();
        [SerializeField] private List<GameObject> _wallTiles = new List<GameObject>();
        [SerializeField] private List<GameObject> _roadTiles = new List<GameObject>();
        [SerializeField] private List<GameObject> _cornerRoadTiles = new List<GameObject>();
        [SerializeField] private List<GameObject> _pathTiles = new List<GameObject>();
        [SerializeField] private List<GameObject> _guardHouseTile = new List<GameObject>();
        [SerializeField] private List<GameObject> _guardProp = new List<GameObject>();
        [SerializeField] private List<GameObject> _rimTiles = new List<GameObject>();
        [SerializeField] private List<GameObject> _towerTile = new List<GameObject>();
        [SerializeField] private List<GameObject> _gateTile = new List<GameObject>();

        public ReadOnlyCollection<GameObject> GetGroundTiles() => _groundTiles.AsReadOnly();
        public ReadOnlyCollection<GameObject> GetTreeTiles() => _treeTiles.AsReadOnly();
        public ReadOnlyCollection<GameObject> GetBushTiles() => _bushTiles.AsReadOnly();
        public ReadOnlyCollection<GameObject> GetWallTiles() => _wallTiles.AsReadOnly();
        public ReadOnlyCollection<GameObject> GetRoadTiles() => _roadTiles.AsReadOnly();
        public ReadOnlyCollection<GameObject> GetCornerRoadTiles() => _cornerRoadTiles.AsReadOnly();
        public ReadOnlyCollection<GameObject> GetPathTiles() => _pathTiles.AsReadOnly();
        public ReadOnlyCollection<GameObject> GetGuardHouseTiles() => _guardHouseTile.AsReadOnly();
        public ReadOnlyCollection<GameObject> GetGuardProps() => _guardProp.AsReadOnly();
        public ReadOnlyCollection<GameObject> GetRimTiles() => _rimTiles.AsReadOnly();
        public ReadOnlyCollection<GameObject> GetTowerTile() => _towerTile.AsReadOnly();
        public ReadOnlyCollection<GameObject> GetGateTile() => _gateTile.AsReadOnly();

        private TileType _type;
        public TileType TileType => _type;

        private Dictionary<TileType, List<GameObject>> _typeSetters;

        private void Awake()
        {
            _typeSetters = new Dictionary<TileType, List<GameObject>>
            {
                { TileType.Ground, _groundTiles },
                { TileType.Tree, _treeTiles },
                { TileType.Bush, _bushTiles },
                { TileType.Wall, _wallTiles },
                { TileType.Road, _roadTiles },
                { TileType.CornerRoad, _cornerRoadTiles },
                { TileType.Path, _pathTiles },
                { TileType.GuardHouse, _guardHouseTile },
                { TileType.Guard, _guardProp },
                { TileType.Rim, _rimTiles },
                { TileType.Tower, _towerTile },
                { TileType.Gate, _gateTile }
            };
        }

        public void SetType(TileType type)
        {
            _type = type;

            if (_typeSetters.ContainsKey(type))
            {
                int i = Random.Range(0, _typeSetters[type].Count);
                _typeSetters[type][i].gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"No setter defined for TileType {type}");
            }
        }
    }
}