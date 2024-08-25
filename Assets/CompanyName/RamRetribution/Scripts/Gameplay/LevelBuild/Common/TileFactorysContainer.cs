using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Interfaces;
using Generator.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Common
{
    public class TileFactoriesContainer
    {
        private readonly Dictionary<GridTypes, ITileFactory<Tile>> _tileFactories;

        public TileFactoriesContainer(params ITileFactory<Tile>[] factories)
        {
            _tileFactories = new Dictionary<GridTypes, ITileFactory<Tile>>();
            
            foreach (var tileFactory in factories)
            {
                switch (tileFactory.GetType().Name)
                {
                    case nameof(ForestTileFactory):
                        _tileFactories.Add(GridTypes.Forest, tileFactory);
                        break;
                    case nameof(SandTileFactory):
                        _tileFactories.Add(GridTypes.Sand, tileFactory);
                        break;
                    case nameof(IceTileFactory):
                        _tileFactories.Add(GridTypes.Ice, tileFactory);
                        break;
                }
            }
        }
        
        public ITileFactory<Tile> GetFactory(int lvlNumber)
        {
            return lvlNumber switch
            {
                >= 0 and <= 100 => _tileFactories[GridTypes.Forest],
                >= 101 and <= 200 => _tileFactories[GridTypes.Sand],
                >= 201 and <= GameConstants.MaxLevels => _tileFactories[GridTypes.Ice],
                _ => throw new Exception()
            };
        }
    }
}