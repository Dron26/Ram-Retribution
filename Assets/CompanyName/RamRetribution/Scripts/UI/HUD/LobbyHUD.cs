using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.UI.Shop;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using StateMachine = CompanyName.RamRetribution.Scripts.FiniteStateMachine.StateMachine;

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
        
        private Wallet _wallet;

        public event Action<LobbyHUD> PlayClicked;

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
        
        public void Init(Wallet wallet)
        {
            _wallet = wallet;
            
            _walletView.Init(_wallet);
            _wallet.UpdateText();
        }
        
        private void OnPlayClicked()
        {
            PlayClicked?.Invoke(this);
        }
        
        private void OnAddMoneyClicked()
        {
            _wallet.Add(CurrencyTypes.Money, 1000);
        }

        private void DeleteGameData()
        {
            Services.PrefsDataService.Delete(DataNames.GameData.ToString());
        }

        private void DeleteShopData()
        {
            Services.PrefsDataService.Delete(DataNames.ShopDataState.ToString());
        }
    }
}