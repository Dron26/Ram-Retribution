using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors.Shop
{
    public class ItemSelector : IShopItemVisitor
    {
        private readonly ShopDataState _shopData;

        public ItemSelector(ShopDataState shopData) 
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
            _shopData.SelectRam(item.ConfigId);
        }
    }
}