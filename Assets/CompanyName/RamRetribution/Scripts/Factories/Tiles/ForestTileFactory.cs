using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Factories.Interfaces;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Tiles;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factories.Tiles
{
    public class ForestTileFactory : ITileFactory<ForestTile>
    {
        private readonly ForestTile _tilePrefab;

        public ForestTileFactory(IResourceLoadService loadService)
        {
            _tilePrefab = loadService
                .Load<ForestTile>($"{AssetPaths.GridData}{nameof(ForestTile)}");
        }

        public ForestTile Create(Transform parent)
        {
            var instance = Object.Instantiate(_tilePrefab, parent);
            return instance;
        }
    }
}