using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors
{
    public class SetPositionVisitor : IUnitVisitor
    {
        private readonly Vector3 _ramOrigin;
        private readonly Vector3 _enemyOrigin;
        private readonly IPlacementStrategy _ramPlacementStrategy;
        private readonly IPlacementStrategy _enemiesPlacementStrategy;
        
        public SetPositionVisitor(
            IPlacementStrategy ramPlacementStrategy, 
            IPlacementStrategy enemiesPlacementStrategy, 
            Vector3 ramOrigin,
            Vector3 enemyOrigin)
        {
            _ramOrigin = ramOrigin;
            _enemyOrigin = enemyOrigin;
            _ramPlacementStrategy = ramPlacementStrategy;
            _enemiesPlacementStrategy = enemiesPlacementStrategy;
        }
        
        public void Visit(Unit unit)
        {
            unit.Accept(this);
        }

        public void Visit(IRam ram)
        {
            var concreteRam = ram as Unit;
            concreteRam.transform.position = _ramPlacementStrategy.SetPosition(_ramOrigin);
        }

        public void Visit(IEnemy enemy)
        {
            var concreteEnemy = enemy as Unit;
            concreteEnemy.transform.position = _enemiesPlacementStrategy.SetPosition(_enemyOrigin);
        }

        public void Visit(Squad squad)
        {
        }
    }
}