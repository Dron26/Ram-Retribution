using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class SquadsHolder
    {
        private readonly List<Squad> _enemySquads = new();
        
        public SquadsHolder(int maxRamUnits, int maxEnemyUnits, int enemiesSquadsCount)
        {
            IPlacementStrategy ramsPlacementStrategy = new RamsPlacementStrategy();
            IPlacementStrategy enemiesPlacementStrategy = new CirclePlacementStrategy(1, 3);
            
            Rams = new Squad(maxRamUnits, ramsPlacementStrategy);

            for (int i = 0; i < enemiesSquadsCount; i++)
                _enemySquads.Add(new Squad(maxEnemyUnits, enemiesPlacementStrategy));
        }

        public Squad Rams { get; }
        public IReadOnlyList<Squad> EnemySquads => _enemySquads;
        public bool HasEnemies => _enemySquads.Any(squad => squad.IsAlive);
        
        public void Add(Unit unit)
        {
            switch (unit.Type)
            {
                case UnitTypes.Ram:
                    Rams.Add(unit);
                    break;
                case UnitTypes.Enemy:
                    _enemySquads[0].Add(unit);
                    break;
                case UnitTypes.Squad:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}