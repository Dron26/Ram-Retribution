using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IUnitFactory<out T>
    where T : Unit
    {
        public T Create(ConfigId configId, Vector3 at);
    }
}