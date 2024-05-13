using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IFactory
    {
        GameObject Create(Vector3 at);
    }
}