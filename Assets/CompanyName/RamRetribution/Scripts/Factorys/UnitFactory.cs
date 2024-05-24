using System;
using CompanyName.RamRetribution.Scripts.Common.AssetLoad;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Ram;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class UnitFactory : IFactory<Unit>
    {
        public Unit Create(UnitTypes type, Vector3 at) 
        {
            var prefab = GetPrefab(type);
            var instance = Object.Instantiate(prefab, at, Quaternion.identity);
            
            return instance;
        }

        private Unit GetPrefab(UnitTypes type)
        {
            return type switch
            {
                UnitTypes.Attacker => LoadAsset<Attacker>(),
                UnitTypes.Leader => null,
                UnitTypes.Support => null,
                UnitTypes.Scout => null,
                UnitTypes.Tank => null,
                UnitTypes.Demolisher => null,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        private T LoadAsset<T>()
            where T : Object
            => Services.ResourceLoadService.Load<T>(
                $"{AssetPath.RamDataPath}{typeof(T).Name}");
    }
}