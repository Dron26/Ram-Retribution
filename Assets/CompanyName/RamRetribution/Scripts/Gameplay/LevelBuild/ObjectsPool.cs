using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class ObjectsPool<T>
        where T : MonoBehaviour
    {
        private readonly Queue<T> _objects;
        private readonly HashSet<T> _inUseObjects;
        private IFactory<T> _factory;
        private Transform _parent;

        public ObjectsPool(IFactory<T> factory, Transform parent)
        {
            _objects = new Queue<T>();
            _inUseObjects = new HashSet<T>();
            _factory = factory;
            _parent = parent;
        }

        public void SetFactory(IFactory<T> factory)
        {
            _factory = factory;
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