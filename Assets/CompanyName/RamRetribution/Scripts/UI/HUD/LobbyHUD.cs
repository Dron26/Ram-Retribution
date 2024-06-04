using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.UI.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.UI.HUD
{
    public class LobbyHUD : MonoBehaviour
    {
        [SerializeField] private PlayButton _playButton;
        [SerializeField] private WalletView walletView;
        
        [Header("For Tests buttons")]
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _deleteGameData;
        [SerializeField] private Button _deleteShopData;
        
        private StateMachine _stateMachine;
        private Wallet _wallet;
        
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
        
        public void Init(StateMachine stateMachine, Wallet wallet)
        {
            _stateMachine = stateMachine;
            _wallet = wallet;
            
            walletView.Init(_wallet);
            _wallet.UpdateText();
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
            Services.PrefsDataService.Delete(DataNames.GameData.ToString());
        }

        private void DeleteShopData()
        {
            Services.PrefsDataService.Delete(DataNames.ShopDataState.ToString());
        }
    }
}