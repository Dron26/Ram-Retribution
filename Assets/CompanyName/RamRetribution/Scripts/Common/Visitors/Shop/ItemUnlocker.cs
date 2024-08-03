using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors.Shop
{
    public class ItemUnlocker : IShopItemVisitor
    {
        private readonly ShopDataState _shopData;

        public ItemUnlocker(ShopDataState shopData)
            => _shopData = shopData;

        public void Visit(ShopItem item)
        {
            item.Accept(this);
        }

        public void Visit(SkinItem item)
        {
            _shopData.OpenSkin(item.SkinsId);
        }

        public void Visit(SpellItem item)
        {
            _shopData.OpenSpell(item.SpellsId);
        }

        public void Visit(RamItem item)
        {
            _shopData.OpenRam(item.ConfigId);
        }
    }
}