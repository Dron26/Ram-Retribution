using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IFactory<T>
    {
        public T Create(Transform parent);
    }
}