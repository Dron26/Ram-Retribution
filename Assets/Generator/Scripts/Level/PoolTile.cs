using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common.Services;
using Generator.Scripts.Factory;
using UnityEngine;

namespace Generator.Scripts.Level
{
     public class PoolTile : MonoBehaviour
    {
        public int CountUsedTiles => _usedTiles.Count;
        
        private Queue<Tile> _pool;
        private List<Tile> _usedTiles;
        private TileFactory _tileFactory;
        private ResourceLoaderService _loaderService;
        private Tile _prefab;
        private readonly string _prefabName = "Tile";

        public void Initialize(TileFactory tileFactory, ResourceLoaderService loaderService)
        {
            _tileFactory = tileFactory;
            _loaderService = loaderService;
            _pool = new Queue<Tile>();
            _usedTiles = new List<Tile>();
            _prefab = loaderService.Load<Tile>(_prefabName);
        }

        public void CreatePool(int gridSize, Transform parent)
        {
            if (_pool.Count+_usedTiles.Count != gridSize)
            {
                _pool.Clear();
                for (int i = 0; i < gridSize; i++)
                {
                    Tile tile = _tileFactory.Create(_prefab);
                    tile.transform.SetParent(parent, false); 
                    tile.gameObject.SetActive(false);
                    _pool.Enqueue(tile);
                }
            }
            else
            {
                ReturnAllUsedTiles();
            }
        }

        public Tile GetTile()
        {
            if (_pool.Count > 0)
            {
                var tile = _pool.Dequeue();
                _usedTiles.Add(tile);
                tile.gameObject.SetActive(true);
                return tile;
            }
            else
            {
                throw new Exception("No available tiles in pool");
            }
        }

        public List<Tile> GetAllTiles()
        {
            List<Tile> allTiles = new List<Tile>();
            allTiles.AddRange(_usedTiles);
            allTiles.AddRange(_pool);
            return allTiles;
        }

        public void ReturnTile(Tile tile)
        {
            if (_usedTiles.Contains(tile))
            {
                _usedTiles.Remove(tile);
                tile.gameObject.SetActive(false);
                _pool.Enqueue(tile);
            }
            else
            {
                throw new Exception("Tile is not in the used list");
            }
        }

        public void ReturnAllUsedTiles()
        {
            foreach (var tile in _usedTiles.ToArray())
            {
                ReturnTile(tile);
            }
        }
    }
}