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
    }
}