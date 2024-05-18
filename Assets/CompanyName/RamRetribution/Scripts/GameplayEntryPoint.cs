using CompanyName.RamRetribution.Scripts.Boot;
using CompanyName.RamRetribution.Scripts.Common.AssetLoad;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private SaveLoadService _saveLoadService;
        [SerializeField] private Transform _ramContainer;

        private GameData _gameData;
        
        private void Awake()
        {
            _saveLoadService.Init(new FileDataService(new JsonSerializer()));
            _gameData = _saveLoadService.Load(SaveNames.CoreSave.ToString());
        }

        private T LoadAsset<T>() 
            where T : Object
        {
            IAssets assetProvider = new AssetProvider();
            var prefab = assetProvider.Get<T>($"{AssetPath.RamDataPath}{typeof(T).Name}");
            return prefab;
        }
    }
}