using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Common.Visitors;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.UI.Shop;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Factorys
{
    public class ShopItemViewFactory
    {
        private readonly ShopItemView _skinView;
        private ShopItemViewPrefabVisitor _visitor;

        public ShopItemViewFactory()
        {
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
            => Services.ResourceLoadService.Load<T>(
                $"{AssetPaths.ShopPrefabs}{typeof(T).Name}");
    }
}