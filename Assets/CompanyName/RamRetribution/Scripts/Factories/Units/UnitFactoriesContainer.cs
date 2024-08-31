using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factories.Units.Variant;

namespace CompanyName.RamRetribution.Scripts.Factories.Units
{
    public class UnitFactoriesContainer
    {
        private readonly RamFactory _ramFactory;
        private readonly Dictionary<LocationTypes, EnemyFactory> _enemyFactories;

        public UnitFactoriesContainer(RamFactory ramFactory, params EnemyFactory[] enemyFactories)
        {
            _ramFactory = ramFactory;
            _enemyFactories = new Dictionary<LocationTypes, EnemyFactory>();

            foreach (var factory in enemyFactories)
            {
                switch (factory.GetType().Name)
                {
                    case nameof(ForestEnemiesFactory):
                        _enemyFactories.Add(LocationTypes.Forest, factory);
                        break;
                    case nameof(SandEnemiesFactory):
                        _enemyFactories.Add(LocationTypes.Sand, factory);
                        break;
                    case nameof(IceEnemiesFactory):
                        _enemyFactories.Add(LocationTypes.Ice, factory);
                        break;
                }
            }
        }

        public EnemyFactory GetEnemyFactory(int levelNumber)
        {
            return levelNumber switch
            {
                >= 1 and <= GameConstants.ForestLocationEndLevel => _enemyFactories[LocationTypes.Forest],
                
                > GameConstants.ForestLocationEndLevel and <= GameConstants.SandLocationEndLevel => _enemyFactories[
                    LocationTypes.Sand],
                
                > GameConstants.SandLocationEndLevel and <= GameConstants.IceLocationEndLevel => _enemyFactories[
                    LocationTypes.Ice],
                
                _ => throw new Exception()
            };
        }

        public RamFactory GetRamFactory()
            => _ramFactory;
    }
}