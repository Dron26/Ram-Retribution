using CompanyName.RamRetribution.Scripts.Common.AssetLoad;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts
{
    public class FactoryEnemy : IFactory
    {
        private readonly IAssets _assets;

        public FactoryEnemy(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject Create(Vector3 at)
        {
            GameObject spawnerPrefab = _assets.Instantiate(AssetPath.SpawnPoint, at);
            return spawnerPrefab;
        }
    }
}