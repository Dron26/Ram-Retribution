using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Lobby.GameShop
{
    [CreateAssetMenu(menuName = "ShopItems/Skin")]
    public class SkinItem : ShopItem
    {
        [field: SerializeField] public GameObject Model { get; private set; }
        [field: SerializeField] public SkinTypes SkinType { get; private set; }
        
        public override void Accept(IShopItemVisitor visitor) 
            => visitor.Visit(this);
    }
}