using UnityEngine;

namespace CompanyName.RamRetribution.Interfaces
{
    public interface IFactory
    {
        GameObject Create(Vector3 at);
    }
}