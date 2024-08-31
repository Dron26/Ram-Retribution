using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.Interfaces;
using CompanyName.RamRetribution.Scripts.UI;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates
{
    public class GameBootstrapState : IState
    {
        private readonly GameData _gameData;
        private readonly ISaveLoadDataService _saveLoadDataService;
        
        private List<ConfigId> _selectedRams;

        public GameBootstrapState(ISaveLoadDataService saveLoadDataService, GameData gameData)
        {
            _saveLoadDataService = saveLoadDataService;
            _gameData = gameData;
        }

        public void Enter() 
            => SceneManager.LoadSceneAsync(SceneNames.Gameplay);

        public void Exit() 
            => _saveLoadDataService.Save(_gameData);

        private void InitUI()
        {
            var uiPrefab = Services
                .ResourceLoadService
                .Load<GameUI>($"{AssetPaths.CommonPrefabs}{nameof(GameUI)}");

            var gameUI = Object.Instantiate(uiPrefab);
            // var wallet = new Wallet(_gameData);
            // gameUI.Init(_stateMachine, wallet);
        }
    }
}