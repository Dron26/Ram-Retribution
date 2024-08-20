using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Gameplay;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates
{
    public class GameBootstrapState : BaseState
    {
        private readonly StateMachine _stateMachine;
        private Game _game;
        private ModulesContainer _modulesContainer;
        private LeaderDataState _leaderData;
        private List<ConfigId> _selectedRams;
        private GameData _gameData;

        public GameBootstrapState(StateMachine stateMachine)
            => _stateMachine = stateMachine;

        public override void Enter()
        {
            Services.InitGameSceneCtx();
            var asyncOperation = SceneManager.LoadSceneAsync(SceneNames.Gameplay);

            if (asyncOperation != null)
                asyncOperation.completed += _ => Init();
        }

        public override void Exit()
        {
            Services.LvlCombinator.UnSubscribeFromGameEvents(_game);
            _modulesContainer.Get<BattleMediator>().UnRegisterSpawner();
            
            Services.PrefsDataService.Save(_gameData);
        }
        
        private void Init()
        {
            _modulesContainer = new ModulesContainer();

            LoadData();
            InitLevelBuilder();
            InitBattle();
            InitUI();

            _game = new Game(_modulesContainer);
            Services.LvlCombinator.SubscribeToGameEvents(_game);
            _game.StartAsync(LoadLevel()).Forget();
        }

        private void LoadData()
        {
            _leaderData = Services.PrefsDataService.Load<LeaderDataState>(
                DataNames.LeaderDataState.ToString());

            _selectedRams = Services.PrefsDataService.Load<ShopDataState>(
                DataNames.ShopDataState.ToString()).SelectedRams;

            _gameData = Services.PrefsDataService.Load<GameData>(
                DataNames.GameData.ToString());
        }

        private void InitBattle()
        {
            var battleBootstrap =
                new BattleBootstrap(_modulesContainer, _selectedRams, _leaderData);

            battleBootstrap.Init();
        }

        private void InitLevelBuilder()
        {
            IFactory<Tile> tileFactory = new TileFactory();
            var levelBuilder = new LevelBuilder(tileFactory);

            _modulesContainer.Register(levelBuilder);
        }

        private void InitUI()
        {
            var uiPrefab = Services
                .ResourceLoadService
                .Load<GameUI>($"{AssetPaths.CommonPrefabs}{nameof(GameUI)}");
            
            var gameUI = Object.Instantiate(uiPrefab);
            var wallet = new Wallet(_gameData);
            
            gameUI.Init(_stateMachine, wallet);

            _modulesContainer.Register(gameUI);
            _modulesContainer.Register(wallet);
        }

        private int LoadLevel()
        {
            var levelNumber = _gameData.GetLastPassedLevelIndex();
            Debug.Log($"Level: {levelNumber} start");

            return levelNumber;
        }
    }
}