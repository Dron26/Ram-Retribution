using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Factories.Interfaces;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Tiles;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factories.Tiles
{
    public class IceTileFactory : ITileFactory<IceTile>
    {
        private readonly IceTile _tilePrefab;
        
        public IceTileFactory(IResourceLoadService loadService)
        {
            _tilePrefab = loadService
                .Load<IceTile>($"{AssetPaths.GridData}{nameof(IceTile)}");
        }

        public IceTile Create(Transform parent)
        {
            var instance = Object.Instantiate(_tilePrefab, parent);
            return instance;
        }
    }
}