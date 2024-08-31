using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Factories.Interfaces;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Tiles;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factories.Tiles
{
    public class SandTileFactory : ITileFactory<SandTile>
    {
        private readonly SandTile _tilePrefab;
        
        public SandTileFactory(IResourceLoadService loadService)
        {
            _tilePrefab = loadService
                .Load<SandTile>($"{AssetPaths.GridData}{nameof(SandTile)}");
        }

        public SandTile Create(Transform parent)
        {
            var instance = Object.Instantiate(_tilePrefab, parent);
            return instance;
        }
    }
}