using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.AssetLoad
{
    public class AssetProvider : IAssets
    {
        public T Get<T>(string path) 
            where T : Object
        {
            var prefab = Resources.Load<T>(path);
            return prefab;
        }
    }
}