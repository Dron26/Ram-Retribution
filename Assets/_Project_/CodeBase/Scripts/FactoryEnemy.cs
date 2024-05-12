using _Project_.CodeBase.Scripts.Common.AssetLoad;
using _Project_.CodeBase.Scripts.Interfaces;
using UnityEngine;

namespace _Project_.CodeBase.Scripts
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