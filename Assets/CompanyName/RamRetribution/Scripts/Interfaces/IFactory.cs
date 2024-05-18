using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IFactory<T>
        where T : Object
    {
    public T Create(Vector3 at);
    }
}