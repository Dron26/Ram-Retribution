using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;

namespace CompanyName.RamRetribution.Scripts.Factories.Units.Variant
{
    public class IceEnemiesFactory : EnemyFactory
    {
        public IceEnemiesFactory(ConfigsContainer configsContainer, IResourceLoadService loadService)
            : base(configsContainer, loadService, AssetPaths.IceEnemies)
        {
        }
    }
}