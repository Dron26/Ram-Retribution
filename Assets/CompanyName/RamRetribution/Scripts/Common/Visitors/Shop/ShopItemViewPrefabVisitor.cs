using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.UI.Shop;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors
{
    public class ShopItemViewPrefabVisitor : IShopItemVisitor
    {
        private readonly ShopItemView _skinPrefab;

        public ShopItemViewPrefabVisitor(ShopItemView skinPrefab)
        {
            _skinPrefab = skinPrefab;
        }

        public ShopItemView Prefab { get; private set; }
        
        public void Visit(ShopItem item)
        {
            item.Accept(this);
        }

        public void Visit(SkinItem item)
        {
            Prefab = _skinPrefab;
        }

        public void Visit(SpellItem item)
        {
            Prefab = _skinPrefab;
        }

        public void Visit(RamItem item)
        {
            Prefab = _skinPrefab;
        }
    }
}