using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors
{
    public class RamsPlacementVisitor : IRamsVisitor
    {
        private readonly IReadOnlyList<Vector3> _origins;
        private readonly IPlacementStrategy _placementStrategy;

        private int _positionIndex = -1;
        
        public RamsPlacementVisitor(IReadOnlyList<Vector3> origins, IPlacementStrategy strategy)
        {
            _origins = origins;
            _placementStrategy = strategy;
        }

        public void Visit(Unit unit)
        {
            unit.Accept(this);
        }

        public void Visit(Leader leader)
        {
            leader.MoveToPoint(
                _placementStrategy
                    .SetPosition(
                        GetRandomPosition(),
                        leader),
                leader.ActivateAgent);
        }

        public void Visit(Tank tank)
        {
            tank.MoveToPoint(
                _placementStrategy
                    .SetPosition(
                        GetRandomPosition(),
                        tank),
                tank.ActivateAgent);
        }

        public void Visit(Attacker attacker)
        {
            attacker.MoveToPoint(
                _placementStrategy
                    .SetPosition(
                        GetRandomPosition(),
                        attacker),
                attacker.ActivateAgent);
        }

        public void Visit(Demolisher demolisher)
        {
            demolisher.MoveToPoint(
                _placementStrategy
                    .SetPosition(
                        GetRandomPosition(),
                        demolisher),
                demolisher.ActivateAgent);
        }

        public void Visit(Support support)
        {
            support.MoveToPoint(
                _placementStrategy
                    .SetPosition(
                        GetRandomPosition(),
                        support),
                support.ActivateAgent);
        }

        private Vector3 GetRandomPosition()
        {
            _positionIndex++;

            if (_positionIndex >= _origins.Count)
                _positionIndex = 0;
            
            return _origins[_positionIndex];
        }
    }
}