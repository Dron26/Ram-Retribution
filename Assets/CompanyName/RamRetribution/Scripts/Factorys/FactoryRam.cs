using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Ram;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class FactoryRam : IFactory<Unit>
    {  
        public Unit Create(Vector3 at)
        {
            //Same logic like FactoryEnemy.cs
            return null;
        }
    }
}