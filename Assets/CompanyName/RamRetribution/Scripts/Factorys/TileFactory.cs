using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class TileFactory : IFactory<Tile>
    {
        private readonly Tile _tilePrefab;

        public TileFactory()
        {
            _tilePrefab = Services
                .ResourceLoadService
                .Load<Tile>($"{AssetPaths.GridData}{nameof(Tile)}");
        }

        public Tile Create(Transform parent)
        {
            var instance = Object.Instantiate(_tilePrefab, parent);
            return instance;
        }
    }
}