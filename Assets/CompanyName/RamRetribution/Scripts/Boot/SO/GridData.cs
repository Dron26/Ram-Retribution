using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using Generator.Scripts.Common.Enums;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(menuName = "GridData")]
    public class GridData : ScriptableObject
    {
        private const int _gridSizeX = GridConstants.SizeX;
        private const int _gridSizeY = GridConstants.SizeY;
        
        public TileType[] Tiles = new TileType[_gridSizeX * _gridSizeY];
    }
}