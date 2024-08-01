using CompanyName.RamRetribution.Scripts.Common.Enums;
using Generator.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class Grid
    {
        public Grid(TileType[,] tiles)
        {
            Tiles = tiles;
        }
        
        public TileType[,] Tiles { get; private set; }
    }
}