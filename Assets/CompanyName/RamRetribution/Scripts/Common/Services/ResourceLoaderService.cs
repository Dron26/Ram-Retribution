using System.Collections.Generic;
using System.Linq;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.Services
{
    public class ResourceLoaderService : IResourceLoadService
    {
        public T Load<T>(string path)
            where T : Object
        {
            var prefab = Resources.Load<T>(path);
            return prefab;
        }

        public List<T> LoadAll<T>(string path)
            where T : Object
        {
            var prefabs = Resources.LoadAll<T>(path).ToList();
            return prefabs;
        }
    }
}