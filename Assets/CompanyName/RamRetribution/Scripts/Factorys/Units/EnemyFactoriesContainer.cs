using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Enemies;
using Generator.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class EnemyFactoriesContainer
    {
        private readonly Dictionary<GridTypes, IUnitFactory<Enemy>> _factories;

        public EnemyFactoriesContainer(params IUnitFactory<Enemy>[] factories)
        {
            _factories = new Dictionary<GridTypes, IUnitFactory<Enemy>>();
            
            foreach (var tileFactory in factories)
            {
                switch (tileFactory.GetType().Name)
                {
                    case nameof(ForestEnemiesFactory):
                        _factories.Add(GridTypes.Forest, tileFactory);
                        break;
                }
            }
        }

        public IUnitFactory<Enemy> Get(int lvlNumber)
        {
            return lvlNumber switch
            {
                >= 0 and <= 100 => _factories[GridTypes.Forest],
                _ => throw new Exception()
            };
        }
    }
}