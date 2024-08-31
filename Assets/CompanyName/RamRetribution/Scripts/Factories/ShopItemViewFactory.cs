using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Visitors.Shop;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.UI.Shop;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factories
{
    public class ShopItemViewFactory
    {
        private readonly ShopItemView _skinView;
        private readonly IResourceLoadService _loadService;
        private ShopItemViewPrefabVisitor _visitor;

        public ShopItemViewFactory(IResourceLoadService loadService)
        {
            _loadService = loadService;
            _skinView = LoadAsset<ShopItemView>();
        }
        
        public ShopItemView Create(ShopItem item, Transform parent)
        {
            _visitor = new ShopItemViewPrefabVisitor(_skinView);
            
            _visitor.Visit(item);

            var instance = Object.Instantiate(_visitor.Prefab, parent);
            instance.Init(item);
            
            return instance;
        }
        
        private T LoadAsset<T>()
            where T : Object
            => _loadService.Load<T>(
                $"{AssetPaths.ShopPrefabs}{typeof(T).Name}");
    }
}