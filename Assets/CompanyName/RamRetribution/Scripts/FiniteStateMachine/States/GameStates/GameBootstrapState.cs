using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Gameplay;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.UI;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates
{
    public class GameBootstrapState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly GameData _gameData;
        private readonly IDataService _dataService;
        
        private List<ConfigId> _selectedRams;

        public GameBootstrapState(StateMachine stateMachine, GameData gameData, IDataService dataService)
        {
            _stateMachine = stateMachine;
            _gameData = gameData;
            _dataService = dataService;
        }

        public void Enter() 
            => SceneManager.LoadSceneAsync(SceneNames.Gameplay);

        public void Exit() 
            => _dataService.Save(_gameData);

        private void Init()
        {
            InitUI();
        }

        private void InitUI()
        {
            var uiPrefab = Services
                .ResourceLoadService
                .Load<GameUI>($"{AssetPaths.CommonPrefabs}{nameof(GameUI)}");

            var gameUI = Object.Instantiate(uiPrefab);
            var wallet = new Wallet(_gameData);

            gameUI.Init(_stateMachine, wallet);

            // _modulesContainer.Register(gameUI);
            // _modulesContainer.Register(wallet);
        }
    }
}