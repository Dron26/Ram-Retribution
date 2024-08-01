using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts._TestScripts
{
    public class Starter : MonoBehaviour
    {
        private LevelBuilder _levelBuilder;
    
        private void Awake()
        {
            Services.Init();
            IFactory<Tile> factory = new TileFactory(); 
            _levelBuilder = new LevelBuilder(factory);
        }

        private async void Start()
        {
            await _levelBuilder.EntryBuild(1);
            Debug.Log($"Ended");
        }
    }
}
