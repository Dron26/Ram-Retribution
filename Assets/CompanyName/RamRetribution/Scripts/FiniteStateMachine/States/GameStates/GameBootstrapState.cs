using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates
{
    public class GameBootstrapState : BaseState
    {
        private readonly StateMachine _stateMachine;
        private LeaderDataState _leaderData;
        private ShopDataState _shopDataState;
        private GameData _gameData;
        private BattleBootstrap _battleBootstrap;

        public GameBootstrapState(StateMachine stateMachine)
            => _stateMachine = stateMachine;

        public override void Enter()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneNames.Gameplay);

            if (asyncOperation != null)
                asyncOperation.completed += _ => LoadLevel();
        }

        private void LoadLevel()
        {
            LoadData();
            InitBattle();
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
            var battleCommander = Object.Instantiate(Services
                .ResourceLoadService
                .Load<BattleCommander>($"{AssetPaths.CommonPrefabs}{nameof(BattleCommander)}"));

            var battleBootstrap = new BattleBootstrap(_gameData, _shopDataState.SelectedRams, _leaderData);
            battleBootstrap.Init(battleCommander);
        }
    }
}