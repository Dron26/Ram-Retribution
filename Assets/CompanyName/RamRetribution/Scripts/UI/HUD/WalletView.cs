using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using TMPro;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.UI.Shop
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _hornsText;
        
        private Wallet _wallet;

        private void OnDestroy() 
            => _wallet.CurrencyChanged -= OnCurrencyChanged;

        public void Init(Wallet wallet)
        {
             _wallet = wallet;
             _wallet.CurrencyChanged += OnCurrencyChanged;
        }
        
        private void OnCurrencyChanged(CurrencyTypes currency, int amount)
        {
            switch (currency)
            {
                case CurrencyTypes.Money:
                    _moneyText.text = amount.ToString();
                    break;
                case CurrencyTypes.Horns:
                    _hornsText.text = amount.ToString();
                    break;
            }
        }
    }
}