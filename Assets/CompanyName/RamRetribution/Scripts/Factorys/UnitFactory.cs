using CompanyName.RamRetribution.Scripts.Common.AssetLoad;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Ram;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class UnitFactory : IFactory<Unit>
    {
        private readonly IAssets _assets;

        public UnitFactory(IAssets assets)
        {
            _assets = assets;
        }

        public Unit Create(Vector3 at)
        {
          return GameObject.Instantiate(_assets.Get<Unit>($"{AssetPath.RamDataPath}{typeof(Unit).Name}"));
        }
    }
}