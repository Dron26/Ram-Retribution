using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Common.Visitors;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;

namespace CompanyName.RamRetribution.Scripts.Boot
{
    public class ShopBootstrap
    {
        private readonly Shop _shop;

        private ShopContent _content;
        private LeaderDataState _leaderData;
        private ShopDataState _shopData;

        private SkinSelector _skinSelector;
        private ItemUnlocker _itemUnlocker;
        private OpenItemChecker _openItemChecker;
        private SelectedItemChecker _selectedItemChecker;

        public ShopBootstrap(Shop shop)
            => _shop = shop;

        public void Init(Wallet wallet)
        {
            LoadData();

            CreateVisitors();

            _shop.Init(wallet, _shopData, _content, _skinSelector, _itemUnlocker, _openItemChecker, _selectedItemChecker);
        }

        private void LoadData()
        {
            _content = new ShopContent();
            _content.LoadAllAssets();
            
            _leaderData = Services.PrefsDataService.Load<LeaderDataState>(
                DataNames.LeaderDataState.ToString());

            _shopData = Services.PrefsDataService.Load<ShopDataState>(
                DataNames.ShopDataState.ToString());
        }

        private void CreateVisitors()
        {
            _skinSelector = new SkinSelector(_shopData);
            _itemUnlocker = new ItemUnlocker(_shopData);
            _openItemChecker = new OpenItemChecker(_shopData);
            _selectedItemChecker = new SelectedItemChecker(_shopData);
        }
    }
}