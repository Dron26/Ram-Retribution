using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;

namespace CompanyName.RamRetribution.Scripts.Factories.Units.Variant
{
    public class SandEnemiesFactory : EnemyFactory
    {
        public SandEnemiesFactory(ConfigsContainer configsContainer, IResourceLoadService loadService) 
            : base(configsContainer, loadService, AssetPaths.SandEnemies)
        {
        }
    }
}