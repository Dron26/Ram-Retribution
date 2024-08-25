using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface ITileFactory<out T>
    where T : Tile
    {
        public T Create(Transform parent);
    }
}