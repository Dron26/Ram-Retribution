using System;
using System.Collections.Generic;
using System.Threading;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using Generator.Scripts.Common.Enum;
using Generator.Scripts.Common.Enums;
using Generator.Scripts.Factory;
using UnityEngine;

namespace Generator.Scripts.Level
{
    [RequireComponent(typeof(PoolTile))]
    [RequireComponent(typeof(TileFactory))]
    public class LevelConfigurator : MonoBehaviour
    {
        public async UniTask GenerateGridAsync(CancellationTokenSource tokenSource = null) 
            => await PlaceTilesAsync().WithCancellation(tokenSource.Token);

        public Vector3 RamsStartPosition => _ramsStartPosition;
        public List<Transform> EnemySpawnPoint => _enemySpawnPoint;
        public Transform GatePoint => _gatePoint;
        public Gate Gate => _gate;
        public int GridId => _gridId;

        [SerializeField] private int fallSpeed = 8;

        private PoolTile _poolTile;
        private TileFactory _tileFactory;
        private List<Transform> _enemySpawnPoint = new List<Transform>();
        private Vector3 _ramsStartPosition;
        private Transform _gatePoint;
        private WaitForSeconds _fallDelay;
        private HashSet<TileType> _tileTypes = new HashSet<TileType>();
        private TileType[,] _tiles;
        private GridType _gridType;
        private Gate _gate;
        private int _gridId;
        private int _gridSizeX = Constant.SizeX;
        private int _gridSizeY = Constant.SizeY;
        private int _hightUp = 30;
        private int _gateX;
        private int _gateY;
        private int _countHouse = 2;
        private bool _isRamsStartPointSet;
        private bool _isEnemySpawnPointSet;
        private bool _isGatePointSet;
        private bool _isHide = false;
        private int _offset = 0;

        private void Awake()
        {
            _poolTile = GetComponent<PoolTile>();
            _tileFactory = GetComponent<TileFactory>();
            _poolTile.Initialize(_tileFactory);
            SetType();
        }

        public void SetData(GridType gridType, int gridId, bool isHideTiles)
        {
            _gridType = gridType;
            _gridId = gridId;
            _isHide = isHideTiles;

            LoadGridData();
            AdjustPosition();
            
            _poolTile.CreatePool(_tiles.Length, transform);
            _enemySpawnPoint.Clear();
        }

        private void LoadGridData()
        {
            var path = $"{AssetPaths.GridData}" + _gridType + "/" + _gridId;
            var data = Services.ResourceLoadService.Load<GridData>(path);
            
            _tiles = ConvertGridTo2D(data.Tiles);
        }

        private void AdjustPosition()
        {
            _offset = _gridId * _gridSizeX;
            var position = transform.position;
            position = Vector3.one.With(x: position.x, y: _hightUp, z: position.z + _offset);
        }

        private TileType[,] ConvertGridTo2D(TileType[] tiles)
        {
            TileType[,] tiles2D = new TileType[_gridSizeY, _gridSizeX + 1];

            for (int y = 0; y < _gridSizeY; y++)
            {
                for (int x = 0; x < _gridSizeX; x++)
                {
                    tiles2D[y, x + 1] = tiles[y * _gridSizeX + x];
                }
            }

            return tiles2D;
        }

        private void SetType()
        {
            _tileTypes.Add(TileType.Road);
            _tileTypes.Add(TileType.Gate);
            _tileTypes.Add(TileType.GuardHouse);
        }

        private async UniTask PlaceTilesAsync()
        {
            int width = _tiles.GetLength(0);
            int height = _tiles.GetLength(1);
            List<UniTask> tasks = new List<UniTask>();

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                tasks.Add(SetTileAsync(_tiles[x, y], x, y + _offset));

            await UniTask.WhenAll(tasks);
        }

        private async UniTask SetTileAsync(TileType type, float x, float y, float height = 0,
            Vector3 rotation = new Vector3())
        {
            Tile tempTile = _poolTile.GetTile();
            Transform tempTransform = tempTile.transform;
            tempTransform.localPosition = new Vector3(x, 0, y);
            tempTile.SetType(type);
            tempTransform.rotation = Quaternion.Euler(rotation);
            Vector3 position = new Vector3(x * 2, height, y * 2);

            if (_tileTypes.Contains(type))
            {
                SetPoints(tempTile);
            }

            await FallWithDelayAsync(tempTransform, _poolTile.CountUsedTiles * 0.05f, position);

            if (_isHide)
            {
                _poolTile.ReturnTile(tempTile);
            }
        }

        private void SetPoints(Tile tile)
        {
            if (tile.TileType == TileType.Road && !_isRamsStartPointSet)
            {
                var localPosition = tile.transform.localPosition;
                _ramsStartPosition = new Vector3(localPosition.x, localPosition.y - localPosition.y, localPosition.z);
                _isRamsStartPointSet = true;
            }

            if (tile.TileType == TileType.GuardHouse)
            {
                _enemySpawnPoint.Add(tile.transform);
            }

            if (tile.TileType == TileType.Gate && _isGatePointSet)
            {
                tile.transform.rotation = new Quaternion(0, 180, 0, 0);
            }

            if (tile.TileType == TileType.Gate && !_isGatePointSet)
            {
                _gatePoint = tile.transform;
                _gate = tile.GetComponentInChildren<Gate>(includeInactive: false);
                _isGatePointSet = true;
            }
        }

        private async UniTask FallWithDelayAsync(Transform tileTransform, float delay, Vector3 targetPosition)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));

            while (tileTransform.position != targetPosition)
            {
                tileTransform.position =
                    Vector3.MoveTowards(tileTransform.position, targetPosition, fallSpeed * Time.deltaTime);
                await UniTask.Yield();
            }
        }

        public void ShowTiles()
        {
            List<Tile> allTiles = _poolTile.GetAllTiles();

            foreach (var tile in allTiles)
                tile.gameObject.SetActive(true);
        }
    }
}