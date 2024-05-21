using System;
using CompanyName.RamRetribution.Scripts.Boot;
using CompanyName.RamRetribution.Scripts.Common.AssetLoad;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace CompanyName.RamRetribution.Scripts
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private SaveLoadService _saveLoadService;
        [SerializeField] private Transform _ramContainer;
        [SerializeField] private Button _loadLeaderDataButton;
        [SerializeField] private Button _loadGateButton;
        [SerializeField] private Button _loadLevelButton;
        
        private void Awake()
        {
            _saveLoadService.Init(new FileDataService(new JsonSerializer()));
        }

        private void OnEnable()
        {
            _loadLeaderDataButton.onClick.AddListener(LoadLeaderData);
            _loadLevelButton.onClick.AddListener(LoadLevel);
            _loadGateButton.onClick.AddListener(LoadGateData);
        }

        private void OnDisable()
        {
            _loadLeaderDataButton.onClick.RemoveListener(LoadLeaderData);
            _loadLevelButton.onClick.RemoveListener(LoadLevel);
            _loadGateButton.onClick.RemoveListener(LoadGateData);
        }

        private void LoadLevel()
        {
            _saveLoadService.Load<LevelData>(DataNames.LevelData);
        }

        private void LoadLeaderData()
        {
            _saveLoadService.Load<LeaderDataState>(DataNames.LeaderDataState);
        }

        private void LoadGateData()
        {
            _saveLoadService.Load<GateData>(DataNames.GateData);
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