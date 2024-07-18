using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Gameplay;
using CompanyName.RamRetribution.Scripts.UI;
using Cysharp.Threading.Tasks;
using Generator.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates
{
    public class GameBootstrapState : BaseState
    {
        private readonly StateMachine _stateMachine;
        private Game _game;
        private ModulesContainer _modulesContainer;
        private LeaderDataState _leaderData;
        private ShopDataState _shopDataState;
        private GameData _gameData;

        public GameBootstrapState(StateMachine stateMachine)
            => _stateMachine = stateMachine;

        public override void Enter()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneNames.Gameplay);

            if (asyncOperation != null)
                asyncOperation.completed += _ => Init();
        }

        private void Init()
        {
            _modulesContainer = new ModulesContainer();
            
            LoadData();
            InitLevelBuilder();
            InitBattle();
            InitUI();
            
            _game = new Game(_modulesContainer);
            _game.Start().Forget();
        }

        private void LoadData()
        {
            _leaderData = Services.PrefsDataService.Load<LeaderDataState>(
                DataNames.LeaderDataState.ToString());

            _shopDataState = Services.PrefsDataService.Load<ShopDataState>(
                DataNames.ShopDataState.ToString());

            _gameData = Services.PrefsDataService.Load<GameData>(
                DataNames.GameData.ToString());
        }

        private void InitBattle()
        {
            var battleBootstrap = new BattleBootstrap(_modulesContainer,_gameData, _shopDataState.SelectedRams, _leaderData);
            battleBootstrap.Init();
        }

        private void InitLevelBuilder()
        {
            var levelBuilder = Object.Instantiate(Services
                .ResourceLoadService
                .Load<LevelBuilder>($"{AssetPaths.CommonPrefabs}{nameof(LevelBuilder)}"));
            
            levelBuilder.Init();
            
            _modulesContainer.Register(levelBuilder);
        }

        private void InitUI()
        {
            var uiPrefab = Services
                .ResourceLoadService
                .Load<GameUI>($"{AssetPaths.CommonPrefabs}{nameof(GameUI)}");

            var gameUI = Object.Instantiate(uiPrefab);
            gameUI.Init(_stateMachine);
            
            _modulesContainer.Register(gameUI);
        }
    }
}