using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors
{
    public class SelectedItemChecker : IShopItemVisitor
    {
        private ShopDataState _shopData;

        public SelectedItemChecker(ShopDataState shopData) 
            => _shopData = shopData;

        public bool IsSelected { get; private set; }
        
        public void Visit(ShopItem item)
        {
            item.Accept(this);
        }

        public void Visit(SkinItem item)
        {
            IsSelected = _shopData.SelectedSkin == item.SkinType;
        }

        public void Visit(SpellItem item)
        {
        }

        public void Visit(RamItem item)
        {
        }
    }
}