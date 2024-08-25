using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CompanyName.RamRetribution.Scripts.UI.HUD
{
    public class LobbyHUD : MonoBehaviour
    {
        [SerializeField] private PlayButton _playButton;
        [SerializeField] private WalletView _walletView;

        [Header("For Tests buttons")] 
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _deleteGameData;
        [SerializeField] private Button _deleteShopData;

        private StateMachine _stateMachine;
        private Wallet _wallet;
        private IDataService _dataService;

        private void OnEnable()
        {
            _playButton.Clicked += OnPlayClicked;
            _addMoneyButton.onClick.AddListener(OnAddMoneyClicked);
            _deleteGameData.onClick.AddListener(DeleteGameData);
            _deleteShopData.onClick.AddListener(DeleteShopData);
        }

        private void OnDisable()
        {
            _playButton.Clicked -= OnPlayClicked;
            _addMoneyButton.onClick.RemoveListener(OnAddMoneyClicked);
            _deleteGameData.onClick.RemoveListener(DeleteGameData);
            _deleteShopData.onClick.RemoveListener(DeleteShopData);
        }

        [Inject]
        public void Construct(StateMachine stateMachine, Wallet wallet, IDataService dataService)
        {
            _stateMachine = stateMachine;
            _wallet = wallet;
            _walletView.Init(_wallet);
            _dataService = dataService;
        }

        private void OnPlayClicked()
        {
            _stateMachine.SetState<GameBootstrapState>();
        }

        private void OnAddMoneyClicked()
        {
            _wallet.Add(CurrencyTypes.Money, 1000);
        }

        private void DeleteGameData()
        {
            _dataService.Delete(DataNames.GameData.ToString());
        }

        private void DeleteShopData()
        {
            _dataService.Delete(DataNames.ShopDataState.ToString());
        }
    }
}