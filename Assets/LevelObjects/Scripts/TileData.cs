using UnityEngine;

namespace LevelObjects.Scripts
{
    public class TileData
    {
        public readonly float X;
        public readonly float Y;
        public readonly TileType TileType;
        public readonly GameObject Tile;
        public bool isUp;

        public TileData(float x, float y, TileType tileType, GameObject tile = null)
        {
            X = x;
            Y = y;
            TileType = tileType;
            Tile = tile;
        }

        public void SetUp(bool isUp)
        {
            this.isUp = isUp;
        }
    }
}