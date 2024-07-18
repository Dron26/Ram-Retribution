using Generator.Scripts.Common.Enums;
using UnityEngine;

namespace Generator.Scripts
{
    [CreateAssetMenu(menuName = "GridData")]
    public class GridData : ScriptableObject
    {
        private const int _gridSizeX = Constant.SizeX;
        private const int _gridSizeY= Constant.SizeY;
        
        public TileType[] Tiles = new TileType[_gridSizeX* _gridSizeY];
    }
}