using Generator.Scripts.Level;
using UnityEngine;

namespace Generator.Scripts.Factory
{
    public class TileFactory : MonoBehaviour
    {
        public Tile Create(Tile tile)
        {
            Tile instance = Instantiate(tile);

            return instance;
        }
    }
}