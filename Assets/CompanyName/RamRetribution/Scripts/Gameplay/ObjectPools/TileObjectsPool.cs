using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Factories.Interfaces;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Tiles;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class TileObjectsPool<T>
        where T : Tile
    {
        private readonly Queue<T> _objects;
        private readonly HashSet<T> _inUseObjects;
        private readonly Transform _parent;
        private readonly ITileFactory<T> _factory;

        public TileObjectsPool(ITileFactory<T> factory, Transform parent)
        {
            _factory = factory;
            _objects = new Queue<T>();
            _inUseObjects = new HashSet<T>();
            _parent = parent;
        }

        public void Create(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T newObject = _factory.Create(_parent);
                _objects.Enqueue(newObject);
            }
        }

        public T Get()
        {
            if (_objects.Count == 0)
            {
                T newObject = _factory.Create(_parent);
                _objects.Enqueue(newObject);
            }

            T instance = _objects.Dequeue();
            _inUseObjects.Add(instance);
            return instance;
        }

        public void Return(T unit)
        {
            
        }
    }
}