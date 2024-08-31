using System;
using CompanyName.RamRetribution.Scripts.Common.Visitors.Variants;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Components
{
    public class RamsPlacementStrategy : IPlacementStrategy
    {
        private readonly RamsPlacementVisitor _ramsPlacementVisitor;

        public RamsPlacementStrategy(RamsPlacementVisitor placementVisitor) 
            => _ramsPlacementVisitor = placementVisitor;

        public Vector3 SetPosition(Vector3 origin, Unit unit)
        {
            if (unit is not Ram ram)
                throw new ArgumentException($"Expected unit of type 'Ram', but received '{unit.GetType().Name}'.");

            _ramsPlacementVisitor.Visit(ram);
            
            return _ramsPlacementVisitor.Position + origin;
        }
    }
}