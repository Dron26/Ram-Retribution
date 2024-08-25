using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class RamsFactory : IUnitFactory<Ram>
    {
        public Ram Create(ConfigId configId, Vector3 at)
        {
            return null;
        }
    }
}