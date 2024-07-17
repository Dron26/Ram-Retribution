using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using CompanyName.RamRetribution.Scripts.Units.Enemies;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors
{
    public class UnitsPlacementVisitor : IUnitVisitor
    {
        private readonly Vector3 _origin;
        private readonly IPlacementStrategy _placementStrategy;

        public UnitsPlacementVisitor(Vector3 origin, IPlacementStrategy strategy)
        {
            _origin = origin;
            _placementStrategy = strategy;
        }
        
        public void Visit(Unit unit)
        {
            unit.Accept(this);
        }

        public void Visit(Leader leader)
        {
            leader.MoveToPoint(_placementStrategy.SetPosition(_origin,leader), leader.ActivateAgent);
        }

        public void Visit(Tank tank)
        {
            tank.MoveToPoint(_placementStrategy.SetPosition(_origin,tank), tank.ActivateAgent);
        }

        public void Visit(Attacker attacker)
        {
            attacker.MoveToPoint(_placementStrategy.SetPosition(_origin,attacker), attacker.ActivateAgent);
        }

        public void Visit(Demolisher demolisher)
        {
            demolisher.MoveToPoint(_placementStrategy.SetPosition(_origin,demolisher), demolisher.ActivateAgent);
        }

        public void Visit(Support support)
        {
            support.MoveToPoint(_placementStrategy.SetPosition(_origin,support), support.ActivateAgent);
        }

        public void Visit(LightEnemy lightEnemy)
        {
            lightEnemy.MoveToPoint(_placementStrategy.SetPosition(_origin,lightEnemy), lightEnemy.ActivateAgent);
        }

        public void Visit(MediumEnemy mediumEnemy)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(HeavyEnemy heavyEnemy)
        {
            heavyEnemy.MoveToPoint(_placementStrategy.SetPosition(_origin, heavyEnemy), heavyEnemy.ActivateAgent);
        }
    }
}