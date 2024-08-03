using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Common.Visitors;
using CompanyName.RamRetribution.Scripts.Common.Visitors.Shop;
using CompanyName.RamRetribution.Scripts.UI.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.Lobby.GameShop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopView _view;

        [Header("Buttons")] 
        [SerializeField] private CategoryButton _skinsButton;
        [SerializeField] private CategoryButton _ramsButton;
        [SerializeField] private CategoryButton _spellsButton;
        [SerializeField] private BuyButton _buyButton;
        [SerializeField] private Button _selectionButton;
        [SerializeField] private Button _unSelectionButton;
        [SerializeField] private Image _selectedText;

        private Wallet _wallet;
        private ShopContent _content;
        private ShopItemView _selectedView;

        private ShopDataState _shopDataState;
        private ItemSelector _itemSelector;
        private ItemUnlocker _itemUnlocker;
        private OpenItemChecker _openItemChecker;
        private SelectedItemChecker _selectedItemChecker;

        private CurrencyTypes Currency => _selectedView.Currency;
        private int Price => _selectedView.Price;

        private void OnEnable()
        {
            _skinsButton.Clicked += OnSkinsButtonClicked;
            _ramsButton.Clicked += OnRamButtonClicked;
            _spellsButton.Clicked += OnSpellButtonClicked;

            _buyButton.Clicked += OnBuyButtonClicked;
            _selectionButton.onClick.AddListener(OnSelectionButtonClicked);
        }

        private void OnDisable()
        {
            _skinsButton.Clicked -= OnSkinsButtonClicked;
            _ramsButton.Clicked -= OnRamButtonClicked;
            _spellsButton.Clicked -= OnSpellButtonClicked;

            _selectionButton.onClick.RemoveListener(OnSelectionButtonClicked);
            _buyButton.Clicked -= OnBuyButtonClicked;
        }

        public void Init(
            Wallet wallet,
            ShopDataState shopDataState,
            ShopContent content,
            ItemSelector itemSelector,
            ItemUnlocker itemUnlocker,
            OpenItemChecker openItemChecker,
            SelectedItemChecker selectedItemChecker)
        {
            _wallet = wallet;
            _shopDataState = shopDataState;
            _content = content;
            _itemSelector = itemSelector;
            _itemUnlocker = itemUnlocker;
            _openItemChecker = openItemChecker;
            _selectedItemChecker = selectedItemChecker;

            _view.Init(_openItemChecker, _selectedItemChecker);
            _view.ItemViewClicked += OnItemViewClicked;

            OnSkinsButtonClicked();
            _selectionButton.gameObject.SetActive(false);
            _buyButton.gameObject.SetActive(false);
        }

        private void OnItemViewClicked(ShopItemView view)
        {
            _selectedView = view;

            _openItemChecker.Visit(_selectedView.Item);

            if (_openItemChecker.IsOpen)
            {
                _selectedItemChecker.Visit(_selectedView.Item);

                if (_selectedItemChecker.IsSelected)
                {
                    ShowSelectedText();
                    return;
                }

                ShowSelectionButton();
            }
            else
            {
                ShowBuyButton(Currency, Price);
            }
        }

        #region ShowButtons

        private void ShowBuyButton(CurrencyTypes currency, int price)
        {
            _buyButton.gameObject.SetActive(true);
            _buyButton.UpdateText(currency, price);

            if (_wallet.IsEnough(currency, price))
                _buyButton.Unlock();
            else
                _buyButton.Lock();

            _selectionButton.gameObject.SetActive(false);
            _selectedText.gameObject.SetActive(false);
            _unSelectionButton.gameObject.SetActive(false);
        }

        private void ShowSelectionButton()
        {
            _selectionButton.gameObject.SetActive(true);
            _unSelectionButton.gameObject.SetActive(false);
            _buyButton.gameObject.SetActive(false);
            _selectedText.gameObject.SetActive(false);
        }

        private void ShowSelectedText()
        {
            _selectedText.gameObject.SetActive(true);
            _unSelectionButton.gameObject.SetActive(true);
            _buyButton.gameObject.SetActive(false);
            _selectionButton.gameObject.SetActive(false);
        }

        #endregion

        #region ActionsHandlers

        private void OnBuyButtonClicked()
        {
            if (_wallet.IsEnough(Currency, Price))
            {
                _wallet.Remove(Currency, Price);

                _itemUnlocker.Visit(_selectedView.Item);

                SelectItem();

                _selectedView.Unlock();

                Services.PrefsDataService.Save(_shopDataState);
            }

            OnItemViewClicked(_selectedView);
        }

        private void OnSelectionButtonClicked()
        {
            SelectItem();

            OnItemViewClicked(_selectedView);
        }

        private void OnSkinsButtonClicked()
        {
            _spellsButton.Unselect();
            _ramsButton.Unselect();
            _skinsButton.Select();

            _view.CreateItemViews(_content.SkinItems);
        }

        private void OnSpellButtonClicked()
        {
            _ramsButton.Unselect();
            _skinsButton.Unselect();
            _spellsButton.Select();

            _view.CreateItemViews(_content.SpellItems);
        }

        private void OnRamButtonClicked()
        {
            _spellsButton.Unselect();
            _skinsButton.Unselect();
            _ramsButton.Select();

            _view.CreateItemViews(_content.RamItems);
        }

        #endregion

        private void SelectItem()
        {
            _itemSelector.Visit(_selectedView.Item);
            _view.Select(_selectedView);
        }

        private void UnselectButton()
        {
            
        }
    }
}