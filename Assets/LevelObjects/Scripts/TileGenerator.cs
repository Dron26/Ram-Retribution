using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelObjects.Scripts
{
    public class TileGenerator : MonoBehaviour
    {
        [Range(11, 55)] [SerializeField] private int _gridSize = 13;
        [SerializeField] private float _tileSize = 9;
        [Range(1, 10)] [SerializeField] private int _treeDensity = 1;
        [Range(1, 10)] [SerializeField] private int _frequencyCorner = 3;
        [Range(1, 10)] [SerializeField] private int _frequencyHill = 3;
        [Range(1, 10)] [SerializeField] private int _hightHill = 0;
        [SerializeField] private bool _isMountainRimCreate;
        [Range(1, 10)] [SerializeField] private float _heightIncrement;
        [SerializeField] private bool _isPlantForest;
        [SerializeField] private bool _isFillUpMountain;

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

        [SerializeField] private GameObject _ground;
        [SerializeField] private GameObject _tree;
        [SerializeField] private GameObject _road;
        [SerializeField] private GameObject _guardHouse;
        [SerializeField] private GameObject _castel;
        [SerializeField] private GameObject _prop;
        [SerializeField] private GameObject _rim;
        [SerializeField] private int _hideHight = 100;
        private List<GameObject> _objGroup;
        private GameObject _groundGroup;
        private GameObject _treeGroup;
        private GameObject _roadGroup;
        private GameObject _guardHouseGroup;
        private GameObject _castelGroup;
        private GameObject _propGroup;
        private int _gateX;
        private int _gateY;
        private int _countTowerLeft = 1;
        private int _countTowerRight = 1;
        private int _countFreeTileRight = 1;
        private int _countFreeTileLeft = 1;
        private HashSet<TileData> _occupiedPositions = new HashSet<TileData>();
        private int _leftWallLength;
        private int _rightWallLength;
        public ParticleSystem _particlePrefab;
        private ParticleSystem _particleSystem;
    
        HashSet<TileType> _buildingTypes = new HashSet<TileType>
        {
            TileType.CastelTiles,
            TileType.GuardHouseTile,
            TileType.TowerTile,
            TileType.GateTile
        };

        private void Initialize(LocationData data)
        {
        
            _gridSize = data.GridSize;
            _tileSize = data.TileSize;
            _treeDensity = data.TreeDensity;
            _frequencyCorner = data.FrequencyCorner;
            _frequencyHill = data.FrequencyHill;
            _hightHill = data.HeightHill;
            _isMountainRimCreate = data.IsMountainRimCreate;
            _heightIncrement = data.HeightIncrement;
            _isPlantForest = data.IsPlantForest;
            _isFillUpMountain = data.IsFillUpMountain;

            _groundTiles = data.GetGroundTiles().ToArray();
            _treeTiles = data.GetTreeTiles().ToArray();
            _bushTiles = data.GetBushTiles().ToArray();
            _wallTiles = data.GetWallTiles().ToArray();
            _roadTiles = data.GetRoadTiles().ToArray();
            _cornerRoadTiles = data.GetCornerRoadTiles().ToArray();
            _pathTiles = data.GetPathTiles().ToArray();
            _guardHouseTile = data.GetGuardHouseTiles().ToArray();
            _guardProp = data.GetGuardProps().ToArray();
            _rimTiles = data.GetRimTiles().ToArray();
            _towerTile = data.GetTowerTile();
            _gateTile = data.GetGateTile();
            _countHouse = data.GetHouseCount();
        }
        public void GenerateGrid()
        {
            Time.timeScale = 5;
            ClearOldPosition();
            CraeteParentObject();
            PlaceGateAndWalls();
            PlaceRoad();
            PlaceGuardHouse(_countHouse);
            FillWithGround();

            if (_isFillUpMountain)
            {
                FillUpMountain();
            }

            if (_isPlantForest)
            {
                PlantForest();
            }

            if (_isMountainRimCreate)
            {
                CreateMountainRim();
            }
            Time.timeScale = 1;
        }

        private void CraeteParentObject()
        {
            _groundGroup = Instantiate(_ground, transform);
            _treeGroup = Instantiate(_tree, transform);
            _roadGroup = Instantiate(_road, transform);
            _guardHouseGroup = Instantiate(_guardHouse, transform);
            _castelGroup = Instantiate(_castel, transform);
            _propGroup = Instantiate(_prop, transform);
            }

        private void ClearOldPosition()
        {
            _occupiedPositions.Clear();
        }

        private void PlaceGateAndWalls()
        {
            _gateX = Random.Range(5, _gridSize - 5);
            _gateY = Random.Range(7, _gridSize - 3);

            AddTile(_gateTile, TileType.GateTile, _gateX, _gateY, _castelGroup);
            _leftWallLength = Random.Range(1, _gateX - 2);
            _rightWallLength = Random.Range(1, _gridSize - (_gateX + 3));

            for (int i = 1; i <= _leftWallLength; i++)
            {
                AddTile(_wallTiles[Random.Range(0, _wallTiles.Length)], TileType.CastelTiles, _gateX - i, _gateY, _castelGroup);
            }

            AddTile(_towerTile, TileType.TowerTile, _gateX - _leftWallLength - 1, _gateY, _castelGroup);

            for (int y = _gateY + 1; y < _gridSize; y++)
            {
                AddTile(_wallTiles[Random.Range(0, _wallTiles.Length)], TileType.CastelTiles, _gateX - _leftWallLength - 1,
                    y,
                    _castelGroup, 0, new Vector3(0, 90, 0));
            }

            for (int i = 1; i <= _rightWallLength + 1; i++)
            {
                AddTile(_wallTiles[Random.Range(0, _wallTiles.Length)], TileType.CastelTiles, _gateX + i, _gateY,
                    _castelGroup);
            }

            AddTile(_towerTile, TileType.TowerTile, _gateX + _rightWallLength + 2, _gateY, _castelGroup);

            for (int y = _gateY + 1; y < _gridSize; y++)
            {
                AddTile(_wallTiles[Random.Range(0, _wallTiles.Length)], TileType.CastelTiles, _gateX + _rightWallLength + 2,
                    y,
                    _castelGroup, 0, new Vector3(0, 90, 0));
            }
        }

        private void PlaceRoad()
        {
            int roadX = _gateX;

            for (int y = _gateY - 1; y >= 0; y--)
            {
                GameObject roadTile = _roadTiles[Random.Range(0, _roadTiles.Length)];
                GameObject cornerTile = _cornerRoadTiles[Random.Range(0, _roadTiles.Length)];
                GameObject pathTile;

                if (Random.Range(0, _frequencyCorner) == 0)
                {
                    AddTile(roadTile, TileType.RoadTiles, roadX, y, _roadGroup);

                    pathTile = _pathTiles[Random.Range(0, _pathTiles.Length)];
                    AddTile(pathTile, TileType.PathTiles, roadX + 1, y, _roadGroup);
                    pathTile = _pathTiles[Random.Range(0, _pathTiles.Length)];
                    AddTile(pathTile, TileType.PathTiles, roadX - 1, y, _roadGroup);

                    int x = Random.Range(0, 2) == 0 ? -1 : 1;
                    // roadX += x;


                    y--;
                    if (y >= 0)
                    {
                        if (x > 0)
                        {
                            AddTile(cornerTile, TileType.CornerRoadTiles, roadX, y, _roadGroup, 0, new Vector3(0, 90, 0));
                            AddTile(cornerTile, TileType.CornerRoadTiles, roadX + x, y, _roadGroup, 0,
                                new Vector3(0, -90, 0));

                            pathTile = _pathTiles[Random.Range(0, _pathTiles.Length)];
                            AddTile(pathTile, TileType.PathTiles, roadX + x + x, y, _roadGroup);
                            pathTile = _pathTiles[Random.Range(0, _pathTiles.Length)];
                            AddTile(pathTile, TileType.PathTiles, roadX - x, y, _roadGroup);
                        }
                        else
                        {
                            AddTile(cornerTile, TileType.CornerRoadTiles, roadX, y, _roadGroup, 0, new Vector3(0, 0, 0));
                            AddTile(cornerTile, TileType.CornerRoadTiles, roadX + x, y, _roadGroup, 0,
                                new Vector3(0, 180, 0));

                            pathTile = _pathTiles[Random.Range(0, _pathTiles.Length)];
                            AddTile(pathTile, TileType.PathTiles, roadX - x, y, _roadGroup);
                            pathTile = _pathTiles[Random.Range(0, _pathTiles.Length)];
                            AddTile(pathTile, TileType.PathTiles, roadX + x + x, y, _roadGroup);
                        }
                    }
                    else
                    {
                        break;
                    }


                    y--;


                    if (y >= 0)
                    {
                        roadX += x;
                        roadX = Mathf.Clamp(roadX, 0, _gridSize - 1);

                        AddTile(roadTile, TileType.RoadTiles, roadX, y, _roadGroup);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    AddTile(roadTile, TileType.RoadTiles, roadX, y, _roadGroup);
                }


                pathTile = _pathTiles[Random.Range(0, _pathTiles.Length)];
                AddTile(pathTile, TileType.PathTiles, roadX + 1, y, _roadGroup);
                pathTile = _pathTiles[Random.Range(0, _pathTiles.Length)];
                AddTile(pathTile, TileType.PathTiles, roadX - 1, y, _roadGroup);
            }
        }

        private void PlaceGuardHouse(int countHouses)
        {
            int[] xOffsetsLeft = { 3, _gateX - 1 };
            int[] xOffsetsRight = { _gateX + 1, _gridSize - 1 };
            int[] yOffsets = { _gateY - 2, _gateY - 6 };
            bool createInLeft = false;
            bool createInRight = false;
            int count = countHouses;
            int countAttempts = 0;
            int maxCountAttempts = 30;

            List<TileType> tiles = new List<TileType>();
            tiles.Add(TileType.RoadTiles);
            tiles.Add(TileType.GroundTiles);
            tiles.Add(TileType.GuardHouseTile);

            while (count > 0 && countAttempts < maxCountAttempts)
            {
                if (!createInLeft)
                {
                    if (TrySetLeftHouse(xOffsetsLeft, yOffsets, tiles))
                    {
                        count--;
                    }
                    else
                    {
                        createInLeft = true;
                    }
                }


                if (!createInRight & count > 0)
                {
                    if (TrySetRightHouse(xOffsetsRight, yOffsets, tiles))
                    {
                        count--;
                    }
                    else
                    {
                        createInRight = true;
                    }
                }

                countAttempts++;
            }
        }

        private void FillWithGround()
        {
            var occupiedPositions = new Dictionary<(float, float), TileType>();
            foreach (var tile in _occupiedPositions)
            {
                occupiedPositions[(tile.X, tile.Y)] = tile.TileType;
            }

            for (int y = 0; y < _gridSize; y++)
            {
                for (int x = 0; x < _gridSize; x++)
                {
                    if (!IsOccupied(occupiedPositions, x, y))
                    {
                        GameObject groundTile = _groundTiles[Random.Range(0, _groundTiles.Length)];
                        AddTile(groundTile, TileType.GroundTiles, x, y, _groundGroup);
                    }
                }
            }
        }

        private void FillUpMountain()
        {
            List<TileType> tiles = new List<TileType>
            {
                TileType.RoadTiles,
                TileType.PathTiles,
                TileType.CornerRoadTiles,
                TileType.GuardHouseTile,
                TileType.CastelTiles
            };

            foreach (TileData tileData in _occupiedPositions)
            {
                if (IsPositionFree(tileData.X, tileData.Y, tiles) && !tileData.isUp)
                {
                    if (Random.Range(0, 5) == 0)
                    {
                        float centralHeight = Random.Range(0, _hightHill);
                        tileData.Tile.transform.localPosition =
                            new Vector3(tileData.X * _tileSize, centralHeight, tileData.Y * _tileSize);

                        tileData.SetUp(true);

                        var aroundUpTile = new[]
                        {
                            new Vector2(1, 0), new Vector2(-1, 0),
                            new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 1),
                            new Vector2(-1, -1), new Vector2(1, -1), new Vector2(-1, 1)
                        };

                        foreach (var offset in aroundUpTile)
                        {
                            Vector2 pos = new Vector2(tileData.X + offset.x, tileData.Y + offset.y);
                            if (pos.x >= 0 && pos.x < _gridSize && pos.y >= 0 && pos.y < _gridSize)
                            {
                                TileData surroundingTile =
                                    _occupiedPositions.FirstOrDefault(t => t.X == pos.x && t.Y == pos.y);
                                if (surroundingTile != null && surroundingTile.Tile != null && !surroundingTile.isUp)
                                {
                                    float surroundingHeight =
                                        centralHeight * 0.5f;
                                    surroundingTile.Tile.transform.position = new Vector3(pos.x * _tileSize,
                                        surroundingHeight, pos.y * _tileSize);
                                    surroundingTile.SetUp(true);
                                }
                            }
                        }
                    }
                    else
                    {
                        float centralHeight = Random.Range(0, _hightHill);
                        tileData.Tile.transform.localPosition =
                            new Vector3(tileData.X * _tileSize, -centralHeight, tileData.Y * _tileSize);

                        tileData.SetUp(true);
                    }
                }
            }
        }

        private void PlantForest()
        {
            List<TileData> groundTiles = GetGroundTiles();

            foreach (TileData position in groundTiles)
            {
                if (IsPositionClearForForest(position))
                {
                    if (Random.Range(0, _treeDensity) == 0)
                    {
                        TileData groundTile =
                            _occupiedPositions.FirstOrDefault(t => t.X == position.X && t.Y == position.Y);
                        if (groundTile != null)
                        {
                            AddTile(_treeTiles[Random.Range(0, _treeTiles.Length)], TileType.TreeTiles, position.X,
                                position.Y, _treeGroup, groundTile.Tile.transform.position.y-_hideHight,
                                new Vector3(0, Random.Range(0, 360), 0));
                        }
                    }
                }
            }
        }

        private void CreateMountainRim()
        {
            int rimRows = 10;
            float heightIncrement = 4f;
            int countRows ;
            int tempSize;

            countRows =0;
            tempSize = _gridSize + 1;
        
            _particleSystem = Instantiate(_particlePrefab, transform);
        
            for (int x = -1; x > -rimRows; x--)
            {
                countRows++;
                for (int y = -countRows; y < tempSize; y++)
                {
                    float currentHeight = heightIncrement * countRows;
                    CreateParticle(new Vector3(x, currentHeight, y));
                    // AddTile(_rimTiles[Random.Range(0, _rimTiles.Length)], TileType.RimTiles, x, y, _rimGroup, currentHeight);
                }

                tempSize += 1;
            }
        
            countRows = 0;
            tempSize = _gridSize + 1;
            for (int y = _gridSize; y < _gridSize + rimRows; y++)
            {
                countRows++;
                for (int x = -countRows; x < tempSize; x++)
                {
                    float currentHeight = heightIncrement * countRows;
                    CreateParticle(new Vector3(x, currentHeight, y));
                    //  AddTile(_rimTiles[Random.Range(0, _rimTiles.Length)], TileType.RimTiles, x, y, _rimGroup,currentHeight);
                }
        
                tempSize += 1;
            }

            countRows = 0;
            tempSize = _gridSize + 1;
            for (int x = _gridSize; x < _gridSize + rimRows; x++)
            {
                countRows++;
                for (int y = -countRows; y < tempSize; y++)
                {
                    float currentHeight = heightIncrement * countRows;
                    CreateParticle(new Vector3(x, currentHeight, y));
                    // AddTile(_rimTiles[Random.Range(0, _rimTiles.Length)], TileType.RimTiles, x, y, _rimGroup, currentHeight);
                }
                tempSize += 1;
            }
        }

        private List<TileData> GetGroundTiles()
        {
            return _occupiedPositions.Where(tile => tile.TileType == TileType.GroundTiles).ToList();
        }

        private bool IsPositionClearForForest(TileData data)
        {
            return !_occupiedPositions.Any(tile =>
                tile.X == data.X && tile.Y == data.Y && _buildingTypes.Contains(tile.TileType));
        }
    
        private bool IsOccupied(Dictionary<(float, float), TileType> occupiedPositions, int x, int y)
        {
            if (occupiedPositions.TryGetValue((x, y), out TileType tileType))
            {
                return tileType == TileType.PathTiles || tileType == TileType.RoadTiles ||
                       tileType == TileType.CornerRoadTiles;
            }

            return false;
        }

        private bool IsPositionFree(float centerX, float centerY, List<TileType> verifiableTypes)
        {
            Dictionary<(float, float), TileType> occupiedPositions = new Dictionary<(float, float), TileType>();
            foreach (var tile in _occupiedPositions)
            {
                occupiedPositions[(tile.X, tile.Y)] = tile.TileType;
            }

            HashSet<TileType> verifiableTypesSet = new HashSet<TileType>(verifiableTypes);

            Vector2[] offsets = new[]
            {
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(-1, 0),
                new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 1),
                new Vector2(-1, -1), new Vector2(1, -1), new Vector2(-1, 1)
            };

            foreach (Vector2 offset in offsets)
            {
                Vector2 pos = new Vector2(centerX + offset.x, centerY + offset.y);

                if (pos.x < 0 || pos.x >= _gridSize || pos.y < 0 || pos.y >= _gridSize)
                {
                    return false;
                }

                if (occupiedPositions.TryGetValue((pos.x, pos.y), out var tileType) &&
                    verifiableTypesSet.Contains(tileType))
                {
                    return false;
                }
            }

            return true;
        }
    
        private void AddPropGuardHouse(float x, float y)
        {
            int z = Random.Range(0, 2) == 0 ? -1 : 1;
            int a = Random.Range(0, 2) == 0 ? -1 : 1;
            AddObject(_guardProp[Random.Range(0, _guardProp.Length)], x + z, y + a);
        }

        private bool TrySetLeftHouse(int[] xOffsets, int[] yOffsets, List<TileType> tileTypes)
        {
            bool leftHousePlaced = false;

            for (int x = xOffsets[1]; x >= xOffsets[0]; x--)
            {
                for (int y = yOffsets[0]; y >= yOffsets[1]; y--)
                {
                    if (IsPositionFree(x, y, tileTypes))
                    {
                        AddTile(_guardHouseTile[Random.Range(0, _guardHouseTile.Length)], TileType.GuardHouseTile, x, y,
                            _guardHouseGroup);
                        AddPropGuardHouse(x, y);
                        leftHousePlaced = true;
                        return leftHousePlaced;
                    }
                }
            }

            return leftHousePlaced;
        }

        private bool TrySetRightHouse(int[] xOffsets, int[] yOffsets, List<TileType> tileTypes)
        {
            bool rightHousePlaced = false;

            for (int x = xOffsets[0]; x <= xOffsets[1]; x++)
            {
                for (int y = yOffsets[1]; y <= yOffsets[0]; y++)
                {
                    if (!rightHousePlaced && IsPositionFree(x, y, tileTypes))
                    {
                        AddTile(_guardHouseTile[Random.Range(0, _guardHouseTile.Length)], TileType.GuardHouseTile, x, y,
                            _groundGroup);
                        AddPropGuardHouse(x, y);
                        rightHousePlaced = true;
                        return rightHousePlaced;
                    }
                }
            }

            return rightHousePlaced;
        }

        private void AddTile(GameObject tile, TileType type, float x, float y, GameObject parent, float height = 0, Vector3 rotation = new Vector3())
        {
            if (!_occupiedPositions.Contains(new TileData(x, y, type)))
            {
                GameObject instance = Instantiate(tile, parent.transform);
                instance.transform.position = new Vector3(x * _tileSize, _hideHight, y * _tileSize);
                instance.transform.rotation = Quaternion.Euler(rotation);

                TileFall tileFall = instance.AddComponent<TileFall>();
                tileFall.targetPosition = new Vector3(x * _tileSize, height, y * _tileSize);
                tileFall.delay = _occupiedPositions.Count * 0.01f; 

                _occupiedPositions.Add(new TileData(x, y, type, instance));
            }
        }

        private void AddObject(GameObject obj, float x, float y, Vector3 rotation = new Vector3(), float height = 0)
        {
            GameObject instance = Instantiate(obj, _propGroup.transform);
            instance.transform.position = new Vector3(x * _tileSize, _hideHight, y * _tileSize); 
            instance.transform.rotation = Quaternion.Euler(rotation);

            TileFall tileFall = instance.AddComponent<TileFall>();
            tileFall.targetPosition = new Vector3(x * _tileSize, height, y * _tileSize);
            tileFall.delay = _occupiedPositions.Count * 0.01f; 
        }   private void CreateParticle(Vector3 position)
        {
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            _particleSystem.gameObject.transform.position =new Vector3(position.x * _tileSize, position.y, position.z * _tileSize); // Преобразуем локальные координаты в глобальные
            _particleSystem.Emit(emitParams, 1);
        }
    }
}