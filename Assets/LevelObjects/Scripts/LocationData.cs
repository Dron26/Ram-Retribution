using System.Collections.ObjectModel;
using UnityEngine;

namespace LevelObjects.Scripts
{
    [CreateAssetMenu(fileName = "Location Data", menuName = "ScriptableObjects/LocationData", order = 51)]
    public class LocationData : ScriptableObject
    {
    
        [Header("Location Info")]
        [SerializeField] private int _index;
    
        [Header("Grid Settings")]
        [Range(11, 55)] [SerializeField] private int _gridSize;
        [SerializeField] private float _tileSize;

        [Header("Environment Settings")]
        [Range(1, 10)] [SerializeField] private int _treeDensity;
        [Range(1, 10)] [SerializeField] private int _frequencyCorner;
        [Range(1, 10)] [SerializeField] private int _frequencyHill;
        [Range(1, 10)] [SerializeField] private int _heightHill;
        [SerializeField] private bool _isMountainRimCreate;
        [Range(1, 10)] [SerializeField] private float _heightIncrement;
        [SerializeField] private bool _isPlantForest;
        [SerializeField] private bool _isFillUpMountain;

        [Header("Game Objects")]
        [SerializeField] private GameObject[] _groundTiles;
        [SerializeField] private GameObject[] _treeTiles;
        [SerializeField] private GameObject[] _bushTiles;
        [SerializeField] private GameObject[] _wallTiles;
        [SerializeField] private GameObject[] _roadTiles;
        [SerializeField] private GameObject[] _cornerRoadTiles;
        [SerializeField] private GameObject[] _pathTiles;
        [SerializeField] private GameObject[] _guardHouseTile;
        [SerializeField] private GameObject[] _guardProp;
        [SerializeField] private GameObject[] _rimTiles;
        [SerializeField] private GameObject _towerTile;
        [SerializeField] private GameObject _gateTile;
        [SerializeField] private int _countHouse;

        public int Index => _index;
        public int GridSize => _gridSize;
        public float TileSize => _tileSize;
        public int TreeDensity => _treeDensity;
        public int FrequencyCorner => _frequencyCorner;
        public int FrequencyHill => _frequencyHill;
        public int HeightHill => _heightHill;
        public bool IsMountainRimCreate => _isMountainRimCreate;
        public float HeightIncrement => _heightIncrement;
        public bool IsPlantForest => _isPlantForest;
        public bool IsFillUpMountain => _isFillUpMountain;

        public ReadOnlyCollection<GameObject> GetGroundTiles() => new(_groundTiles);
        public ReadOnlyCollection<GameObject> GetTreeTiles() => new(_treeTiles);
        public ReadOnlyCollection<GameObject> GetBushTiles() => new(_bushTiles);
        public ReadOnlyCollection<GameObject> GetWallTiles() => new(_wallTiles);
        public ReadOnlyCollection<GameObject> GetRoadTiles() => new(_roadTiles);
        public ReadOnlyCollection<GameObject> GetCornerRoadTiles() => new(_cornerRoadTiles);
        public ReadOnlyCollection<GameObject> GetPathTiles() => new(_pathTiles);
        public ReadOnlyCollection<GameObject> GetGuardHouseTiles() => new(_guardHouseTile);
        public ReadOnlyCollection<GameObject> GetGuardProps() => new(_guardProp);
        public ReadOnlyCollection<GameObject> GetRimTiles() => new(_rimTiles);
        public GameObject GetTowerTile() => _towerTile;
        public GameObject GetGateTile() => _gateTile;
        public int GetHouseCount() => _countHouse;
    }
}