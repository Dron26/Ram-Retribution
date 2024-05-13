using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Ram;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.AssetLoad
{
    public class AssetProvider : IAssets
    {
        public T GetRam<T>(string path) 
            where T : Unit
        {
            T prefab = Resources.Load<T>(path);
            return prefab;
        }

        public T GetEnemy<T>(string path) 
            where T : Enemy
        {
            T prefab = Resources.Load<T>(path);
            return prefab;
        }
    }
}