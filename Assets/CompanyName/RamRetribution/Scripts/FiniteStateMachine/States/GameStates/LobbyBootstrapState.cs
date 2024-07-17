using CompanyName.RamRetribution.Scripts.Boot;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Lobby;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.UI.HUD;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates
{
    public class LobbyBootstrapState : BaseState
    {
        private readonly StateMachine _stateMachine;
        private LobbyCanvas _instance;
        private Wallet _wallet;

        public LobbyBootstrapState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Enter()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneNames.Lobby);

            if (asyncOperation != null)
                asyncOperation.completed += _ => PrepareScene();
        }

        public override void Exit()
        {
            
        }

        private void PrepareScene()
        {
            GameData gameData = Services.PrefsDataService.Load<GameData>(
                DataNames.GameData.ToString());
            
            CreateLobby();
            CreateHUD(gameData);
            InitShop();
        }

        private void CreateLobby()
        {
            var lobbyPrefab = Services.ResourceLoadService.Load<LobbyCanvas>(
                $"{AssetPaths.CommonPrefabs}{nameof(LobbyCanvas)}");
            
            _instance = Object.Instantiate(lobbyPrefab);
        }

        private void CreateHUD(GameData gameData)
        {
            _wallet = new Wallet(gameData);

            var hudPrefab = Services.ResourceLoadService.Load<LobbyHUD>(
                $"{AssetPaths.CommonPrefabs}{nameof(LobbyHUD)}");

            var hud = Object.Instantiate(hudPrefab);
            hud.Init(_wallet);
            hud.PlayClicked += OnPlayClicked;
        }

        private void InitShop()
        {
            var shopBootstrap = new ShopBootstrap(_instance.Shop);
            shopBootstrap.Init(_wallet);
        }

        private void OnPlayClicked(LobbyHUD hud)
        {
            hud.PlayClicked -= OnPlayClicked;
            _stateMachine.SetState<GameBootstrapState>();
        }
    }
}