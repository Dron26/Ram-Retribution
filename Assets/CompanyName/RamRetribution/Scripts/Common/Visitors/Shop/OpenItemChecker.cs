using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors
{
    public class OpenItemChecker : IShopItemVisitor
    {
        private ShopDataState _shopData;

        public OpenItemChecker(ShopDataState shopData) 
            => _shopData = shopData;

        public bool IsOpen { get; private set; }
        
        public void Visit(ShopItem item)
        {
            item.Accept(this);
        }

        public void Visit(SkinItem item)
        {
            IsOpen = _shopData.OpenedSkins.Contains(item.SkinType);
        }

        public void Visit(SpellItem item)
        {

        }

        public void Visit(RamItem item)
        {
            IsOpen = _shopData.OpenedRams.Contains(item.ConfigId);
        }
    }
}