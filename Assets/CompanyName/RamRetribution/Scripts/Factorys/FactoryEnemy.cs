using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class FactoryEnemy : IFactory<Enemy>
    {
        private readonly IAssets _assets;

        public FactoryEnemy(IAssets assets)
        {
            _assets = assets;
        }
        
        public Enemy Create(Vector3 at)
        {
            //Enemy instance = Instantiate(_assets.GetEnemy());
            //instance.Init(Data data);
            //return instance;
            return null;
        }
    }
}