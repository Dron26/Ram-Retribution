using CompanyName.RamRetribution.Scripts.Boot;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.UI.HUD;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private WalletView _walletView;

        private GameLoseScreen _gameLoseScreen;
        private StateMachine _gameStateMachine;

        public void Init(StateMachine machine, Wallet wallet)
        {
            _gameStateMachine = machine;
            _walletView.Init(wallet);
        }

        public void ShowLoseScreen()
        {
            var screenPrefab = Services
                .ResourceLoadService
                .Load<GameLoseScreen>($"{AssetPaths.CommonPrefabs}{nameof(GameLoseScreen)}");

            var loseScreen = Instantiate(screenPrefab);
            loseScreen.Init(_gameStateMachine);
        }
    }
}