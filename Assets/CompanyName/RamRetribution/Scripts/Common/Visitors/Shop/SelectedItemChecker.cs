using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors.Shop
{
    public class SelectedItemChecker : IShopItemVisitor
    {
        private readonly ShopDataState _shopData;

        public SelectedItemChecker(ShopDataState shopData) 
            => _shopData = shopData;

        public bool IsSelected { get; private set; }
        public bool IsSingleSelectItem { get; private set; }
        
        public void Visit(ShopItem item)
        {
            item.Accept(this);
        }

        public void Visit(SkinItem item)
        {
            IsSelected = _shopData.SelectedSkin == item.SkinsId;
            IsSingleSelectItem = true;
        }

        public void Visit(SpellItem item)
        {
            IsSingleSelectItem = false;
        }

        public void Visit(RamItem item)
        {
            IsSelected = _shopData.SelectedRams.Contains(item.ConfigId);
            IsSingleSelectItem = false;
        }
    }
}