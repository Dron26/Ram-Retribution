using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors
{
    public class SkinSelector : IShopItemVisitor
    {
        private ShopDataState _shopData;

        public SkinSelector(ShopDataState shopData) 
            => _shopData = shopData;

        public void Visit(ShopItem item)
        {
            item.Accept(this);
        }

        public void Visit(SkinItem item)
        {
            _shopData.SelectedSkin = item.SkinType;
        }

        public void Visit(SpellItem item)
        {
        }

        public void Visit(RamItem item)
        {
        }
    }
}