using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Components
{
    public interface IPlacementStrategy
    {
        public Vector3 SetPosition(Vector3 origin, Unit unit);
    }
}