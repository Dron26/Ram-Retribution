using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IFactory<T>
    {
        T Create(Vector3 at);
    }
}