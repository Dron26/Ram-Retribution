using UnityEngine;

namespace _Project_.CodeBase.Scripts.Interfaces
{
    public interface IFactory
    {
        GameObject Create(Vector3 at);
    }
}