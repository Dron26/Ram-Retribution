using CompanyName.RamRetribution.Scripts.Lobby.GameShop;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IShopItemVisitor
    {
        public void Visit(ShopItem item);
        public void Visit(SkinItem item);
        public void Visit(SpellItem item);
        public void Visit(RamItem item);
    }
}