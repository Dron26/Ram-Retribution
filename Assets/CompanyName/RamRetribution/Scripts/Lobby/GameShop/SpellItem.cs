using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Lobby.GameShop
{
    [CreateAssetMenu(menuName = "ShopItems/Spell")]
    public class SpellItem : ShopItem
    {
        [field: SerializeField] public SpellTypes SpellTypes { get; private set; }
        
        public override void Accept(IShopItemVisitor visitor)
            => visitor.Visit(this);
    }
}