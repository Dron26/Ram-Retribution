using System;
using CompanyName.RamRetribution.Scripts.Common;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Components
{
    public class CirclePlacementStrategy : IPlacementStrategy
    {
        private readonly float _minRadius;
        private readonly float _maxRadius;

        public CirclePlacementStrategy(float minRadius, float maxRadius)
        {
            _minRadius = minRadius;
            _maxRadius = maxRadius;
        }
        
        public Vector3 SetPosition(Vector3 origin, Unit unit = null)
        {
            if (unit != null)
                throw new ArgumentException($"{GetType().Name} does not support placing units. " +
                                            "Please provide a null value for the unit parameter.");
            
            return origin.RandomPointInCircle(_minRadius, _maxRadius);
        }
    }
}