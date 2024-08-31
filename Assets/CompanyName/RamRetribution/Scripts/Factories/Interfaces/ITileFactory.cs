using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.Tiles;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factories.Interfaces
{
    public interface ITileFactory<out T>
    where T : Tile
    {
        public T Create(Transform parent);
    }
}