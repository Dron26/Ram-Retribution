using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Lobby.GameShop
{
    [CreateAssetMenu(menuName = "ShopItems/Ram")]
    public class RamItem : ShopItem
    {
        [field: SerializeField] public ConfigId ConfigId { get; private set; }
        
        public override void Accept(IShopItemVisitor visitor)
            => visitor.Visit(this);
    }
}