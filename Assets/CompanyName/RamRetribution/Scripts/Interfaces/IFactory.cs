using CompanyName.RamRetribution.Scripts.Common.Enums;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IFactory<T>
    {
        public T Create(UnitTypes type, Vector3 at);
    }
}